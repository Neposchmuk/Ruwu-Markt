using NUnit.Framework;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;

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

    //public LayerMask interactionLayer;

    //public QuestType selectedQuest;

    //public List<int> sanityChangeValue = new List<int>();

    //public List<int> jobSecurityChangeValue = new List<int>();

    private Quest_Manager QM;

    private InputAction interact;

    private InputAction attack;

    MiniGameBaseState currentQuest;
    MiniGameShelf ShelfQuest = new MiniGameShelf();
    MiniGameWipeFloor FloorQuest = new MiniGameWipeFloor();
    MiniGamePfand BottlesQuest = new MiniGamePfand();
    MiniGameWaterPlants FlowersQuest = new MiniGameWaterPlants();

    private void Start()
    {
        QM = FindFirstObjectByType<Quest_Manager>();

        attack = GameObject.FindFirstObjectByType<PlayerInput>().actions.FindAction("Attack");

        interact = GameObject.FindFirstObjectByType<PlayerInput>().actions.FindAction("Interact");
    }

    public void StartQuest(MiniGame_Caller MGC, QuestType selectedQuest, int questVariant, List<int> sanityChanges, List<int> jobChanges)
    {
        //sanityChangeValue.AddRange(sanityChanges);
        //jobSecurityChangeValue.AddRange(jobChanges);

        switch (selectedQuest)
        {
            case QuestType.Shelf:
                currentQuest = ShelfQuest;
                currentQuest.StartQuest(MGC, questVariant);         
                break;
            case QuestType.Floor:
                currentQuest = FloorQuest;
                currentQuest.StartQuest(MGC, questVariant);              
                break;
            case QuestType.Bottles:
                currentQuest = BottlesQuest;
                currentQuest.StartQuest(MGC, questVariant);
                break;
            case QuestType.Flowers:
                currentQuest = FlowersQuest;
                currentQuest.StartQuest(MGC, questVariant);
                break;
            
        }

        
    }

    private void Update()
    {
        if (QM.isDoingQuest)
        {
            if (interact.WasPressedThisDynamicUpdate())
            {
                currentQuest.Interact();
            }

            if (attack.IsPressed())
            {
                currentQuest.HoldingAttack(true);
            }
            else if (attack.WasReleasedThisDynamicUpdate())
            {
                currentQuest.HoldingAttack(false);
            }
            /*Debug.Log(currentQuest);
            currentQuest.UpdateQuest();*/
        }
        
    }
}
