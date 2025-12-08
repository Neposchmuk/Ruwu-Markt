using NUnit.Framework;
using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;
using System.Collections;

public class NPC_Idle_State : NPC_BaseState
{
    NPC_State_Manager _stateManager;

    NavMeshAgent _agent;



    public override void EnterState(NPC_State_Manager State_Manager)
    {
        _stateManager = State_Manager;

        _agent = State_Manager.gameObject.GetComponent<NavMeshAgent>();

        _agent.isStopped = true;

        NavMeshPath _path = new NavMeshPath();

        _agent.CalculatePath(_stateManager._destinations[_stateManager._currentTarget].position, _path);

        _agent.SetPath(_path);

        _agent.updateRotation = false;

        Debug.Log("Finished Idle EnterState");

        //_stateManager.SwitchState(_stateManager.Walking_State);
        

        //play Idle Animation
    }

    public override void UpdateState()
    {
        if (!_agent.updateRotation && _stateManager._currentTarget - 1 >= 0)
        {
            LookAtDestinationDirection(_stateManager._destinations[_stateManager._currentTarget - 1]);
        }
    }

    public override void EndState()
    {
        throw new System.NotImplementedException();
    }

    void LookAtDestinationDirection(Transform _target)
    {
        /*if(_stateManager.transform.rotation != _target.rotation)
        {
            Vector3 lookPosition = _target.position;
            lookPosition.y = 0;
            Quaternion lookRot = Quaternion.LookRotation(lookPosition);     
            _stateManager.transform.rotation = Quaternion.Slerp(_stateManager.transform.rotation, lookRot, 1f * Time.deltaTime);
        }*/
        float angle = Vector3.SignedAngle(_stateManager.transform.eulerAngles, _target.eulerAngles, Vector3.up);
        Debug.Log(angle);
        if (angle < 0)
        {
            TurnRight(_target);
        }
        else if(angle > 0)
        {
            TurnLeft(_target);
        }
    }

    void TurnRight(Transform _target)
    {
        float rotY = Mathf.Lerp(_stateManager.transform.rotation.y, _target.rotation.y, 1);

        _stateManager.transform.rotation = Quaternion.Euler(new Vector3 (0, rotY, 0));
    }

    void TurnLeft(Transform _target)
    {
        float rotY = Mathf.Lerp(_stateManager.transform.rotation.y, _target.rotation.y, -1);

        _stateManager.transform.rotation = Quaternion.Euler(new Vector3(0, rotY, 0));
    }
}
