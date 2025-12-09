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

    private float priceTotal;

    private float changeToGive;

    private float changeGiven;

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
            changeGiven = 0;
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
        priceTotal += productInfo.price;
        productInfo.hasBeenScanned = true;
        productsScanned++;
        //playAnimation
        RegisterScannedProducts.text += productInfo.name + " - " +productInfo.price + "$\n";
        RegisterTotalPrice.text ="Total: " + $"{priceTotal}$";
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
            ChangeButtons[0].onClick.AddListener(() => CountChange(0.01f));
            ChangeButtons[1].onClick.AddListener(() => CountChange(0.02f));
            ChangeButtons[2].onClick.AddListener(() => CountChange(0.05f));
            ChangeButtons[3].onClick.AddListener(() => CountChange(0.1f));
            ChangeButtons[4].onClick.AddListener(() => CountChange(0.2f));
            ChangeButtons[5].onClick.AddListener(() => CountChange(0.5f));
            ChangeButtons[6].onClick.AddListener(() => CountChange(1f));
            ChangeButtons[7].onClick.AddListener(() => CountChange(2f));
        }
        catch (IndexOutOfRangeException)
        {
            Debug.Log("More / Less than 8 Buttons registered in List!");
        }
        
    }

    void PayCard()
    {
        //playAnimationShowCard
        CleanUp(true, true);

    }

    void PayCash()
    {
        int moneyGiven = Mathf.CeilToInt(priceTotal / 5) * 5;
        Debug.Log(moneyGiven);
        changeToGive = (Mathf.Round((moneyGiven - priceTotal) * 100)) / 100;

        RegisterChangeToGive.text = "Change to give: \n" + $"{changeToGive}$";
        
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

    void CountChange(float changeValue)
    {
        changeGiven += changeValue;
        RegisterChangeGiven.text = "Change given:\n" + $"{changeGiven}$";
        if(changeGiven == changeToGive)
        {
            Debug.Log("Change given exactly!");
            CleanUp(false, true);
        }
        else if(changeGiven > changeToGive)
        {
            cashRegisterDeficit -= changeGiven - changeToGive;
            Debug.Log("Given too much change!");
            CleanUp(false, false);
        }

    }

    void CleanUp(bool paidWithCard, bool givenExactChange)
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
        if (paidWithCard)
        {
            SM.ChangeSanity(-5, 15);
        }
        else if(!paidWithCard && givenExactChange)
        {
            SM.ChangeSanity(-10, 15);
        }
        else
        {
            SM.ChangeSanity(-15, -20);
        }

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
            //Debug.Log("CashRegister RayCastHit");
            if (hit.collider.CompareTag("CashRegister") && !questIsRunning)
            {
                //Debug.Log("Interacted with Register");
                InitializeQuest();
            }


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
