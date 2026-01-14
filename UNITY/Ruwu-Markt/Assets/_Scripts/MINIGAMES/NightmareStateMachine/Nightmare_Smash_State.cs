using System;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class Nightmare_Smash_State : NightmareBaseState
{
    Nightmare_State_Manager _stateManager;

    int _objectsDestroyed;

    SmashThings_Animation _batParent;

    InputAction Attack;

    TMP_Text _smashedCounter;

    public override void EnterState(Nightmare_State_Manager stateManager)
    {
        _stateManager = stateManager;

        Attack = GameObject.FindFirstObjectByType<PlayerInput>().actions.FindAction("Attack");

        _smashedCounter = GameObject.FindGameObjectWithTag("SmashThingsCounter").GetComponent<TMP_Text>();

        _batParent = GameObject.FindFirstObjectByType<SmashThings_Animation>();

        SmashThings.OnDestroy += CountDestroyedObjects;
    }

    public override void UpdateState()
    {
        if (Attack.WasPressedThisDynamicUpdate())
        {
            _batParent.StartAnimatorCoroutine();
            
        }
    }

    public override void EndState()
    {
        _stateManager.EndNight(true, 10);
    }

    void CountDestroyedObjects()
    {
        _objectsDestroyed++;

        _smashedCounter.text = $"{_objectsDestroyed}" + "/" + $"40";

        if(_objectsDestroyed == 40)
        {
            SmashThings.OnDestroy -= CountDestroyedObjects;
            EndState();
        }
    }
}
