using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering;

public class Customer_Behaviour : MonoBehaviour
{
    public int _currentTarget;

    public List<Transform> Destinations;

    public Transform[] CheckoutTargets;

    public Transform LeaveMarketTarget;

    public int _destinationsToReach { get; private set; }

    public Transform[] _randomDestinations { get; private set; }

    public bool IsInTrigger;

    private Trigger_NPC_Method[] _destinationTriggers;

    private NPC_State_Manager _stateManager;

    private NavMeshAgent _agent;

    private bool _headingToCheckout = false;

    private bool _isAtCheckout = false;

    private int _currentCheckoutSlot;

    private Trigger_NPC_Method[] _checkoutTriggers;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        _stateManager = GetComponent<NPC_State_Manager>();

        _agent = GetComponent<NavMeshAgent>();

        _destinationsToReach = RandomizeDestinations();

        Debug.Log("Destinations: " + _destinationsToReach);

        _randomDestinations = SetRandomDestinations();

        _destinationTriggers = new Trigger_NPC_Method[_randomDestinations.Length];

        for (int i = 0; i < _randomDestinations.Length; i++)
        {
            _destinationTriggers[i] = _randomDestinations[i].GetComponent<Trigger_NPC_Method>();
        }

        Debug.Log("DestinationsLength: " + _randomDestinations.Length);

        _currentTarget = 0;

        _checkoutTriggers = new Trigger_NPC_Method[CheckoutTargets.Length];

        for (int i=0; i < CheckoutTargets.Length; i++)
        {
            _checkoutTriggers[i] = CheckoutTargets[i].GetComponent<Trigger_NPC_Method>();
        }
    }

    private void Start()
    {
        SetDestination();

        //StartWalking();
    }

    private void Update()
    {
        if (_destinationTriggers[_currentTarget].IsOccupied && !IsInTrigger && !_headingToCheckout)
        {
            if(_agent.remainingDistance <= 3f)
            {
                if(_currentTarget < _randomDestinations.Length - 1)
                {
                    _currentTarget++;

                    SetDestination();
                }
                else if(_currentTarget == _randomDestinations.Length - 1)
                {
                    StartCoroutine(WaitForCheckout());
                }
            }
        }
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
            _lastIndex = _randomIndex;
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

    public void CheckFinalDestination()
    {
        if (_currentTarget == _randomDestinations.Length - 1 && !_headingToCheckout)
        {
            StartCoroutine(WaitForCheckout());
        }
        else if(_currentTarget != _randomDestinations.Length - 1 && !_headingToCheckout)
        {
            StartCoroutine(WaitForNewDestination());
        }
    }

    public void StartCheckoutGame()
    {
        _isAtCheckout = true;
        GameObject.FindFirstObjectByType<CashRegister_MiniGame>().InitializeQuest();
    }

    public void MoveUpCheckout()
    {
        if (_headingToCheckout && _currentCheckoutSlot > 0)
        {
            _currentCheckoutSlot--;
            Debug.Log(gameObject.name + " " + _currentCheckoutSlot);

            //Debug.Log("Entered MoveUp");

            NavMeshPath path = new NavMeshPath();

            _agent.CalculatePath(CheckoutTargets[_currentCheckoutSlot].position, path);

            _agent.SetPath(path);

            StartWalking();
        }     
    }

    public void CheckSlotAhead()
    {
        //Debug.Log("Entered CheckSlots");
        //Debug.Log(!_checkoutTriggers[_currentCheckoutSlot - 1].IsOccupied);
        if (!_checkoutTriggers[_currentCheckoutSlot - 1].IsOccupied)
        {
            MoveUpCheckout();
        }
    }

    public void FinalDestination()
    {
        NavMeshPath path = new NavMeshPath();

        _agent.CalculatePath(LeaveMarketTarget.position, path);

        _agent.SetPath(path);

        StartWalking();
    }

    public void Kill()
    {
        Trigger_NPC_Method.OnCheckoutLeave -= MoveUpCheckout;
        Destroy(gameObject);
    }

    IEnumerator WaitForNewDestination()
    {
        yield return new WaitForSeconds(3);

        _currentTarget++;

        SetDestination();
    }

    IEnumerator WaitForCheckout()
    {
        Debug.Log("Starting Checkout Coroutine");

        yield return new WaitForSeconds(3);

        NavMeshPath path = new NavMeshPath();

        Trigger_NPC_Method.OnCheckoutLeave += MoveUpCheckout;

        _agent.areaMask += 1 << NavMesh.GetAreaFromName("Checkout");    

        _agent.CalculatePath(CheckoutTargets[3].position, path);

        _agent.SetPath(path);

        _headingToCheckout = true;

        _currentCheckoutSlot = 3;

        StartWalking();
    }
}
