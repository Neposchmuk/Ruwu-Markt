using UnityEngine;
using System;
using UnityEngine.InputSystem;
using StarterAssets;

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

        _stateManager.DoomLevel.SetActive(true);

        Enemy_Spawner.OnEnemySpawned += CountEnemies;

        Enemy_Behaviour.OnEnemyKill += CountKilled;

        Enemy_Behaviour.OnKilledPlayer += HitPlayer;
    }

    public override void UpdateState()
    {
        if (_attack.WasPressedThisFrame())
        {
            _gun.Fire();
        }

        if (_melee.WasPressedThisFrame())
        {
            _gun.gameObject.SetActive(false);
            _batParent.StartAnimatorCoroutine();
            _gun.gameObject.SetActive(true);
        }

        if (_reload.WasPressedThisFrame())
        {
            _gun.StartReload();
        }
    }

    public override void EndState()
    {
        OnEndState?.Invoke();

        Enemy_Spawner.OnEnemySpawned -= CountEnemies;

        Enemy_Behaviour.OnEnemyKill -= CountKilled;

        Enemy_Behaviour.OnKilledPlayer -= HitPlayer;

        _stateManager.EndNight(true, 10);
    }

    void CountEnemies()
    {
        _enemiesSpawned++;

        if(_enemiesSpawned >= _enemiesToKill)
        {
            OnMaxEnemiesSpawned?.Invoke();
            _enemiesToKill = _enemiesSpawned;
        }
    }

    void CountKilled()
    {
        _enemiesKilled++;
        Debug.Log("Enemies Killed: " + _enemiesKilled + " | Enemies To Kill: " + _enemiesToKill);
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

            Enemy_Spawner.OnEnemySpawned -= CountEnemies;

            Enemy_Behaviour.OnEnemyKill -= CountKilled;

            Enemy_Behaviour.OnKilledPlayer -= HitPlayer;

            _stateManager.EndNight(false, -20);
        }   
    }
}
