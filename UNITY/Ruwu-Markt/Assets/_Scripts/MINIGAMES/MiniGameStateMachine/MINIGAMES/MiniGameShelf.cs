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

    private MiniGame_Caller QuestSource;

    private Quest_Manager QM;

    private Hand_Actions HA;

    private Instantiate_Collection IC;

    private string defaultQuestText;


    public override void StartQuest(MiniGame_Caller Quest, int questVariant)
    {
        QM = GameObject.FindFirstObjectByType<Quest_Manager>();

        HA = GameObject.FindFirstObjectByType<Hand_Actions>();

        this.questVariant = questVariant;

        QuestSource = Quest;

        Quest.gameObject.GetComponent<Interaction_MenuTest>().ToggleUI(false);

        switch (questVariant)
        {
            case 1:
                objectsToPlace = 30;
                InitiateQuest();
                break;
            case 2:
                objectsToPlace = 10;
                InitiateQuest();
                break;
            case 3:
                //Needs to be changed to require Animation first
                InitiateQuest();
                break;
            case 4:
                InitiateQuest();
                break;
        }
    }

    public override void InitiateQuest()
    {
        objectsPlaced = 0;

        QM.isDoingQuest = true;

        GameEventsManager.instance.questEvents.UpdateQuestText("Grab the Products");

        QuestSource.QuestMarkerBig.SetActive(false);

        QuestSource.QuestMarkerSmall.SetActive(true);
    }

    public override void UpdateQuest()
    {
        switch (questVariant)
        {
            case 1:
                GameEventsManager.instance.questEvents.UpdateQuestText("Restock the shelves (" + $"{objectsPlaced}" + "/" + $"{objectsToPlace}" + ")");
                break;
            case 2:
                GameEventsManager.instance.questEvents.UpdateQuestText("Restock the shelves (" + $"{objectsPlaced}" + "/" + $"{objectsToPlace}" + ")");
                break;
            case 3:
                GameEventsManager.instance.questEvents.UpdateQuestText("Take a sip");
                break;
            case 4:
                GameEventsManager.instance.questEvents.UpdateQuestText("Let it flow!!!");
                break;
        }
        
    }

    public override void EndQuest()
    {
        QuestSource.QuestMarkerBig.SetActive(false);
        GameObject.FindFirstObjectByType<Hand_Actions>().DestroyObjectInHand();
        QM.CompleteQuest(0, questVariant -1, QuestSource.gameObject);
    }

    public override void Interact()
    {
        Debug.Log("Called Interact");

        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0.5f));

        if (Physics.Raycast(ray, out RaycastHit hit, 2, QuestSource.interactionLayer))
        {
            if (hit.collider.tag == "ProduceCan")
            {
                GameObject.FindFirstObjectByType<Hand_Actions>().PickUpObject(0);
                Debug.Log(hit.collider.gameObject.layer);
                isHoldingObject = true;
                Debug.Log(isHoldingObject);
                GameObject.Destroy(hit.collider.gameObject);
                UpdateQuest();
                QuestSource.QuestMarkerBig.SetActive(true);
            }



            if (hit.collider.tag == "Shelf" && isHoldingObject && objectsPlaced < objectsToPlace && (questVariant == 1 || questVariant == 2))
            {
                GameObject.FindFirstObjectByType<Hand_Actions>().Place(hit);
                objectsPlaced++;
                UpdateQuest();
                if (objectsPlaced == objectsToPlace)
                {
                    EndQuest();
                }
            }
        }
    }

    public override void HoldingAttack(bool buttonIsPressed)
    {
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0.5f));


        if (isHoldingObject && buttonIsPressed && questVariant == 4)
        {
            HA.Pour();
            if (HA.Pour() <= 0)
            {
                EndQuest();
            }
        }

        if (isHoldingObject && buttonIsPressed && questVariant == 3)
        {
            //Run animation -> Reduces counter, if counter == 0 EndQuest
            EndQuest();
        }
    }
}
