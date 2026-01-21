using UnityEngine;

public class FaceDirection : MonoBehaviour
{

    [SerializeField] private GameObject agent;

    private GameObject currentTrigger;

    private void OnEnable()
    {
        GameEventsManager.instance.npcEvents.onResetFace += ResetFace;
        GameEventsManager.instance.npcEvents.onFacePlayer += FacePlayer;
        GameEventsManager.instance.npcEvents.onSetFaceTrigger += SetCurrentTrigger;
        GameEventsManager.instance.npcEvents.onFaceTrigger += FaceInDirection;
    }

    private void OnDisable()
    {
        GameEventsManager.instance.npcEvents.onResetFace -= ResetFace;
        GameEventsManager.instance.npcEvents.onFacePlayer -= FacePlayer;
        GameEventsManager.instance.npcEvents.onSetFaceTrigger -= SetCurrentTrigger;
        GameEventsManager.instance.npcEvents.onFaceTrigger -= FaceInDirection;
    }

    private void SetCurrentTrigger(GameObject agent, GameObject trigger)
    {
        if(agent != this.agent) return;

        currentTrigger = trigger;
    }

    private void FaceInDirection(GameObject agent)
    {
        if(agent != this.agent || currentTrigger == null) return;

        Quaternion LookDirection = Quaternion.Euler(new Vector3(transform.rotation.x, currentTrigger.transform.localEulerAngles.y, transform.rotation.z));

        Debug.Log(LookDirection);

        transform.rotation = LookDirection;
    }

    private void FacePlayer(GameObject agent, Vector3 playerPosition)
    {
        if(agent != this.agent) return;

        Vector3 PlayerDirection = new Vector3(playerPosition.x, transform.position.y, playerPosition.z);

        transform.LookAt(PlayerDirection);
    }

    private void ResetFace(GameObject agent)
    {
        if(agent != this.agent) return;

        transform.localEulerAngles = Vector3.zero;
    }
}
