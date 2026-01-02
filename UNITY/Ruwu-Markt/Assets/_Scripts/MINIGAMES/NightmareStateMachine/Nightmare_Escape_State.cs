using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Nightmare_Escape_State : NightmareBaseState
{
    Nightmare_State_Manager _stateManager;

    InputAction _flash;

    Flashlight _flashlight;

    Image _keyImage;

    bool _flashActive = false;

    public override void EnterState(Nightmare_State_Manager stateManager)
    {
        _stateManager = stateManager;

        _flash = GameObject.FindFirstObjectByType<PlayerInput>().actions.FindAction("Flash");

        _flashlight = GameObject.FindFirstObjectByType<Flashlight>();

        _keyImage = GameObject.FindGameObjectWithTag("EscapeKeyImage").GetComponent<Image>();

        RayCast.OnMarketLeave += EndState;

        RayCast.OnKeyPickup += ToggleKeyImage;

        _keyImage.enabled = false;
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

    void ToggleKeyImage()
    {
        _keyImage.enabled = true;
    }
}
