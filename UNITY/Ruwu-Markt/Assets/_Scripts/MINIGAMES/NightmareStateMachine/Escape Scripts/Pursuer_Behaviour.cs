using StarterAssets;
using System.Drawing;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Pursuer_Behaviour : MonoBehaviour
{
    public LayerMask Layers;

    public float SprintSpeed;

    public float WalkingSpeed;

    Nightmare_State_Manager _nightmareStateManager;

    NPC_State_Manager _npcStateManager;

    NavMeshAgent _agent;

    GameObject _player;

    BoxCollider _marketFloor;

    bool _patrolling;

    bool _patrolToPlayer;

    bool _seesPlayer;

    bool _gameRunning;

    Ray ray;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _npcStateManager = GetComponent<NPC_State_Manager>();

        _nightmareStateManager = GameObject.FindFirstObjectByType<Nightmare_State_Manager>();

        _agent = GetComponent<NavMeshAgent>();

        _player = GameObject.FindGameObjectWithTag("Player");

        _marketFloor = GameObject.Find("MarketFloor").GetComponent<BoxCollider>();

        _patrolToPlayer = false;

        _gameRunning = true;

        InvokeRepeating("PatrolBehaviour", 0, 15);
    }

    // Update is called once per frame
    void Update()
    {
        ray = new Ray(transform.position, transform.forward);

        if (Physics.Raycast(ray, out RaycastHit hit, 15, Layers))
        {
            if (hit.collider.CompareTag("Player") || hit.collider.CompareTag("PlayerFlashlight") && !_seesPlayer)
            {
                _seesPlayer = true;

                CancelInvoke("PatrolBehaviour");

                Invoke("ChaseBehaviour", 0);
            }

            Debug.Log(hit);
        }

        if(_patrolling && _agent.remainingDistance <= 0.1f)
        {
            _patrolToPlayer = false;
            PatrolBehaviour();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("collided");

        if (collision.gameObject.CompareTag("Player") && _gameRunning)
        {
            GameEventsManager.instance.soundEvents.TriggerSound(SoundType.PLAYER_HURT);
            //trigger stab animation
            _gameRunning = false;
            _nightmareStateManager.EndNight(false, -30);
        }
    }

    void PatrolBehaviour()
    {
        _agent.speed = WalkingSpeed;

        _patrolling = true;

        if(!_patrolToPlayer)
        {
            SetRandomDestination();
        }
        else if(_patrolToPlayer)
        {
            SetPlayerDestination();
        }

        _npcStateManager.SwitchState(_npcStateManager.Walking_State);

        _patrolToPlayer = !_patrolToPlayer;
    }

    void ChaseBehaviour()
    {
        _agent.speed = SprintSpeed;

        _patrolling = false;

        InvokeRepeating("SetPlayerDestination", 0, 2);
        InvokeRepeating("CheckSeesPlayer", 9, 9);
    }

    void CheckSeesPlayer()
    {
        if (Physics.Raycast(ray, out RaycastHit hit, 15))
        {
            if (hit.collider.CompareTag("Player") || hit.collider.CompareTag("PlayerFlashlight") && _seesPlayer)
            {
                return;
            }
            else
            {
                _patrolToPlayer = false;

                CancelInvoke("SetPlayerDestination");
                CancelInvoke("CheckSeesPlayer");
                Invoke("PatrolBehaviour", 0);

                _seesPlayer = false;
            }
        }
    }

    //https://discussions.unity.com/t/randomly-spawn-position-of-object-within-a-area/60537/3
    void SetRandomDestination()
    {
        NavMeshPath path = new NavMeshPath();

        _agent.CalculatePath(_marketFloor.center + new Vector3(Random.Range(_marketFloor.center.x - _marketFloor.size.x/2, _marketFloor.center.x + _marketFloor.size.x / 2), _marketFloor.transform.position.y, Random.Range(_marketFloor.center.z - _marketFloor.size.z /2, _marketFloor.center.z + _marketFloor.size.z / 2)), path);

        _agent.SetPath(path);

        Debug.Log("has path");
    }

    void SetPlayerDestination()
    {
        NavMeshPath path = new NavMeshPath();

        _agent.CalculatePath(_player.transform.position, path);

        _agent.SetPath(path);
    }
}
