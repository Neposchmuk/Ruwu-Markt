using System.IO;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public class NPC_Checkout_State : NPC_BaseState
{
    NPC_State_Manager _stateManager;

    NavMeshAgent _agent;

    bool _startedMinigame;

    bool _hasPaid = false;

    public override void EnterState(NPC_State_Manager State_Manager)
    {
        _stateManager = State_Manager;

        _agent = State_Manager.gameObject.GetComponent<NavMeshAgent>();

        CashRegister_MiniGame.OnPay += SwitchPaidBool;

        _agent.areaMask += 1 << NavMesh.GetAreaFromName("Checkout");

        NavMeshPath path = new NavMeshPath();

        _agent.CalculatePath(_stateManager.gameObject.GetComponent<Agent_TestScript>().CheckoutTarget.position, path);

        _agent.SetPath(path);

        _agent.isStopped = false;

        _agent.updateRotation = true;
    }

    public override void UpdateState()
    {
        if(_agent.remainingDistance <= 0 && !_agent.isStopped  && !_startedMinigame && !_hasPaid)
        {
            _agent.isStopped = true;

            _agent.updateRotation = false;

            GameObject.FindFirstObjectByType<CashRegister_MiniGame>().InitializeQuest();
        }
        else if (_hasPaid && _agent.isStopped)
        {
            NavMeshPath path = new NavMeshPath();

            _agent.CalculatePath(_stateManager.gameObject.GetComponent<Agent_TestScript>().LeaveMarketTarget.position, path);

            _agent.SetPath(path);

            _agent.isStopped = false;

            _agent.updateRotation = true;
        }
        else if (_agent.remainingDistance <= 0 && !_agent.isStopped && _hasPaid)
        {
            EndState();
        }
    }

    public override void EndState()
    {
        CashRegister_MiniGame.OnPay -= SwitchPaidBool;

        GameObject.Destroy(_stateManager.gameObject);
    }

    void SwitchPaidBool()
    {
        _hasPaid = !_hasPaid;
    }
}
