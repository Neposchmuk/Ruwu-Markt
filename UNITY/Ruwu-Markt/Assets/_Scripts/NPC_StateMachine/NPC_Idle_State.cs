using NUnit.Framework;
using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;
using System.Collections;
using Unity.VisualScripting;

public class NPC_Idle_State : NPC_BaseState
{
    NPC_State_Manager _stateManager;

    NavMeshAgent _agent;



    public override void EnterState(NPC_State_Manager State_Manager)
    {
         _stateManager = State_Manager;

        _agent = State_Manager.gameObject.GetComponent<NavMeshAgent>();

        if (_stateManager._currentTarget == _stateManager._destinations.Count - 1)
        {
            _stateManager.SwitchState(_stateManager.Checkout_State);
            return;
        }

        

        if(_stateManager._currentTarget < _stateManager._destinations.Count - 1)
        {
            _agent.isStopped = true;

            NavMeshPath _path = new NavMeshPath();

            _agent.CalculatePath(_stateManager._destinations[_stateManager._currentTarget].position, _path);

            Debug.Log(_stateManager._currentTarget);

            _agent.SetPath(_path);

            _agent.updateRotation = false;
        }
        
        Debug.Log("Finished Idle EnterState");

        //_stateManager.SwitchState(_stateManager.Walking_State);
        

        //play Idle Animation
    }

    public override void UpdateState()
    {
        
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
        /*float angle = Vector3.SignedAngle(_stateManager.transform.forward, _target.forward, Vector3.up);
        Debug.Log(angle);
        if (angle < 0)
        {
            
        }
        else if(angle > 0)
        {
            TurnLeft(_target);
        }*/
    }

    void TurnRight(Transform _target)
    {
        Debug.LogError(-_stateManager.transform.rotation.y);

        Vector3 rotation = Vector3.Slerp(_stateManager.transform.forward, _target.forward, 0.5f);

        _stateManager.transform.rotation = Quaternion.Euler(rotation);

        Debug.LogError(-_stateManager.transform.rotation.y);
    }

    void TurnLeft(Transform _target)
    {
        Debug.LogError(-_stateManager.transform.rotation.y);

        Vector3 rotation = Vector3.Slerp(_stateManager.transform.forward, _target.forward, 0.5f);

        _stateManager.transform.rotation = Quaternion.Euler(rotation);

        Debug.LogError(-_stateManager.transform.rotation.y);
    }

    public IEnumerator RotateAgent()
    {
        if (_stateManager._currentTarget == 0) yield break;

        Transform _target = _stateManager._destinations[_stateManager._currentTarget - 1];

        float timer = 0;

        while(timer < 3)
        {
            float yRotation = Mathf.Lerp(_stateManager.transform.rotation.y, _target.rotation.y, 0.1f);

            //Debug.LogError(yRotation);

            _stateManager.transform.rotation = Quaternion.Euler(new Vector3 (0, yRotation, 0));

            timer += Time.deltaTime;

            yield return null;
        }
        
    }

}
