using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering;

public class Customer_Behaviour : MonoBehaviour
{
    public int _currentTarget;

    public List<Transform> Destinations;

    public Transform CheckoutTarget;

    public Transform LeaveMarketTarget;

    public int _destinationsToReach { get; private set; }

    public Transform[] _randomDestinations { get; private set; }

    private NPC_State_Manager _stateManager;

    private NavMeshAgent _agent;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        _stateManager = GetComponent<NPC_State_Manager>();

        _agent = GetComponent<NavMeshAgent>();

        _destinationsToReach = RandomizeDestinations();

        Debug.Log("Destinations: " + _destinationsToReach);

        _randomDestinations = SetRandomDestinations();

        Debug.Log("DestinationsLength: " + _randomDestinations.Length);

        _currentTarget = 0;
    }

    private void Start()
    {
        SetDestination();

        //StartWalking();

        NPC_Walking_State.OnDestinationReached += CheckFinalDestination;
    }

    int RandomizeDestinations()
    {
        int _destinationsToReach = Random.Range(2, Destinations.Count + 1);

        return _destinationsToReach;
    }

    public Transform[] SetRandomDestinations()
    {
        int _lastIndex = -1;

        Transform[] _randomDestinations = new Transform[_destinationsToReach];

        for (int i = 0; i < _destinationsToReach; i++)
        {
            int _randomIndex = Random.Range(_lastIndex + 1, Destinations.Count - _destinationsToReach + i);
            _lastIndex++;
            _randomDestinations[i] = Destinations[_randomIndex];
            Debug.Log(Destinations[_randomIndex].position);
        }

        return _randomDestinations;
    }

    void StartWalking()
    {
        _stateManager.SwitchState(_stateManager.Walking_State);
    }

    void SetDestination()
    {
        NavMeshPath path = new NavMeshPath();

        _agent.CalculatePath(_randomDestinations[_currentTarget].position, path);

        _agent.SetPath(path);

        StartWalking();
    }

    void CheckFinalDestination()
    {
        Debug.Log("Entered Event Function");

        if (_currentTarget == _randomDestinations.Length - 1)
        {
            NPC_Walking_State.OnDestinationReached -= CheckFinalDestination;

            StartCoroutine(WaitForSeconds());

            _stateManager.SwitchState(_stateManager.Checkout_State);
        }
        else
        {
            StartCoroutine(WaitForSeconds());

            _currentTarget++;

            SetDestination();
        }
    }

    IEnumerator WaitForSeconds()
    {
        yield return new WaitForSeconds(3);
    }
}
