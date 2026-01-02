using UnityEngine;

//https://youtu.be/gx0Lt4tCDE0
public class GameEventsManager : MonoBehaviour
{
    public static GameEventsManager instance { get; private set; }


    public DialogueEvents dialogueEvents;

    private void Awake()
    {
        if(instance != null) 
        {
            Debug.LogError("Found more than one GameEvents Manager in Scene");
        }
        instance = this;

        dialogueEvents = new DialogueEvents();
    }
}
