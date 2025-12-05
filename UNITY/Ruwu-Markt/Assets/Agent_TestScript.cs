using NUnit.Framework;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;

public class Agent_TestScript : MonoBehaviour
{
    //public GameObject Targets;

    public List<Transform> Target;

    private NavMeshAgent _agent;

    private int _currentTarget;

    private bool _hasTarget;

    private bool _waitingForCoroutine;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Target.AddRange(Targets.GetComponentsInChildren<Transform>());

        _agent = GetComponent<NavMeshAgent>();

        Debug.Log(_agent.gameObject);

        _currentTarget = -1;
    }

    private void Update()
    {
        if (_agent.pathStatus != NavMeshPathStatus.PathComplete) return;
        else if (_agent.pathStatus == NavMeshPathStatus.PathComplete && !_hasTarget)
        {
            StartCoroutine(WaitForNewDestination());
        }
        else if (_agent.pathStatus == NavMeshPathStatus.PathComplete && !_waitingForCoroutine)
        {
            _hasTarget = false;
            Debug.Log("Called second else if: hasTarget =" + _hasTarget);
        }
        
    }

    IEnumerator WaitForNewDestination()
    {
        Debug.Log("Called Coroutine");
        _hasTarget = true;

        _waitingForCoroutine = true;

        _currentTarget++;

        yield return new WaitForSeconds(3);

        SetNextDestination(_currentTarget);

        _waitingForCoroutine = false;
    }

    void SetNextDestination(int _targetIndex)
    {
        Debug.Log(_targetIndex);
        _agent.SetDestination(Target[_targetIndex].position);
    }

    void OnReachedDestination()
    {
        _hasTarget = false;
    } 
}
