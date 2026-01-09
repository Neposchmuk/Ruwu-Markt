using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using System.Collections.Generic;

public class MiniGamePfand : MiniGameBaseState
{
    private int questVariant;

    private int bottlesToPlace;

    private int bottlesPlaced;

    private int cratesPlaced;

    private int cratesToPlace;

    private int bottlesToThrow;

    private int bottlesThrown;

    private int pyramidCratesToPlace;

    private int pyramidCratesPlaced;

    private bool isHoldingObject;

    private MiniGame_Caller QuestSource;

    private Quest_Manager QM;

    private Hand_Actions HA;

    private PfandPyramidObjects PPO;

    private List<GameObject> crates = new List<GameObject>();

    private List<GameObject> bottles = new List<GameObject>();

    private List<GameObject> placeZones = new List<GameObject>();

    private GameObject pyramidZone;

    private GameObject crateFill;
    public override void StartQuest(MiniGame_Caller Quest, int questVariant)
    {
        QM = GameObject.FindFirstObjectByType<Quest_Manager>();

        HA = GameObject.FindFirstObjectByType<Hand_Actions>();

        PPO = Quest.gameObject.GetComponent<PfandPyramidObjects>();

        FillLists();

        this.questVariant = questVariant;

        QuestSource = Quest;

        Quest.gameObject.GetComponent<Interaction_MenuTest>().ToggleUI(false);

        switch (questVariant)
        {
            case 1:
                cratesToPlace = 9;
                foreach (GameObject placeZone in PPO.placingZones)
                {
                    placeZone.SetActive(true);
                }
                InitiateQuest();
                break;
            case 2:
                bottlesToPlace = 12;
                InitiateQuest();
                break;
            case 3:
                pyramidZone.SetActive(true);
                /*foreach(GameObject pyramidPlaceholder in PPO.pyramidCrates)
                {
                    pyramidPlaceholder.SetActive(true);
                    pyramidPlaceholder.GetComponent<Renderer>().enabled = false;
                }*/
                pyramidCratesToPlace = 5;
                InitiateQuest();
                break;
            case 4:
                bottlesToThrow = 8;
                InitiateQuest();                
                break;
        }
    }

    public override void InitiateQuest()
    {
        cratesPlaced = 0;

        bottlesPlaced = 0;

        pyramidCratesPlaced = 0;

        bottlesThrown = 0;

        QM.isDoingQuest = true;

        switch(questVariant)
        {
            case 1:
                GameEventsManager.instance.questEvents.UpdateQuestText("Sort the crates (" + $"{cratesPlaced}" + "/" + $"{cratesToPlace}" + ")");
                break;
            case 2:
                GameEventsManager.instance.questEvents.UpdateQuestText("Fill the crate (" + $"{bottlesPlaced}" + "/" + $"{bottlesToPlace}" + ")");
                break;
            case 3:
                GameEventsManager.instance.questEvents.UpdateQuestText("Build a pyramid");
                break;
            case 4:
                GameEventsManager.instance.questEvents.UpdateQuestText("Throw some bottles");
                break;
        }

        ToggleQuestMarkers(questVariant, true);

        QuestSource.QuestMarkerBig.SetActive(false);

        QuestSource.QuestMarkerSmall.SetActive(true);

        GameEventsManager.instance.questEvents.ToggleQuestmarkers(false);
            
    }

    public override void UpdateQuest()
    {
        switch (questVariant)
        {
            case 1:
                GameEventsManager.instance.questEvents.UpdateQuestText("Sort the crates (" + $"{cratesPlaced}" + "/" + $"{cratesToPlace}" + ")");
                break;
            case 2:
                GameEventsManager.instance.questEvents.UpdateQuestText("Fill the crate (" + $"{bottlesPlaced}" + "/" + $"{bottlesToPlace}" + ")");
                break;
            case 3:
                GameEventsManager.instance.questEvents.UpdateQuestText("Place crates (" + $"{pyramidCratesPlaced}" + "/" + $"{pyramidCratesToPlace}" + ")");
                break;
            case 4:
                GameEventsManager.instance.questEvents.UpdateQuestText("Bottles thrown (" + $"{bottlesThrown}" + "/" + $"{bottlesToThrow}" + ")");
                break;
        }
    }

