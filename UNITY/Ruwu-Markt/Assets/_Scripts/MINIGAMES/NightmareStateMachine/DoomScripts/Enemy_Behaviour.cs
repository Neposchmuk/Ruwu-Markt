using StarterAssets;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Enemy_Behaviour : MonoBehaviour
{
    public static Action OnEnemyKill;

    public static Action OnKilledPlayer;

    public ParticleSystem KillParticles;

    bool _canCollide;

    NavMeshAgent _agent;

    NPC_State_Manager _stateManager;

    FirstPersonController _player;

    private void Start()
    {
        _agent = GetComponent<NavMeshAgent>();

        _stateManager = GetComponent<NPC_State_Manager>();

        _player = FindFirstObjectByType<FirstPersonController>();

        InvokeRepeating("SetPlayerDestination", 0, 0.5f);

        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") && _canCollide)
        {
            StartCoroutine(CollisionResetTimer());
            OnKilledPlayer?.Invoke();
            Debug.Log("Collided with Player");
        }
    }

    void SetPlayerDestination()
    {
        NavMeshPath path = new NavMeshPath();

        _agent.CalculatePath(_player.transform.position, path);

        _agent.SetPath(path);

        _stateManager.SwitchState(_stateManager.Walking_State);
    }

    public void KillEnemy()
    {
        OnEnemyKill?.Invoke();
        Debug.Log("Killed enemy");
        CancelInvoke("SetPlayerDestination");
        Instantiate(KillParticles, transform.position + new Vector3(0, 1, 0), transform.rotation);
        Destroy(gameObject);
    }

    IEnumerator CollisionResetTimer()
    {
        _canCollide = false;
        yield return new WaitForSeconds(1);
        _canCollide = true;
    }
}
