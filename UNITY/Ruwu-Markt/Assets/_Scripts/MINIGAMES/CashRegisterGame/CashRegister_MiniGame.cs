using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using Unity.VisualScripting;
using TMPro;
using UnityEngine.UI;
using System;

public class CashRegister_MiniGame : MonoBehaviour
{
    public LayerMask RCLayerMask;

    public GameObject moneyPrefab;

    public List<GameObject> possibleProducts;

    public List<GameObject> placementSlots;

    public GameObject moneySpawnPoint;

    public TMP_Text RegisterScannedProducts;

    public TMP_Text RegisterTotalPrice;

    public TMP_Text RegisterChangeToGive;

    public TMP_Text RegisterChangeGiven;

    public GameObject CashRegisterDrawer;


    private List<GameObject> productsBought = new List<GameObject>();

    private InputAction interact;

    private Sanity_Manager SM;

    private int productsToScan;

    private int productsScanned;

    private int _intPriceTotal;

    private int _intChangeToGive;

    private int _intChangeGiven;

    private float cashRegisterDeficit;

    private bool questIsRunning;

    private GameObject agent;


    private void OnEnable()
    {
        GameEventsManager.instance.questEvents.onButtonAddChange += CountChange;
        GameEventsManager.instance.questEvents.onPayCard += PayCard;
        GameEventsManager.instance.questEvents.onPayCash += PlayCashAnimation;
        GameEventsManager.instance.playerEvents.onPressedInteract += Interact;
        GameEventsManager.instance.checkoutEvents.onStartCheckoutGame += InitializeQuest;
    }

    private void OnDisable()
    {
        GameEventsManager.instance.questEvents.onButtonAddChange -= CountChange;
        GameEventsManager.instance.questEvents.onPayCard -= PayCard;
        GameEventsManager.instance.questEvents.onPayCash -= PlayCashAnimation;
        GameEventsManager.instance.playerEvents.onPressedInteract += Interact;
        GameEventsManager.instance.checkoutEvents.onStartCheckoutGame -= InitializeQuest;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cashRegisterDeficit = 0;

        RegisterScannedProducts.text = "";
        RegisterTotalPrice.text = "";
        RegisterChangeToGive.text = "";
        RegisterChangeGiven.text = "";
        productsScanned = 0;

        GameEventsManager.instance.questEvents.ToggleButtonInteractable(UIButtonType.CASH, false);
        GameEventsManager.instance.questEvents.ToggleButtonInteractable(UIButtonType.PAYCARD, false);
        GameEventsManager.instance.questEvents.ToggleButtonInteractable(UIButtonType.PAYCASH, false);
    }

    public void InitializeQuest(GameObject agent)
    {
        Debug.Log(questIsRunning);
        if (!questIsRunning)
        {
            this.agent = agent;

            questIsRunning = true;
            productsToScan = 0;
            productsScanned = 0;
            _intChangeGiven = 0;
            InstantiateProducts();
        }
    }

    void InstantiateProducts()
    {
        int productsToInstantiate = UnityEngine.Random.Range(1, 9);

        for (int i = 1; i <= productsToInstantiate; i++)
        {
            int randomProduct = UnityEngine.Random.Range(0, possibleProducts.Count - 1);
            productsBought.Add(Instantiate(possibleProducts[randomProduct], placementSlots[i].transform.position, transform.rotation));
            productsToScan++;
        }

        Debug.Log("Called Register Initialize Quest");
    }

    void ScanProduct(productInfo productInfo)
    {
        _intPriceTotal += productInfo.price;
        productInfo.hasBeenScanned = true;
        productsScanned++;
        //playAnimation
        RegisterScannedProducts.text += productInfo.name + " - " +(float)productInfo.price/100 + "$\n";
        RegisterTotalPrice.text ="Total: " + $"{(float)_intPriceTotal / 100}$";
        if(productsScanned == productsToScan)
        {
            CheckPaymentMethod();
        }
    }

