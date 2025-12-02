using JetBrains.Annotations;
using System.Collections;
using UnityEngine;

public class actionsScript : MonoBehaviour
{
    //Controls which set of actions is triggered
    public enum QuestType
    {
        Shelf,
        Floor,
        Bottles
    }

    public QuestType Quest;

    public int[] sanityChange;

    public int[] jobSecChange;

    private Sanity_Manager SM;

    private Interaction_MenuTest IM;

    private Hand_Actions handRC;

    private Quest_Manager QM;

    private int actionSelector;

    private int objectsToPlace;

    private int objectsPlaced;

    private int currentQuest;

    private int currentQuestVersion;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SM = GameObject.Find("Sanity_Manager").GetComponent<Sanity_Manager>();
        QM = GameObject.Find("Quest_Manager").GetComponent<Quest_Manager>();
        IM = GetComponent<Interaction_MenuTest>();
        handRC = GameObject.Find("Hand").GetComponent<Hand_Actions>();       

        switch (Quest)
        {
            case QuestType.Shelf:
                actionSelector = 0;
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //Starts Quests depending on selected variant
    public void TriggerAction(int i)
    {
        currentQuest = actionSelector;
        currentQuestVersion = i;

        switch (Quest)
        {
            case QuestType.Shelf:
                switch (i)
                {
                    case 0:
                        ShelfQuest(i);
                        IM.ToggleUI(false);
                        //LockCursor();
                        //IM.interactionUI.SetActive(false);
                        break;
                    case 1:
                        ShelfQuest(i);
                        IM.ToggleUI(false);
                        //LockCursor();
                        //IM.interactionUI.SetActive(false);
                        break;
                    case 2:
                        ShelfQuest(i);
                        IM.ToggleUI(false);
                        //LockCursor();
                        //IM.interactionUI.SetActive(false);
                        break;
                    case 3:
                        ShelfQuest(i);
                        IM.ToggleUI(false);
                        //LockCursor();
                        //IM.interactionUI.SetActive(false);
                        break;
                }
                break;
        }        
    }
    
    //Updates amount of objects placed for Instantiate Quests
    public void PlaceObject()
    {
        objectsPlaced++;
        QM.stepsDone = objectsPlaced;
        QM.UpdateQuest(actionSelector, currentQuestVersion);

        if(objectsPlaced == objectsToPlace)
        {
            QM.CompleteQuest(currentQuest, currentQuestVersion, gameObject);
            Destroy(handRC.GetComponentInChildren<productInfo>().gameObject);
            //TriggerSanityChange(currentQuestVersion);
        }
    }
    //Function for changing sanity and job security values
    public void TriggerSanityChange(int i)
    {
        SM.ChangeSanity(sanityChange[i], jobSecChange[i]);
    }

    private void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    //Set of Variants for the Restock Shelves Quest
    private void ShelfQuest(int questVersion)
    {
        objectsPlaced = 0;

        switch (questVersion)
        {
            case 0:
                objectsToPlace = 30;
                QM.stepsToDo = objectsToPlace;
                QM.UpdateQuest(actionSelector, questVersion);
                break;
            case 1:               
                objectsToPlace = 10;
                QM.stepsToDo = objectsToPlace;
                QM.UpdateQuest(actionSelector, questVersion);
                break;
            case 2:
                QM.UpdateQuest(actionSelector, questVersion);
                //TriggerSanityChange(questVersion);
                QM.CompleteQuest(actionSelector,questVersion,gameObject);
                break;
            case 3:
                QM.UpdateQuest(actionSelector, questVersion);
                //handRC.UnlockPour(actionSelector, questVersion, gameObject);
                break;

        }
        
            
    }
}
