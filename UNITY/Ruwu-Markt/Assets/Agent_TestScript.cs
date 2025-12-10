using NUnit.Framework;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using System;
using static UnityEngine.ProBuilder.AutoUnwrapSettings;
using Unity.VisualScripting;

public class Agent_TestScript : MonoBehaviour
{
    //public GameObject Targets;

    public List<Transform> Target;

    public Transform CheckoutTarget;

    public Transform LeaveMarketTarget;

    private NPC_State_Manager _stateManager;

    private NavMeshAgent _agent;

    private bool _calledCoroutine;

    private Coroutine _rotationCoroutine;

    private void Start()
    {
        _stateManager = GetComponent<NPC_State_Manager>();

        _agent = GetComponent<NavMeshAgent>();

        //StartCoroutine(SwitchToWalk());
    }

    private void Update()
    {
        if (!_agent.isStopped) return;
        //else if (!_calledCoroutine && _stateManager._currentTarget < _stateManager._destinations.Count - 1) StartCoroutine(SwitchToWalk());
    }

    /*IEnumerator SwitchToWalk()
    {
        Debug.Log("Called Coroutine");

        _calledCoroutine = true;

        yield return _rotationCoroutine = StartCoroutine(_stateManager.Idle_State.RotateAgent());

        //yield return new WaitForSeconds(3);

        if(_rotationCoroutine != null)
        {
            StopCoroutine(_rotationCoroutine);
        }     

        _stateManager.SwitchState(_stateManager.Walking_State);

        _calledCoroutine = false;
    }*/
}
