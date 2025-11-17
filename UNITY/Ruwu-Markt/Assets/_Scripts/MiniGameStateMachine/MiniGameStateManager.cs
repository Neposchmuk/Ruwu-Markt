using UnityEngine;

public class MiniGameStateManager : MonoBehaviour
{
    public bool isDoingQuest;

    public LayerMask interactionLayer;

    public enum QuestType
    {
        Shelf,
        Floor,
        Bottles,
        Flowers
    }

    public QuestType selectedQuest;

    public GameObject IU;

    MiniGameBaseState currentQuest;
    MiniGameShelf ShelfQuest = new MiniGameShelf();

    public void StartQuest(int questVariant)
    {
        switch (selectedQuest)
        {
            case QuestType.Shelf:
                currentQuest = ShelfQuest;
                currentQuest.StartQuest(this, questVariant);
                break;
        }
        
    }

    private void Update()
    {
        if (isDoingQuest)
        {
            currentQuest.UpdateQuest();
        }
        
    }
}
