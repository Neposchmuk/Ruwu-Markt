using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering;

public class Customer_Behaviour : MonoBehaviour
{
    public int _currentTarget;

    private List<Transform> Destinations = new List<Transform>();

    private List<Transform> CheckoutTargets = new List<Transform>();

    private Transform LeaveMarketTarget;

    public int _destinationsToReach { get; private set; }

    public Transform[] _randomDestinations { get; private set; }

    public bool IsInTrigger;

    private Trigger_NPC_Method[] _destinationTriggers;

    private NPC_State_Manager _stateManager;

    private NavMeshAgent _agent;

    private Customer_Spawner _spawner;

    private bool _headingToCheckout = false;

    private bool _waitingForCoroutine;

    private bool subscribedToMoveUp;

    private bool _isAtCheckout = false;

    private bool _hasPaid = false;

    private int _currentCheckoutSlot;

    private float SpawnTime;

    private Trigger_NPC_Method[] _checkoutTriggers;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        _stateManager = GetComponent<NPC_State_Manager>();

        _agent = GetComponent<NavMeshAgent>();

        _spawner = FindFirstObjectByType<Customer_Spawner>();

        Destinations = _spawner.Destinations;

        CheckoutTargets = _spawner.CheckoutTargets;

        LeaveMarketTarget = _spawner.FinalDestination;

        //Debug.Log(CheckoutTargets[0]);

        //Debug.Log(CheckoutTargets.Count);

        LeaveMarketTarget = GameObject.FindGameObjectWithTag("Final_Target").GetComponent<Transform>();


        _destinationsToReach = RandomizeDestinations();

        _randomDestinations = SetRandomDestinations();

        _destinationTriggers = new Trigger_NPC_Method[_randomDestinations.Length];

        for (int i = 0; i < _randomDestinations.Length; i++)
        {
            _destinationTriggers[i] = _randomDestinations[i].GetComponent<Trigger_NPC_Method>();
        }

        _currentTarget = 0;

        _checkoutTriggers = new Trigger_NPC_Method[CheckoutTargets.Count];

