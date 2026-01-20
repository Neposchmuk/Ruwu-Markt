using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_State_Manager : MonoBehaviour
{
    NPC_BaseState currentState;
    public NPC_Idle_State Idle_State = new NPC_Idle_State();
    public NPC_Walking_State Walking_State = new NPC_Walking_State();
    public NPC_Checkout_State Checkout_State = new NPC_Checkout_State();

    [SerializeField] private Animator animator;

    private string currentTrigger;

    private void Awake()
    {
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

    public void SetAnimatorTrigger(string trigger)
    {
        if(currentTrigger != null)
        {
            animator.ResetTrigger(currentTrigger);
        }

        animator.SetTrigger(trigger);
        currentTrigger = trigger;
    }
}
