using System.Collections;
using UnityEngine;

public class MiniGameWaterPlants : MiniGameBaseState
{
    private Quest_Manager QM;

    private MiniGame_Caller QuestSource;

    private GameObject[] FlowerStaions;

    private GameObject objectHeld;

    private int questVariant;

    private int flowersToWater;

    private int flowersWatered;

    private bool isHoldingCan;

    private int questStage;
    public override void StartQuest(MiniGame_Caller Quest, int questVariant)
    {
        QM = GameObject.FindFirstObjectByType<Quest_Manager>();

        QuestSource = Quest;

        this.questVariant = questVariant;

        Quest.gameObject.GetComponent<Interaction_MenuTest>().ToggleUI(false);

        questStage = 0;

        switch(questVariant)
        {
            case 1:
                InitiateQuest();
                break;
            case 2:
                EndQuest();
                break;

        }
    }

    public override void InitiateQuest()
    {
        questStage = 1;

        flowersWatered = 0;

        flowersToWater = 3;

        QM.isDoingQuest = true;

        QM.flowersQuestText.text = "Grab the Watering Can";
    }

    public override void UpdateQuest()
    {
        switch (questStage)
        {
            case 2:
                QM.flowersQuestText.text = "Fill the Can at the sink";
                break;
            case 3:
                QM.flowersQuestText.text = "Water the plants (" + $"{flowersWatered}" + "/" + $"{flowersToWater}" + ")";
                break;
        }
    }

    public override void EndQuest()
    {
        QM.CompleteQuest(3, questVariant - 1, QuestSource.gameObject);
    }

    public override void Interact()
    {
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0.5f));

        if (Physics.Raycast(ray, out RaycastHit hit, 2, QuestSource.interactionLayer))
        {
            if(hit.collider.tag == "WateringCan")
            {
                objectHeld = GameObject.FindFirstObjectByType<Hand_Actions>().PickUpObject(9);
                isHoldingCan = true;
                GameObject.Destroy(hit.collider.gameObject);
                questStage = 2;
                UpdateQuest();
            }

            if(hit.collider.tag == "FlowersQuest" && isHoldingCan)
            {
                questStage = 3;
                UpdateQuest();
            }
        }
    }

    public override void HoldingAttack(bool buttonIsPressed)
    {
        throw new System.NotImplementedException();
    }
}
