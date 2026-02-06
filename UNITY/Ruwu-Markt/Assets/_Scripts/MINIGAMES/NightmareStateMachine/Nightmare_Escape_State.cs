using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Nightmare_Escape_State : NightmareBaseState
{
    Nightmare_State_Manager _stateManager;

    InputAction _flash;

    Flashlight _flashlight;

    bool _flashActive = false;

    public override void EnterState(Nightmare_State_Manager stateManager)
    {
        _stateManager = stateManager;

        _flash = GameObject.FindFirstObjectByType<PlayerInput>().actions.FindAction("Flash");

        _flashlight = GameObject.FindFirstObjectByType<Flashlight>();

        RayCast.OnMarketLeave += EndState;

        RayCast.OnKeyPickup += ToggleKeyImage;

        GameEventsManager.instance.playerEvents.ToggleJump(false);

        GameEventsManager.instance.uiEvents.SendActionSprite(UI_Widget.FLASH, 0);

        GameEventsManager.instance.playerEvents.ChangeInputEventContext(InputEventContext.NIGHTMARE_ESCAPE);
    }

    public override void UpdateState()
    {
        
    }

    public override void EndState()
    {
        RayCast.OnMarketLeave -= EndState;
        _stateManager.EndNight(true, 10);
        GameEventsManager.instance.playerEvents.ToggleJump(true);
    }

    void ToggleKeyImage()
    {
        GameEventsManager.instance.uiEvents.SendActionSprite(UI_Widget.KEY, 1);
    }
}
