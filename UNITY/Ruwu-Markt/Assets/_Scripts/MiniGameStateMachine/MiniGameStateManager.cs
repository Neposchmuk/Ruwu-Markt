using UnityEngine;
using UnityEngine.InputSystem;

public enum QuestType
{
    Shelf,
    Floor,
    Bottles,
    Flowers
}

public class MiniGameStateManager : MonoBehaviour
{
    public bool isDoingQuest;

    public LayerMask interactionLayer;

    public QuestType selectedQuest;

    public int[] sanityChangeValue;

    public int[] jobSecurityChangeValue;

    private Quest_Manager QM;

    private InputAction interact;

    private InputAction attack;

    MiniGameBaseState currentQuest;
    MiniGameShelf ShelfQuest = new MiniGameShelf();
    MiniGameWipeFloor FloorQuest = new MiniGameWipeFloor();

    private void Start()
    {
        QM = FindFirstObjectByType<Quest_Manager>();

        attack = GameObject.Find("PlayerCapsule").GetComponent<PlayerInput>().actions.FindAction("Attack");

        interact = GameObject.FindFirstObjectByType<PlayerInput>().actions.FindAction("Interact");
    }

    public void StartQuest(int questVariant)
    {

        switch (selectedQuest)
        {
            case QuestType.Shelf:
                currentQuest = ShelfQuest;
                currentQuest.StartQuest(this, questVariant);
                QM.SetQuestText(0);
                break;
            case QuestType.Floor:
                currentQuest = FloorQuest;
                currentQuest.StartQuest(this, questVariant);
                QM.SetQuestText(1);
                break;
            
        }

        
    }

    private void Update()
    {
        if (QM.isDoingQuest)
        {
            if (interact.WasPressedThisFrame())
            {
                currentQuest.Interact();
            }

            if (attack.IsPressed())
            {
                currentQuest.Attack();
            }
            /*Debug.Log(currentQuest);
            currentQuest.UpdateQuest();*/
        }
        
    }
}
