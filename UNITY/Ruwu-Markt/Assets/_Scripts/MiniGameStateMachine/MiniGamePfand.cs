using UnityEditor;
using UnityEngine;

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
    public override void StartQuest(MiniGame_Caller Quest, int questVariant)
    {
        QM = GameObject.FindFirstObjectByType<Quest_Manager>();

        HA = GameObject.FindFirstObjectByType<Hand_Actions>();

        PPO = Quest.gameObject.GetComponent<PfandPyramidObjects>();

        this.questVariant = questVariant;

        QuestSource = Quest;

        Quest.gameObject.GetComponent<Interaction_MenuTest>().ToggleUI(false);

        switch (questVariant)
        {
            case 1:
                cratesToPlace = 9;
                InitiateQuest();
                break;
            case 2:
                bottlesToPlace = 12;
                InitiateQuest();
                break;
            case 3:
                InitiateQuest();
                pyramidCratesToPlace = 5;
                break;
            case 4:
                InitiateQuest();
                bottlesToThrow = 5;
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
                QM.pfandQuestText.text = "Sort the crates (" + $"{cratesPlaced}" + "/" + $"{cratesToPlace}" + ")";
                break;
            case 2:
                QM.pfandQuestText.text = "Fill the crate (" + $"{bottlesPlaced}" + "/" + $"{bottlesToPlace}" + ")";
                break;
            case 3:
                QM.pfandQuestText.text = "Build a pyramid";
                break;
            case 4:
                QM.pfandQuestText.text = "Throw some bottles";
                break;
        }

        QuestSource.QuestMarkerBig.SetActive(false);

        QuestSource.QuestMarkerSmall.SetActive(true);
            
    }

    public override void UpdateQuest()
    {
        switch (questVariant)
        {
            case 1:
                QM.pfandQuestText.text = "Sort the crates (" + $"{cratesPlaced}" + "/" + $"{cratesToPlace}" + ")";
                break;
            case 2:
                QM.pfandQuestText.text = "Fill the crate (" + $"{bottlesPlaced}" + "/" + $"{bottlesToPlace}" + ")";
                break;
            case 3:
                QM.pfandQuestText.text = "Place crates (" + $"{pyramidCratesPlaced}" + "/" + $"{pyramidCratesToPlace}" + ")";
                break;
            case 4:
                QM.pfandQuestText.text = "Bottles thrown (" + $"{bottlesThrown}" + "/" + $"{bottlesToThrow}" + ")";
                break;
        }
    }

    public override void EndQuest()
    {
        QuestSource.QuestMarkerBig.SetActive(false);
        QM.CompleteQuest(2, questVariant - 1, QuestSource.gameObject);
    }

    public override void Interact()
    {
        if (questVariant == 4 && isHoldingObject)
        {
            HA.ThrowObject(8);
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

        if (Physics.Raycast(ray, out RaycastHit hit, 2, QuestSource.interactionLayer))
        {


            switch (questVariant)
            {
                case 1:
                    if(hit.collider.tag == "CrateBlue" && !isHoldingObject)
                    {
                        HA.PickUpObject(5);
                        isHoldingObject = true;
                        GameObject.Destroy(hit.collider.gameObject);
                    }
                    else if (hit.collider.tag == "CrateYellow" && !isHoldingObject)
                    {
                        HA.PickUpObject(6);
                        isHoldingObject = true;
                        GameObject.Destroy(hit.collider.gameObject);
                    }
                    else if (hit.collider.tag == "CrateRed" && !isHoldingObject)
                    {
                        HA.PickUpObject(7);
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
                        HA.PickUpObject(4);
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
                        HA.PickUpObject(5);
                        isHoldingObject = true;
                        GameObject.Destroy(hit.collider.gameObject);
                    }
                    else if (hit.collider.tag == "CrateYellow" && !isHoldingObject)
                    {
                        HA.PickUpObject(6);
                        isHoldingObject = true;
                        GameObject.Destroy(hit.collider.gameObject);
                    }
                    else if (hit.collider.tag == "CrateRed" && !isHoldingObject)
                    {
                        HA.PickUpObject(7);
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
                        HA.PickUpObject(4);
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
}
