using UnityEngine;
using UnityEngine.InputSystem;

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

        _stateManager.EscapeLevel.SetActive(true);
    }

    public override void UpdateState()
    {
        if (_flash.WasPressedThisFrame())
        {
            ToggleFlashlight();
        }
    }

    public override void EndState()
    {
        RayCast.OnMarketLeave -= EndState;
        _stateManager.EndNight(true, 10);
    }

    void ToggleFlashlight()
    {
        _flashActive = !_flashActive;

        if (_flashActive)
        {
            _flashlight.ToggleFlashlight(true);
        }
        else _flashlight.ToggleFlashlight(false);   
    }
}
