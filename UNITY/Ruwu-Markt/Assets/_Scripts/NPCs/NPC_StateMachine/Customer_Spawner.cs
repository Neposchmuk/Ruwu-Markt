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

    [Header("Number of NPCS to spawn on Start")]
    [SerializeField] private int earlySpawns;

    [Header("SpawnPoints to Instantiate NPCs at, must be the same number as early spawns")]
    [SerializeField] private GameObject[] earlySpawnPoints;

    private bool isFirstCustomer = true;

    private int _customersSpawned;

    private void Start()
    {
        _customersSpawned = 0;
        EarlySpawns();
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

    void EarlySpawns()
    {
        if(earlySpawns > TotalCustomers)
        {
            Debug.LogError("Number of early spawns exceeds Customer Limit!");
            return;
        }

        for(int i = 0; i<earlySpawns; i++)
        {
            int _randomCustomer = Random.Range(0, Customers.Count);
            GameObject agent = Instantiate(Customers[_randomCustomer], earlySpawnPoints[i].transform.position, transform.rotation);
            if (isFirstCustomer)
            {
                GameEventsManager.instance.questEvents.AllTasksCompleted();
                isFirstCustomer = false;
            }
            Customers.Remove(Customers[_randomCustomer]);
            _customersSpawned++;
        }
        
    }
}
