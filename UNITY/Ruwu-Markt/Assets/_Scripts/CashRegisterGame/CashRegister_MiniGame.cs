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

    public Button ButtonPayWithCash;

    public Button ButtonPayWithCard;

    public List<Button> ChangeButtons;

    public GameObject CashRegisterDrawer;



    public static event Action OnPay;



    private List<GameObject> productsBought = new List<GameObject>();

    private InputAction interact;

    private Sanity_Manager SM;

    private int productsToScan;

    private int productsScanned;

    private int _intPriceTotal;

    private float _floatChangeToGive;

    private float _floatChangeGiven;

    private float cashRegisterDeficit;

    private bool questIsRunning;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SM = FindFirstObjectByType<Sanity_Manager>();

        interact = FindFirstObjectByType<PlayerInput>().actions.FindAction("Interact");

        ButtonListeners();

        cashRegisterDeficit = 0;

        RegisterScannedProducts.text = "";
        RegisterTotalPrice.text = "";
        RegisterChangeToGive.text = "";
        RegisterChangeGiven.text = "";
        productsScanned = 0;
        ButtonPayWithCard.interactable = false;
        ButtonPayWithCash.interactable = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (interact.WasPressedThisFrame())
        {
            Interact();
        }
    }

    public void InitializeQuest()
    {
        if (!questIsRunning)
        {
            questIsRunning = true;
            productsToScan = 0;
            productsScanned = 0;
            _floatChangeGiven = 0;
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
        _intPriceTotal += (int)(productInfo.price * 100);
        productInfo.hasBeenScanned = true;
        productsScanned++;
        //playAnimation
        RegisterScannedProducts.text += productInfo.name + " - " +productInfo.price + "$\n";
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
            ButtonPayWithCard.interactable = true;
        }
        else
        {
            Debug.Log("Pays with cash");
            ButtonPayWithCash.interactable = true;
        }
    }

    void ButtonListeners()
    {
        ButtonPayWithCash.onClick.AddListener(() => PlayCashAnimation());
        ButtonPayWithCard.onClick.AddListener(() => PayCard());

        try
        {
            ChangeButtons[0].onClick.AddListener(() => CountChange(1));
            ChangeButtons[1].onClick.AddListener(() => CountChange(2));
            ChangeButtons[2].onClick.AddListener(() => CountChange(5));
            ChangeButtons[3].onClick.AddListener(() => CountChange(10));
            ChangeButtons[4].onClick.AddListener(() => CountChange(20));
            ChangeButtons[5].onClick.AddListener(() => CountChange(50));
            ChangeButtons[6].onClick.AddListener(() => CountChange(100));
            ChangeButtons[7].onClick.AddListener(() => CountChange(200));
        }
        catch (IndexOutOfRangeException)
        {
            Debug.Log("More / Less than 8 Buttons registered in List!");
        }
        
    }

    void PayCard()
    {
        //playAnimationShowCard
        CleanUp();

    }

    void PayCash()
    {
        int moneyGiven = Mathf.CeilToInt((float)_intPriceTotal / 500) * 500;
        Debug.Log((float)_intPriceTotal / 5);
        Debug.Log(Mathf.CeilToInt((float)_intPriceTotal / 5));
        Debug.Log(moneyGiven);
        _floatChangeToGive = (((float)moneyGiven / 100) - ((float)_intPriceTotal / 100));

        RegisterChangeToGive.text = "Change to give: \n" + $"{_floatChangeToGive}$";

        CashRegisterDrawer.SetActive(false);

        for(int i = 0; i < ChangeButtons.Count; i++)
        {
            ChangeButtons[i].interactable = true;
        }
        //CashRegisterDrawer animation
    }

    void PlayCashAnimation()
    {
        //playAnimationGiveCash
        Instantiate(moneyPrefab, moneySpawnPoint.transform.position, transform.rotation);
    }

    void CountChange(int changeValue)
    {
        _floatChangeGiven += (float)changeValue / 100;
        RegisterChangeGiven.text = "Change given:\n" + $"{_floatChangeGiven}$";
        if(_floatChangeGiven == _floatChangeToGive)
        {
            Debug.Log("Change given exactly!");
            CleanUp();
        }
        else if(_floatChangeGiven > _floatChangeToGive)
        {
            cashRegisterDeficit -= _floatChangeGiven - _floatChangeToGive;
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
        ButtonPayWithCard.interactable = false;
        ButtonPayWithCash.interactable = false;

        CashRegisterDrawer.SetActive(true);


        for (int i = 0; i < ChangeButtons.Count; i++)
        {
            ChangeButtons[i].interactable = false;
        }

        OnPay?.Invoke();

        questIsRunning = false;

        //play CashDrawer animation
    }

    void Interact()
    {
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

            if (hit.collider.CompareTag("UI_Button"))
            {
                Debug.Log("Hit UI_Button)");
                Button button = hit.collider.gameObject.GetComponent<Button>();
                if (button.interactable)
                {
                    button.onClick.Invoke();
                }
            }
        }
    }
}
