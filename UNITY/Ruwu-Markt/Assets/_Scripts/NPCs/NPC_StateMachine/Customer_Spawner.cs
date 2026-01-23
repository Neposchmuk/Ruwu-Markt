using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public class Customer_Spawner : MonoBehaviour
{
    public List<GameObject> Customers;

    public List<Transform> Destinations;

    public List<Transform> CheckoutTargets;

    public Transform FinalDestination;

    public int TotalCustomers;

    public int SpawnInterval;

    private int _customersSpawned;

    private void Start()
    {
        _customersSpawned = 0;
        InvokeRepeating("SpawnCustomer", 10, SpawnInterval);
    }

    void SpawnCustomer()
    {
        if (_customersSpawned >= TotalCustomers) return;
        
        int _randomCustomer = Random.Range(0, Customers.Count);
        Instantiate(Customers[_randomCustomer], transform.position, transform.rotation);
        Customers.Remove(Customers[_randomCustomer]);
        _customersSpawned++;
    }
}
