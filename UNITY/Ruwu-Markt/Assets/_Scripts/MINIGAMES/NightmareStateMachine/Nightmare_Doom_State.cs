using UnityEngine;
using System;
using UnityEngine.InputSystem;
using StarterAssets;
using TMPro;

public class Nightmare_Doom_State : NightmareBaseState
{
    public static Action OnMaxEnemiesSpawned;

    public static Action OnEndState;

    FirstPersonController _player;

    Nightmare_State_Manager _stateManager;

    PlayerInput _playerInput;

    TMP_Text _enemyCounter;

    int _enemiesSpawned;

    int _enemiesToKill;

    int _enemiesKilled;

    int _playerHealth;

    public override void EnterState(Nightmare_State_Manager stateManager)
    {
        SubscribeEvents();

        GameEventsManager.instance.playerEvents.ChangeInputEventContext(InputEventContext.NIGHTMARE_DOOM);

        _stateManager = stateManager;

        _playerInput = GameObject.FindFirstObjectByType<PlayerInput>();

        _enemyCounter = GameObject.FindGameObjectWithTag("DoomEnemyCounter").GetComponent<TMP_Text>();

        _player = _playerInput.gameObject.GetComponent<FirstPersonController>();

        _enemiesToKill = 20;

        _playerHealth = 3;

        _player.MoveSpeed = 10;

        _player.SprintSpeed = 10;
    }

    public override void UpdateState()
    {
        
    }

    public override void EndState()
    {
        OnEndState?.Invoke();

        UnsubscribeEvents();

        _stateManager.EndNight(true, 10);

        GameEventsManager.instance.playerEvents.ChangeInputEventContext(InputEventContext.DEFAULT);
    }

    void CountKilled(GameObject enemy)
    {
        _enemiesKilled++;
        _enemyCounter.text = $"{_enemiesKilled}" + "/" + $"{_enemiesToKill}";
        if(_enemiesKilled == _enemiesToKill)
        {
            EndState();
        }
    }

    void HitPlayer()
    {
        if(_stateManager.playerInvincible) return;

        GameEventsManager.instance.soundEvents.TriggerSound(SoundType.PLAYER_HURT);

        _playerHealth--;
        Debug.Log("Player HP: " + _playerHealth);
        if(_playerHealth <= 0)
        {
            OnEndState?.Invoke();

            UnsubscribeEvents();

            _stateManager.EndNight(false, -20);

            GameEventsManager.instance.playerEvents.ChangeInputEventContext(InputEventContext.DEFAULT);
        }   
    }

    private void SubscribeEvents()
    {
        GameEventsManager.instance.questEvents.onHitEnemy += CountKilled;

        Enemy_Behaviour.OnKilledPlayer += HitPlayer;
    }

    private void UnsubscribeEvents()
    {
        GameEventsManager.instance.questEvents.onHitEnemy -= CountKilled;

        Enemy_Behaviour.OnKilledPlayer -= HitPlayer;
    }
}
