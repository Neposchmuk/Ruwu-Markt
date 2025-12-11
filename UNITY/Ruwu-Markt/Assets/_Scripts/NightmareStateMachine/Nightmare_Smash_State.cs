using UnityEngine;

public class Nightmare_Smash_State : NightmareBaseState
{
    Nightmare_State_Manager _stateManager;

    public override void EnterState(Nightmare_State_Manager stateManager)
    {
        _stateManager = stateManager;
    }

    public override void UpdateState()
    {
        throw new System.NotImplementedException();
    }

    public override void EndState()
    {
        throw new System.NotImplementedException();
    }
}
