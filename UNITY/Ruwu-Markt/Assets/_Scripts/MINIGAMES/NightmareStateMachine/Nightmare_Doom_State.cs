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

    Player_Gun _gun;

    SmashThings_Animation _batParent;

    PlayerInput _playerInput;

    InputAction _attack;

    InputAction _melee;

    InputAction _reload;

    TMP_Text _enemyCounter;

    int _enemiesSpawned;

    int _enemiesToKill;

    int _enemiesKilled;

    int _playerHealth;

    public override void EnterState(Nightmare_State_Manager stateManager)
    {
        _stateManager = stateManager;

        _gun = GameObject.FindFirstObjectByType<Player_Gun>();

        _batParent = GameObject.FindFirstObjectByType<SmashThings_Animation>();

        _playerInput = GameObject.FindFirstObjectByType<PlayerInput>();

        _enemyCounter = GameObject.FindGameObjectWithTag("DoomEnemyCounter").GetComponent<TMP_Text>();

        _player = _playerInput.gameObject.GetComponent<FirstPersonController>();

        _attack = _playerInput.actions.FindAction("Attack");

        _melee = _playerInput.actions.FindAction("Flash");

        _reload = _playerInput.actions.FindAction("Reload");

        _gun.gameObject.SetActive(true);

        _enemiesToKill = 20;

        _playerHealth = 3;

        _player.MoveSpeed = 10;

        _player.SprintSpeed = 10;

        _gun.gameObject.SetActive(true);

        GameEventsManager.instance.questEvents.onHitEnemy += CountKilled;

        Enemy_Behaviour.OnKilledPlayer += HitPlayer;
    }

    public override void UpdateState()
    {
        if (_attack.WasPressedThisDynamicUpdate())
        {
            _gun.Fire();
        }

        if (_melee.WasPressedThisDynamicUpdate())
        {
            _gun.gameObject.SetActive(false);
            _batParent.StartAnimatorCoroutine();
            _gun.gameObject.SetActive(true);
        }

        if (_reload.WasPressedThisDynamicUpdate())
        {
            _gun.StartReload();
        }
    }

    public override void EndState()
    {
        OnEndState?.Invoke();

        Enemy_Behaviour.OnKilledPlayer -= HitPlayer;

        GameEventsManager.instance.questEvents.onHitEnemy -= CountKilled;

        _stateManager.EndNight(true, 10);
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
        _playerHealth--;
        Debug.Log("Player HP: " + _playerHealth);
        if(_playerHealth <= 0)
        {
            OnEndState?.Invoke();

            GameEventsManager.instance.questEvents.onHitEnemy -= CountKilled;

            Enemy_Behaviour.OnKilledPlayer -= HitPlayer;

            _stateManager.EndNight(false, -20);
        }   
    }
}
