using UnityEngine;

public class FaceDirection : MonoBehaviour
{
    private void OnEnable()
    {
        GameEventsManager.instance.npcEvents.onResetFace += ResetFace;
        GameEventsManager.instance.npcEvents.onFacePlayer += FacePlayer;
        GameEventsManager.instance.npcEvents.onFaceDirection += FaceInDirection;
    }

    private void OnDisable()
    {
        GameEventsManager.instance.npcEvents.onResetFace -= ResetFace;
        GameEventsManager.instance.npcEvents.onFacePlayer -= FacePlayer;
        GameEventsManager.instance.npcEvents.onFaceDirection -= FaceInDirection;
    }

    private void FaceInDirection(GameObject agent, Quaternion direction)
    {
        if(agent != this.gameObject) return;

        Vector3 LookDirection = new Vector3(direction.x, transform.position.y, direction.z);

        transform.LookAt(LookDirection);
    }

    private void FacePlayer(GameObject agent, Vector3 playerPosition)
    {
        if(agent != this.gameObject) return;

        Vector3 PlayerDirection = new Vector3(playerPosition.x, transform.position.y, playerPosition.z);

        transform.LookAt(PlayerDirection);
    }

    private void ResetFace(GameObject agent)
    {
        if(agent != this.gameObject) return;

        transform.rotation = Quaternion.Euler(Vector3.zero);
    }
}
