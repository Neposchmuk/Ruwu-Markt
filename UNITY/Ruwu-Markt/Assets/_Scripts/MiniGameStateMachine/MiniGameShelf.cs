using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class MiniGameShelf : MiniGameBaseState
{
    private int questVariant;

    private int objectsPlaced;

    private int objectsToPlace;

    private InputAction interact;

    private bool isHoldingObject;

    private MiniGameStateManager QuestSource;

    private Quest_Manager QM;

    private Instantiate_Collection IC;

    private string defaultQuestText;


    public override void StartQuest(MiniGameStateManager Quest, int questVariant)
    {
        interact = GameObject.FindFirstObjectByType<PlayerInput>().actions.FindAction("Interact");

        QM = GameObject.FindFirstObjectByType<Quest_Manager>();

        IC = Quest.gameObject.GetComponent<Instantiate_Collection>();

        this.questVariant = questVariant;

        QuestSource = Quest;

        objectsPlaced = 0;      

        defaultQuestText = QM.shelfQuestText.text;

        QM.isDoingQuest = true;

        switch (questVariant)
        {
            case 1:
                objectsToPlace = 30;
                Quest.isDoingQuest = true;
                break;
            case 2:
                objectsToPlace = 10;
                Quest.isDoingQuest = true;
                break;
        }

        Quest.gameObject.GetComponent<Interaction_MenuTest>().ToggleUI(false);
    }

    public override void UpdateQuest()
    {
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0.5f));

        QM.shelfQuestText.text = "Restock the shelves (" + $"{objectsPlaced}" + "/" + $"{objectsToPlace}" + ")";
        //Debug.Log("Changed QuestText");

        if (Physics.Raycast(ray, out RaycastHit hit, 2, QuestSource.interactionLayer))
        {
            if(hit.collider.tag == "ProduceCan" && interact.WasPressedThisFrame())
            {
                Debug.Log(hit.collider.gameObject.layer);
                isHoldingObject = true;
                Debug.Log(isHoldingObject);
            }

            if(hit.collider.tag == "Shelf" && interact.WasPressedThisFrame() && isHoldingObject && objectsPlaced < objectsToPlace)
            {
                objectsPlaced++;
                if(objectsPlaced == objectsToPlace)
                {
                    EndQuest();
                    QuestSource.isDoingQuest = false;
                }
            }
        }
    }

    public override void EndQuest()
    {
        QM.CompleteQuest(0, questVariant, QuestSource.gameObject);
    }

}
