using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using Unity.VisualScripting;

public class CashRegister_MiniGame : MonoBehaviour
{
    public List<GameObject> possibleProducts;

    public List<GameObject> placementSlots;

    private List<GameObject> productsBought;

    private InputAction interact;

    private int productsToScan;

    private int productsScanned;

    private float priceTotal;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        interact = FindFirstObjectByType<PlayerInput>().actions.FindAction("Interact");
    }

    // Update is called once per frame
    void Update()
    {
        if (interact.WasPressedThisFrame())
        {
            Interact();
        }
    }

    void InitializeQuest()
    {
        productsToScan = 0;
        InstantiateProducts();
    }

    void InstantiateProducts()
    {
        int productsToInstantiate = Random.Range(1, 9);

        for (int i = 1; i <= productsToInstantiate; i++)
        {
            int randomProduct = Random.Range(0, possibleProducts.Count - 1);
            Instantiate(possibleProducts[randomProduct], placementSlots[i].transform.position, transform.rotation);
            productsToScan++;
        }
    }

    void Interact()
    {
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0.5f));

        if(Physics.Raycast(ray, out RaycastHit hit, 2, 6))
        {
            if (hit.collider.CompareTag("CashRegister"))
            {
                InitializeQuest();
            }


            if (hit.collider.CompareTag("CheckOutProduct"))
            {
                productInfo productInfo = hit.collider.gameObject.GetComponent<productInfo>();
                if (!productInfo.hasBeenScanned)
                {
                    productsBought.Add(hit.collider.gameObject);
                    priceTotal += hit.collider.gameObject.GetComponent<productInfo>().price;
                    productInfo.hasBeenScanned = true;
                    productsScanned++;
                }
                
            }
        }
    }
}
