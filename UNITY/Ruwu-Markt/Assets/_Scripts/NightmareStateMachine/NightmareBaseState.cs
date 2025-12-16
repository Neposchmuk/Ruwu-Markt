using UnityEngine;

public abstract class NightmareBaseState
{
    public abstract void EnterState(Nightmare_State_Manager stateManager);

    public abstract void UpdateState();

    public abstract void EndState();
}
