using Unity.VisualScripting;
using UnityEngine;

public class EnterDialogue : MonoBehaviour
{
    [Header("Dialogue Knot")]
    [SerializeField] private string dialogueKnotName;

    public void SendDialogueEvent()
    {
        if (dialogueKnotName.Equals(""))
        {
            GameEventsManager.instance.dialogueEvents.EnterDialogue(dialogueKnotName);
        }
    }
}
