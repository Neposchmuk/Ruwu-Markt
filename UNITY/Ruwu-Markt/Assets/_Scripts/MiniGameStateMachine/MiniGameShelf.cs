using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class MiniGameShelf : MiniGameBaseState
{
    private int questVariant;

    private int objectsPlaced;

    private int objectsToPlace;

    private InputAction interact;

    private InputAction attack;

    private bool isHoldingObject;

    private MiniGameStateManager QuestSource;

    private Quest_Manager QM;

    private Instantiate_Collection IC;

    private string defaultQuestText;


    public override void StartQuest(MiniGameStateManager Quest, int questVariant)
    {
        interact = GameObject.FindFirstObjectByType<PlayerInput>().actions.FindAction("Interact");

        attack = GameObject.Find("PlayerCapsule").GetComponent<PlayerInput>().actions.FindAction("Attack");

        QM = GameObject.FindFirstObjectByType<Quest_Manager>();

        IC = Quest.gameObject.GetComponent<Instantiate_Collection>();

        this.questVariant = questVariant;

        QuestSource = Quest;

        objectsPlaced = 0;      

        defaultQuestText = QM.shelfQuestText.text;

        QM.isDoingQuest = true;

        Quest.gameObject.GetComponent<Interaction_MenuTest>().ToggleUI(false);
    }

    public override void UpdateQuest()
    {
        switch (questVariant)
        {
            case 1:
                objectsToPlace = 30;
                PlaceQuest();
                break;
            case 2:
                objectsToPlace = 10;
                PlaceQuest();
                break;
            case 3:
                //Needs to be changed to require Animation first
                EndQuest();
                break;
            case 4:
                PourQuest();
                break;
        }
    }

    public override void EndQuest()
    {
        QM.CompleteQuest(0, questVariant -1, QuestSource.gameObject);
    }

    private void PlaceQuest()
    {
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0.5f));

        QM.shelfQuestText.text = "Restock the shelves (" + $"{objectsPlaced}" + "/" + $"{objectsToPlace}" + ")";
        //Debug.Log("Changed QuestText");

        if (Physics.Raycast(ray, out RaycastHit hit, 2, QuestSource.interactionLayer))
        {
            if (hit.collider.tag == "ProduceCan" && interact.WasPressedThisFrame())
            {
                GameObject.FindFirstObjectByType<Hand_Raycast>().PickUpObject(0);
                Debug.Log(hit.collider.gameObject.layer);
                isHoldingObject = true;
                Debug.Log(isHoldingObject);
                GameObject.Destroy(hit.collider.gameObject);
            }

            if (hit.collider.tag == "Shelf" && interact.WasPressedThisFrame() && isHoldingObject && objectsPlaced < objectsToPlace)
            {
                GameObject.FindFirstObjectByType<Hand_Raycast>().Place(hit);
                objectsPlaced++;
                if (objectsPlaced == objectsToPlace)
                {
                    EndQuest();
                    //QuestSource.isDoingQuest = false;
                }
            }
        }
    }

    private void PourQuest()
    {
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0.5f));

        QM.shelfQuestText.text = "Let it flow!!!";

        if (Physics.Raycast(ray, out RaycastHit hit, 2, QuestSource.interactionLayer))
        {
            if (hit.collider.tag == "ProduceCan" && interact.WasPressedThisFrame())
            {
                GameObject.FindFirstObjectByType<Hand_Raycast>().PickUpObject(0);
                Debug.Log(hit.collider.gameObject.layer);
                isHoldingObject = true;
                Debug.Log(isHoldingObject);
                GameObject.Destroy(hit.collider.gameObject);
            }           
        }

        if (isHoldingObject && attack.IsPressed())
        {
            GameObject.FindFirstObjectByType<Hand_Raycast>().Pour();
            if (GameObject.FindFirstObjectByType<Hand_Raycast>().Pour() <= 0)
            {
                EndQuest();
            }
        }
    }

}
