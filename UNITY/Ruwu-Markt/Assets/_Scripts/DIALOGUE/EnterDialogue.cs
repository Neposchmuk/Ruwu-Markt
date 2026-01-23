using Unity.VisualScripting;
using UnityEngine;

public class EnterDialogue : MonoBehaviour
{
    [Header("Dialogue Knot")]
    [SerializeField] private string dialogueKnotName;

    public void OnEnable()
    {
        GameEventsManager.instance.dialogueEvents.onDialogueFinished += SendFinishedEvent;
    }

    public void OnDisable()
    {
        GameEventsManager.instance.dialogueEvents.onDialogueFinished -= SendFinishedEvent;
    }

    public void SendDialogueEvent()
    {
        if (!dialogueKnotName.Equals(""))
        {
            Debug.Log("Sent Dialogue Event");
            GameEventsManager.instance.npcEvents.PingPlayerPosition(gameObject);
            GameEventsManager.instance.dialogueEvents.EnterDialogue(dialogueKnotName);
        }
    }

    private void SendFinishedEvent()
    {
        GameEventsManager.instance.npcEvents.ResumeBehaviour(gameObject);
    }
}
