using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Nightmare_Smash_State : NightmareBaseState
{
    public static Action OnAttack;

    public static Action OnFinish;



    Nightmare_State_Manager _stateManager;

    int _objectsDestroyed;


    InputAction Attack;

    public override void EnterState(Nightmare_State_Manager stateManager)
    {
        _stateManager = stateManager;

        Attack = GameObject.FindFirstObjectByType<PlayerInput>().actions.FindAction("Attack");

        SmashThings.OnDestroy += CountDestroyedObjects;

        _stateManager.SmashLevel.SetActive(true);
    }

    public override void UpdateState()
    {
        if (Attack.WasPressedThisFrame())
        {
            OnAttack?.Invoke();
        }
    }

    public override void EndState()
    {
        OnFinish?.Invoke();
        
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
