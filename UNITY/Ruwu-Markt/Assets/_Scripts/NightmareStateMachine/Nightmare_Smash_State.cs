using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Nightmare_Smash_State : NightmareBaseState
{
    Nightmare_State_Manager _stateManager;

    int _objectsDestroyed;

    SmashThings_Animation _batParent;

    InputAction Attack;

    public override void EnterState(Nightmare_State_Manager stateManager)
    {
        _stateManager = stateManager;

        Attack = GameObject.FindFirstObjectByType<PlayerInput>().actions.FindAction("Attack");

        _batParent = GameObject.FindFirstObjectByType<SmashThings_Animation>();

        SmashThings.OnDestroy += CountDestroyedObjects;

        _stateManager.SmashLevel.SetActive(true);
    }

    public override void UpdateState()
    {
        if (Attack.WasPressedThisFrame())
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
        if(_objectsDestroyed == 20)
        {
            SmashThings.OnDestroy -= CountDestroyedObjects;
            EndState();
        }
    }
}