    public override void EndQuest()
    {
        QuestSource.QuestMarkerBig.SetActive(false);
        ToggleQuestMarkers(questVariant, false);
        GameEventsManager.instance.questEvents.QuestCompleted(QuestType.Bottles);
        GameEventsManager.instance.questEvents.ToggleQuestmarkers(true);
        QM.CompleteQuest(2, questVariant - 1, QuestSource.gameObject);
    }

    public override void Interact()
    {
        if (questVariant == 4 && isHoldingObject)
        {
            HA.ThrowObject(3);
            HA.DestroyObjectInHand();
            bottlesThrown++;
            UpdateQuest();
            isHoldingObject = false;
            if (bottlesThrown == bottlesToThrow)
            {
                EndQuest();
            }
        }

        Debug.Log("Called Interact");
        Debug.Log("QuestVariant: " + questVariant);

        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0.5f));

        if (Physics.Raycast(ray, out RaycastHit hit, 2.5f, QuestSource.interactionLayer))
        {


            switch (questVariant)
            {
                case 1:
                    if(hit.collider.tag == "CrateBlue" && !isHoldingObject)
                    {
                        HA.PickUpObject(4);
                        isHoldingObject = true;
                        GameObject.Destroy(hit.collider.gameObject);
                    }
                    else if (hit.collider.tag == "CrateYellow" && !isHoldingObject)
                    {
                        HA.PickUpObject(5);
                        isHoldingObject = true;
                        GameObject.Destroy(hit.collider.gameObject);
                    }
                    else if (hit.collider.tag == "CrateRed" && !isHoldingObject)
                    {
                        HA.PickUpObject(6);
                        isHoldingObject = true;
                        GameObject.Destroy(hit.collider.gameObject);
                    }

                    if (hit.collider.tag == "CrateAreaBlue" && HA.objectHolding.CompareTag("CrateBlue"))
                    {
                        HA.Place(hit);
                        HA.DestroyObjectInHand();
                        cratesPlaced++;
                        UpdateQuest();
                        isHoldingObject = false;
                        if(cratesPlaced == cratesToPlace)
                        {
                            EndQuest();
                        }
                    }
                    else if (hit.collider.tag == "CrateAreaYellow" && HA.objectHolding.CompareTag("CrateYellow"))
                    {
                        HA.Place(hit);
                        HA.DestroyObjectInHand();                       
                        cratesPlaced++;
                        UpdateQuest();
                        isHoldingObject = false;
                        if (cratesPlaced == cratesToPlace)
                        {
                            EndQuest();
                        }
                    }
                    else if (hit.collider.tag == "CrateAreaRed" && HA.objectHolding.CompareTag("CrateRed"))
                    {
                        HA.Place(hit);
                        HA.DestroyObjectInHand();
                        cratesPlaced++;
                        UpdateQuest();
                        isHoldingObject = false;
                        if (cratesPlaced == cratesToPlace)
                        {
                            EndQuest();
                        }
                    }
                    break;
                case 2:

                    Debug.Log(hit.collider.name);
                    if(hit.collider.tag == "Bottle" && !isHoldingObject)
                    {
                        HA.PickUpObject(3);
                        isHoldingObject = true;
                        GameObject.Destroy(hit.collider.gameObject);
                    }

                    if(hit.collider.tag == "CrateFill" && isHoldingObject && bottlesPlaced < bottlesToPlace)
                    {
                        HA.DestroyObjectInHand();
                        isHoldingObject = false;
                        bottlesPlaced++;
                        UpdateQuest();
                        if (bottlesPlaced == bottlesToPlace)
                        {
                            EndQuest();
                        }
                    }
                    break;
                case 3:
                    if (hit.collider.tag == "CrateBlue" && !isHoldingObject)
                    {
                        HA.PickUpObject(4);
                        isHoldingObject = true;
                        GameObject.Destroy(hit.collider.gameObject);
                    }
                    else if (hit.collider.tag == "CrateYellow" && !isHoldingObject)
                    {
                        HA.PickUpObject(5);
                        isHoldingObject = true;
                        GameObject.Destroy(hit.collider.gameObject);
                    }
                    else if (hit.collider.tag == "CrateRed" && !isHoldingObject)
                    {
                        HA.PickUpObject(6);
                        isHoldingObject = true;
                        GameObject.Destroy(hit.collider.gameObject);
                    }

                    if (hit.collider.tag == "PyramidArea" && isHoldingObject)
                    {
                        HA.Place(PPO.pyramidCrates[pyramidCratesPlaced].transform.position, PPO.pyramidCrates[pyramidCratesPlaced].transform.localEulerAngles, new Vector3(1,1,1));
                        HA.DestroyObjectInHand();
                        pyramidCratesPlaced++;
                        UpdateQuest();
                        isHoldingObject = false;
                        if (pyramidCratesPlaced == pyramidCratesToPlace)
                        {
                            EndQuest();
                        }
                    }
                    break;
                case 4:
                    if (hit.collider.tag == "Bottle" && !isHoldingObject)
                    {
                        HA.PickUpObject(3);
                        isHoldingObject = true;
                        GameObject.Destroy(hit.collider.gameObject);
                    }
                    break;
            }
        }

        
    }

    public override void HoldingAttack(bool buttonIsPressed)
    {
        
    }

    private void FillLists()
    {
        crates.AddRange(GameObject.FindGameObjectsWithTag("CrateBlue"));
        crates.AddRange(GameObject.FindGameObjectsWithTag("CrateYellow"));
        crates.AddRange(GameObject.FindGameObjectsWithTag("CrateRed"));

        bottles.AddRange(GameObject.FindGameObjectsWithTag("Bottle"));

        placeZones.AddRange(GameObject.FindGameObjectsWithTag("CrateAreaBlue"));
        placeZones.AddRange(GameObject.FindGameObjectsWithTag("CrateAreaYellow"));
        placeZones.AddRange(GameObject.FindGameObjectsWithTag("CrateAreaRed"));

        pyramidZone = PPO.pyramidZone;

        crateFill = GameObject.FindGameObjectWithTag("CrateFill");
    }

    private void ToggleQuestMarkers(int questVariant, bool isActive)
    {
        switch(questVariant)
        {
            case 1:
                foreach (GameObject crate in crates)
                {
                    if (crate != null)
                    {
                        crate.GetComponentInChildren<Canvas>().enabled = isActive;
                    }
                }
                foreach (GameObject placeZone in placeZones)
                {
                    placeZone.GetComponentInChildren<Canvas>().enabled = isActive;
                }
                break;
            case 2:
                foreach (GameObject bottle in bottles)
                {
                    if (bottle != null)
                    {
                        bottle.GetComponentInChildren<Canvas>().enabled = isActive;
                    }
                }
                crateFill.GetComponentInChildren<Canvas>().enabled = isActive;
                break;
            case 3:
                 foreach (GameObject crate in crates)
                 {
                    if(crate != null)
                    {
                        crate.GetComponentInChildren<Canvas>().enabled = isActive;
                    }    
                 }
                pyramidZone.GetComponentInChildren<Canvas>().enabled = isActive;
                break;
            case 4:
                foreach (GameObject bottle in bottles)
                {
                    if (bottle != null)
                    {
                        bottle.GetComponentInChildren<Canvas>().enabled = isActive;
                    }
                }
                break;
        }
    }
}
