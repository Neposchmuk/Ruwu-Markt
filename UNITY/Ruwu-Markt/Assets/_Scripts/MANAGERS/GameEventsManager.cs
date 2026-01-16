using UnityEngine;

//https://youtu.be/gx0Lt4tCDE0
public class GameEventsManager : MonoBehaviour
{
    public static GameEventsManager instance { get; private set; }


    public DialogueEvents dialogueEvents;

    public PlayerEvents playerEvents;

    public QuestEvents questEvents;

    public GameEvents gameEvents;

    public CheckoutEvents checkoutEvents;

    private void Awake()
    {
        if(instance != null) 
        {
            Debug.LogError("Found more than one GameEvents Manager in Scene");
        }
        instance = this;

        dialogueEvents = new DialogueEvents();

        playerEvents = new PlayerEvents();

        questEvents = new QuestEvents();

        gameEvents = new GameEvents();

        checkoutEvents = new CheckoutEvents();
    }
}
