using NUnit.Framework;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using System;
using static UnityEngine.ProBuilder.AutoUnwrapSettings;

public class Agent_TestScript : MonoBehaviour
{
    //public GameObject Targets;

    public List<Transform> Target;

    private NavMeshAgent _agent;

    private int _currentTarget;

    private bool _gotFirstTarget = false;

    private bool _waitingForCoroutine;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Target.AddRange(Targets.GetComponentsInChildren<Transform>());

        _agent = GetComponent<NavMeshAgent>();

        Debug.Log(_agent.gameObject);

        _currentTarget = 0;

        StartCoroutine(SetFirstDestination());
    }

    private void Update()
    {
        if(_gotFirstTarget && !_agent.hasPath && !_agent.pathPending && !_waitingForCoroutine && _currentTarget<=Target.Count)
        {
            StartCoroutine(WaitForNewDestination());
            FaceTarget(Target[_currentTarget - 1]);
        }
    }

    IEnumerator SetFirstDestination()
    {
        yield return new WaitForSeconds(1);

        SetNextDestination(_currentTarget);

        _gotFirstTarget = true;
    }

    IEnumerator WaitForNewDestination()
    {
        _waitingForCoroutine = true;

        _currentTarget++;

        yield return new WaitForSeconds(5);

        try
        {
            SetNextDestination(_currentTarget);
        }
        catch (IndexOutOfRangeException)
        {
            Debug.Log("End of Targets reached");
        }
        
        _waitingForCoroutine = false;
    }

    void SetNextDestination(int _targetIndex)
    {
        Debug.Log(_targetIndex);
        _agent.SetDestination(Target[_targetIndex].position);;
    }

    private void FaceTarget(Transform destination)
    {
        Vector3 lookRot = destination.forward - transform.forward;
        lookRot.y = 0;
        Quaternion rotation = Quaternion.LookRotation(lookRot);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 5);
    }

}
