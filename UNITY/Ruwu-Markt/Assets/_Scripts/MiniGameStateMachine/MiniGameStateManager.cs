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

    public int[] sanityChangeValue;

    public int[] jobSecurityChangeValue;

    private Quest_Manager QM;

    MiniGameBaseState currentQuest;
    MiniGameShelf ShelfQuest = new MiniGameShelf();

    private void Start()
    {
        QM = FindFirstObjectByType<Quest_Manager>();
    }

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
        if (QM.isDoingQuest)
        {
            currentQuest.UpdateQuest();
        }
        
    }
}
