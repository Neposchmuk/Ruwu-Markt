using StarterAssets;
using System;
using UnityEngine;

public class Enemy_Spawner : MonoBehaviour
{
    //public static Action OnEnemySpawned;

    public GameObject[] Enemies;

    FirstPersonController _player;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _player = FindFirstObjectByType<FirstPersonController>();

        Nightmare_Doom_State.OnMaxEnemiesSpawned += StopSpawning;

        Nightmare_Doom_State.OnEndState += UnsubscribeEvents;

        InvokeRepeating("SpawnEnemy", 5, 18);
    }

    void SpawnEnemy()
    {
        float distanceToPlayer = (_player.transform.position - transform.position).magnitude;
        if (distanceToPlayer <= 15) return;
        else
        {
            int randomIndex = UnityEngine.Random.Range(0, Enemies.Length);

            Instantiate(Enemies[randomIndex], transform.position, transform.rotation);

            //OnEnemySpawned?.Invoke();
        }
    }

    void StopSpawning()
    {
        CancelInvoke("SpawnEnemy");
    }

    void UnsubscribeEvents()
    {
        Nightmare_Doom_State.OnMaxEnemiesSpawned -= StopSpawning;

        Nightmare_Doom_State.OnEndState -= UnsubscribeEvents;
    }
}
