using UnityEditor.DeviceSimulation;
using UnityEngine;

public class Nightmare_Jump_State : NightmareBaseState
{
    Nightmare_State_Manager _stateManager;


    public override void EnterState(Nightmare_State_Manager stateManager)
    {
        _stateManager = stateManager;

        TouchGroundEvent.OnTouchedGround += EndState;

        _stateManager.JumpLevel.SetActive(true);
    }

    public override void UpdateState()
    {
        //throw new System.NotImplementedException();
    }

    public override void EndState()
    {
        TouchGroundEvent.OnTouchedGround -= EndState;
        _stateManager.EndNight(false, -10);
    }
}
