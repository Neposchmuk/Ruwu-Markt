using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering;

public class NPC_Boss_Behaviour : MonoBehaviour
{
    [SerializeField] NavMeshAgent agent;

    [SerializeField] NPC_State_Manager stateManager;

    [SerializeField] Animator animator;

    [SerializeField] BoxCollider walkableArea;

    [SerializeField] GameObject chair;


    private enum BehaviourType
    {
        SIT,
        WALK
    }

    private BehaviourType currentBehaviour;

    private bool patrolling;

    private bool waitingForPath;

    private void OnEnable()
    {
        GameEventsManager.instance.npcEvents.onFacePlayer += TalkBehaviour;
        GameEventsManager.instance.npcEvents.onResumeBehaviour += ResumePreviousBehaviour;
    }
    private void OnDisable()
    {
        GameEventsManager.instance.npcEvents.onFacePlayer -= TalkBehaviour;
        GameEventsManager.instance.npcEvents.onResumeBehaviour -= ResumePreviousBehaviour;
    }

    private void Start()
    {
        RandomizeBehaviour();
    }

    private void Update()
    {
        if(!waitingForPath && patrolling && agent.remainingDistance <= 0.1f)
        {
            StartCoroutine(DelayNewPath());
        }
    }

    private void RandomizeBehaviour()
    {
        int randomBehaviour = Random.Range(2,3);

        animator.ResetTrigger("Idle");

        switch (randomBehaviour)
        {
            case 1:
                SitBehaviour();
                currentBehaviour = BehaviourType.SIT;
                break;
            case 2:
                WalkBehaviour();
                currentBehaviour = BehaviourType.WALK;
                break;
        }
        
    }

    private void SitBehaviour()
    {
        transform.position = chair.transform.position;

        animator.SetTrigger("Sit");
    }

    private void WalkBehaviour()
    {
        patrolling = true;

        RandomizeDestination();
    }

    private void RandomizeDestination()
    {
        NavMeshPath path = new NavMeshPath();

        Vector3 randomPosition = walkableArea.transform.position + new Vector3(Random.Range(walkableArea.center.x - walkableArea.size.x/2, walkableArea.center.x + walkableArea.size.x / 2), 0, Random.Range(walkableArea.center.z - walkableArea.size.z /2, walkableArea.center.z + walkableArea.size.z / 2));

        agent.CalculatePath(randomPosition, path);

        agent.SetPath(path);

        stateManager.SwitchState(stateManager.Walking_State);
    }

    private IEnumerator DelayNewPath()
    {
        waitingForPath = true;

        yield return new WaitForSeconds(5);

        RandomizeDestination();

        waitingForPath = false;
    }

    private void TalkBehaviour(GameObject agent, Vector3 playerPosition)
    {
        if(agent != this.gameObject) return;

        switch (currentBehaviour)
        {
            case BehaviourType.SIT:
                animator.SetTrigger("Idle");
                break;
            case BehaviourType.WALK:
                if(waitingForPath) StopCoroutine(DelayNewPath());
                patrolling = false;
                stateManager.SwitchState(stateManager.Idle_State);
                break;
        }
    }

    private void ResumePreviousBehaviour(GameObject agent)
    {
        if(agent != this.gameObject) return;

        animator.ResetTrigger("Idle");

        switch (currentBehaviour)
        {
            case BehaviourType.SIT:
                GameEventsManager.instance.npcEvents.ResetFace(gameObject);
                SitBehaviour();
                break;
            case BehaviourType.WALK:
                GameEventsManager.instance.npcEvents.ResetFace(gameObject);
                WalkBehaviour();
                break;
        }
    }
}
