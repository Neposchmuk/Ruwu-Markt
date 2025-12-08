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

    private NPC_State_Manager _stateManager;

    private NavMeshAgent _agent;

    private bool _calledCoroutine;

    private void Start()
    {
        _stateManager = GetComponent<NPC_State_Manager>();

        _agent = GetComponent<NavMeshAgent>();

        StartCoroutine(SwitchToWalk());
    }

    private void Update()
    {
        if (!_agent.isStopped) return;
        else if (!_calledCoroutine) StartCoroutine(SwitchToWalk());
    }

    IEnumerator SwitchToWalk()
    {
        Debug.Log("Called Coroutine");

        _calledCoroutine = true;

        yield return new WaitForSeconds(3);

        _stateManager.SwitchState(_stateManager.Walking_State);

        _calledCoroutine = false;
    }
}