    void CheckPaymentMethod()
    {
        int payWithCard = UnityEngine.Random.Range(1, 7);
        if(payWithCard > 4)
        {
            Debug.Log("Pays with Card");
            GameEventsManager.instance.questEvents.ToggleButtonInteractable(UIButtonType.PAYCARD, true);
        }
        else
        {
            Debug.Log("Pays with cash");
            GameEventsManager.instance.questEvents.ToggleButtonInteractable(UIButtonType.PAYCASH, true);
        }
    }

    void PayCard()
    {
        //playAnimationShowCard
        CleanUp();

    }

    void PayCash()
    {
        /*Debug.Log(_intPriceTotal);
        Debug.Log(Mathf.CeilToInt((float)_intPriceTotal / 100 * 5));
        int moneyGiven = Mathf.CeilToInt(Mathf.CeilToInt((float)_intPriceTotal / 100 * 5 )/ 5) * 100;*/

        int moneyGiven = _intPriceTotal;
        moneyGiven += 500 - (moneyGiven % 500);
        Debug.Log(moneyGiven);

        _intChangeToGive = moneyGiven - _intPriceTotal;
        Debug.Log(_intChangeToGive);
        RegisterChangeToGive.text = "Change to give: \n" + $"{(float)_intChangeToGive / 100}$";

        

        CashRegisterDrawer.SetActive(false);

        GameEventsManager.instance.questEvents.ToggleButtonInteractable(UIButtonType.CASH, true);
        //CashRegisterDrawer animation
    }

    void PlayCashAnimation()
    {
        //playAnimationGiveCash
        Instantiate(moneyPrefab, moneySpawnPoint.transform.position, transform.rotation);
    }

    void CountChange(int changeValue)
    {
        _intChangeGiven += changeValue;
        Debug.Log(_intChangeGiven);
        RegisterChangeGiven.text = "Change given:\n" + $"{(float)_intChangeGiven / 100}$";
        if(_intChangeGiven == _intChangeToGive)
        {
            Debug.Log("Change given exactly!");
            CleanUp();
        }
        else if(_intChangeGiven > _intChangeToGive)
        {
            cashRegisterDeficit -= _intChangeGiven - _intChangeToGive;
            Debug.Log("Given too much change!");
            CleanUp();
        }

    }

    void CleanUp()
    {
        Debug.Log(productsBought.Count);
        for (int i = 0; i < productsBought.Count; i++)
        {
            Destroy(productsBought[i]);
        }

        RegisterScannedProducts.text = "";
        RegisterTotalPrice.text = "";
        RegisterChangeToGive.text = "";
        RegisterChangeGiven.text = "";
        productsScanned = 0;

        CashRegisterDrawer.SetActive(true);


        GameEventsManager.instance.questEvents.ToggleButtonInteractable(UIButtonType.CASH, false);
        GameEventsManager.instance.questEvents.ToggleButtonInteractable(UIButtonType.PAYCARD, false);
        GameEventsManager.instance.questEvents.ToggleButtonInteractable(UIButtonType.PAYCASH, false);

        GameEventsManager.instance.checkoutEvents.Pay(agent);

        questIsRunning = false;

        //play CashDrawer animation
    }

    void Interact(InputEventContext inputEventContext)
    {
        if(inputEventContext != InputEventContext.DEFAULT) return;

        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0.5f));

        if(Physics.Raycast(ray, out RaycastHit hit, 2, RCLayerMask))
        {
            if (hit.collider.CompareTag("CheckOutProduct"))
            {
                productInfo productInfo = hit.collider.gameObject.GetComponent<productInfo>();
                if (!productInfo.hasBeenScanned)
                {
                    ScanProduct(productInfo);
                    hit.collider.gameObject.transform.localPosition += new Vector3(0,0,-2.5f);
                }
                
            }

            if (hit.collider.CompareTag("Cash"))
            {
                PayCash();
                Destroy(hit.collider.gameObject);
            }

            
        }
    }
}
