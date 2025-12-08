using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_State_Manager : MonoBehaviour
{
    public List<Transform> _destinations { get; private set; }

    public int _currentTarget;


    NPC_BaseState currentState;
    public NPC_Idle_State Idle_State = new NPC_Idle_State();
    public NPC_Walking_State Walking_State = new NPC_Walking_State();

    private void Start()
    {
        _destinations = GetComponent<Agent_TestScript>().Target;

        _currentTarget = 0;

        SwitchState(Idle_State);
    }

    private void Update()
    {
        currentState.UpdateState();
    }

    public void SwitchState(NPC_BaseState state)
    {
        currentState = state;

        currentState.EnterState(this);
    }

    
}
