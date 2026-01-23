using UnityEngine;
using UnityEngine.AI;

public class NPC_Walking_State : NPC_BaseState
{
    bool _switchedState;

    NPC_State_Manager _stateManager;

    NavMeshAgent _agent;

    public override void EnterState(NPC_State_Manager State_Manager)
    {
        _stateManager = State_Manager;

        _agent = State_Manager.gameObject.GetComponent<NavMeshAgent>();

        _agent.isStopped = false;

        _agent.updateRotation = true;

        _switchedState = false;

        _stateManager.SetAnimatorTrigger("Walk");

        GameEventsManager.instance.npcEvents.ResetFace(_stateManager.gameObject);

        //walkingAnimation
    }

    public override void UpdateState()
    {
        if(_agent.remainingDistance == 0 && !_switchedState)
        {
            _switchedState = true;    
            _stateManager.SwitchState(_stateManager.Idle_State);
        }
    }

    public override void EndState()
    {
        throw new System.NotImplementedException();
    }
}