        for (int i=0; i < CheckoutTargets.Count; i++)
        {
            _checkoutTriggers[i] = CheckoutTargets[i].GetComponent<Trigger_NPC_Method>();
        }
    }

    private void OnEnable()
    {
        GameEventsManager.instance.questEvents.onAllTasksCompleted += StartCheckoutBehaviour;
        GameEventsManager.instance.checkoutEvents.onArrivedAtTarget += NextTargetBehaviour;
        GameEventsManager.instance.checkoutEvents.onKillAgent += Kill;
        GameEventsManager.instance.checkoutEvents.onPay += FinalDestination;
        GameEventsManager.instance.checkoutEvents.onSetNPCTrigger += SetTriggerMethod;
        
    }

    private void OnDisable()
    {
        
        GameEventsManager.instance.checkoutEvents.onArrivedAtTarget -= NextTargetBehaviour;
        GameEventsManager.instance.checkoutEvents.onKillAgent -= Kill;
        GameEventsManager.instance.checkoutEvents.onPay -= FinalDestination;
        GameEventsManager.instance.checkoutEvents.onSetNPCTrigger -= SetTriggerMethod;
        GameEventsManager.instance.checkoutEvents.onEnteredCheckoutLine -= CheckCheckoutLine;
        
    }

    private void Start()
    {
        SetDestination();

        SpawnTime = Time.time;

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
                else if(_currentTarget == _randomDestinations.Length - 1 && !_waitingForCoroutine)
                {
                    _waitingForCoroutine = true;
                    StartCoroutine(WaitForCheckout());
                    Debug.Log("Called Coroutine from Update: " + gameObject);
                }
            }
        }
    }

    private void SetTriggerMethod(GameObject agent, bool inTrigger)
    {
        if(agent != this.gameObject) return;

        IsInTrigger = inTrigger;
    }

    private void NextTargetBehaviour(GameObject agent)
    {
        if(agent != this.gameObject) return;

        Debug.Log("Entered Next Target: " + gameObject);

        CheckFinalDestination();
    }

    int RandomizeDestinations()
    {
        int _destinationsToReach = UnityEngine.Random.Range(2, Destinations.Count + 1);

        return _destinationsToReach;
    }

    public Transform[] SetRandomDestinations()
    {
        int _lastIndex = -1;

        Transform[] _randomDestinations = new Transform[_destinationsToReach];

        for (int i = 0; i < _destinationsToReach; i++)
        {
            int _randomIndex = UnityEngine.Random.Range(_lastIndex + 1, Destinations.Count - _destinationsToReach + i);
            _lastIndex = _randomIndex;
            _randomDestinations[i] = Destinations[_randomIndex];
        }

        return _randomDestinations;
    }

    void StartWalking()
    {
        _stateManager.SwitchState(_stateManager.Walking_State);
    }

    void SetDestination()
    {
        if(_headingToCheckout) return;

        NavMeshPath path = new NavMeshPath();

        _agent.CalculatePath(_randomDestinations[_currentTarget].position, path);

        _agent.SetPath(path);

        StartWalking();
    }

    private void StartCheckoutBehaviour()
    {
        if(_headingToCheckout) return;

        GameEventsManager.instance.questEvents.onAllTasksCompleted -= StartCheckoutBehaviour;

        GameEventsManager.instance.checkoutEvents.onRecalculateCheckoutSlot += RecalculateCheckoutSlot;

        GameEventsManager.instance.checkoutEvents.onEnteredCheckoutLine += CheckCheckoutLine;

        NavMeshPath path = new NavMeshPath();

        _agent.CalculatePath(CheckoutTargets[CheckoutTargets.Count - 1].position, path);

        _agent.SetPath(path);

        StartWalking();

        _headingToCheckout = true;
    }

    private void CheckCheckoutLine(GameObject agent)
    {
        if(agent != this.gameObject) return;

        if (!subscribedToMoveUp)
        {
            GameEventsManager.instance.checkoutEvents.onPay += MoveUpCheckout;
            subscribedToMoveUp = true;
        }

        GameEventsManager.instance.checkoutEvents.onSendSlotUpdate += CheckSlotOccupation;

        RequestSlotUpdate(0);
    }

    private void RequestSlotUpdate(int slotIndex)
    {
        _currentCheckoutSlot = slotIndex;

        try
        {
            GameEventsManager.instance.checkoutEvents.RequestSlotUpdate(CheckoutTargets[slotIndex].gameObject, this.gameObject);
        }
        catch (ArgumentOutOfRangeException)
        {
            Debug.LogError("ArgumentException: " + slotIndex);
        }
        
    }

    private void CheckSlotOccupation(GameObject slot, bool occupied, GameObject agent)
    {
        if(agent != this.gameObject) return;

        if (occupied)
        {
            _currentCheckoutSlot++;

            RequestSlotUpdate(_currentCheckoutSlot);
        }
        else
        {
            GameEventsManager.instance.checkoutEvents.ReserveSlot(slot);
            SetCheckoutPath(slot);
        }
    }

    private void RecalculateCheckoutSlot(GameObject agent)
    {
        if(agent == this.gameObject) return;

        if(_currentCheckoutSlot == 0)
        {
            RequestSlotUpdate(0);
        }
        
    }

    private void SetCheckoutPath(GameObject slot)
    {
        NavMeshPath path = new NavMeshPath();

        _agent.areaMask += 1 << NavMesh.GetAreaFromName("Checkout");    

        _agent.CalculatePath(slot.transform.position, path);

        _agent.SetPath(path);

        StartWalking();

        GameEventsManager.instance.checkoutEvents.onSendSlotUpdate -= CheckSlotOccupation;
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

    public void MoveUpCheckout(GameObject agent)
    {
        if(agent == this.gameObject) return;

        if (_headingToCheckout && _currentCheckoutSlot > 0)
        {
            _currentCheckoutSlot--;

            Debug.Log(gameObject + ": Moving up to Slot " + _currentCheckoutSlot);
            //Debug.Log(gameObject.name + " " + _currentCheckoutSlot);

            //Debug.Log("Entered MoveUp");

            NavMeshPath path = new NavMeshPath();

            _agent.CalculatePath(CheckoutTargets[_currentCheckoutSlot].position, path);

            _agent.SetPath(path);

            StartWalking();
        }     
    }

    public void FinalDestination(GameObject agent)
    {
        if(agent != this.gameObject) return;

        NavMeshPath path = new NavMeshPath();

        _agent.CalculatePath(LeaveMarketTarget.position, path);

        _agent.SetPath(path);

        StartWalking();

        GameEventsManager.instance.checkoutEvents.onRecalculateCheckoutSlot -= RecalculateCheckoutSlot;

        GameEventsManager.instance.checkoutEvents.onPay -= MoveUpCheckout;
    }

    public void Kill(GameObject agent)
    {
        if(agent != this.gameObject) return;
        
        Destroy(gameObject);
    }

    IEnumerator WaitForNewDestination()
    {
        Debug.Log("Started NewDestination Coroutine: " + gameObject);

        yield return new WaitForSeconds(5);

        _currentTarget++;

        SetDestination();
    }

    IEnumerator WaitForCheckout()
    {
        Debug.Log("Starting Checkout Coroutine");

        yield return new WaitForSeconds(5);

        StartCheckoutBehaviour();
    }
}
