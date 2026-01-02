using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering;

public abstract class NPC_BaseState
{
    public abstract void EnterState(NPC_State_Manager State_Manager);

    public abstract void UpdateState();

    public abstract void EndState();
}
