using UnityEngine;

public class RaiseQuestmarker : MonoBehaviour
{
    private GameObject player;
    
    private float height;

    private void OnEnable()
    {
        GameEventsManager.instance.gameEvents.onSendPlayerObject += SetPlayerObject;
    }
    private void OnDisable()
    {
        GameEventsManager.instance.gameEvents.onSendPlayerObject -= SetPlayerObject;
    }

    void Start()
    {
        GameEventsManager.instance.gameEvents.RequestPlayerObject(gameObject);
    }

    void Update()
    {
        ChangeHeight();
    }

    void SetPlayerObject(GameObject requester, GameObject playerObject)
    {
        if(requester != this.gameObject) return;

        player = playerObject;
    }

    float ChangeHeight()
    {
        Vector3 playerDistance = transform.position - player.transform.position;

        height = Mathf.Clamp(playerDistance.magnitude - 2, 2, 20);

        transform.localPosition = new Vector3(transform.localPosition.x, height, transform.localPosition.z);

        return height;

        
    }
}
