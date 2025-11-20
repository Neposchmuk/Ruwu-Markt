using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class MiniGameWipeFloor : MiniGameBaseState
{
    private Quest_Manager QM;

    private MiniGame_Caller QuestSource;

    private GameObject[] Puddles;

    private GameObject objectHeld;

    private Animator MopAnimator;

    private Collider MopCollider;

    private int questVariant;

    private int puddlesToClean;

    private int puddlesCleaned;

    private bool isHoldingMop;


    private void OnEnable()
    {
        Clean_Puddles.OnPuddleDestroy += UpdateQuest;
    }

    private void OnDisable()
    {
        Clean_Puddles.OnPuddleDestroy -= UpdateQuest;
    }



    public override void StartQuest(MiniGame_Caller Quest, int questVariant)
    {
        QM = GameObject.FindFirstObjectByType<Quest_Manager>();

        QuestSource = Quest;
  
        this.questVariant = questVariant;

        Quest.gameObject.GetComponent<Interaction_MenuTest>().ToggleUI(false);

        switch (questVariant)
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
        Clean_Puddles.OnPuddleDestroy += UpdateQuest;

        Puddles = GameObject.FindGameObjectsWithTag("Puddle");

        puddlesCleaned = 0;

        puddlesToClean = Puddles.Length;

        QM.isDoingQuest = true;

        QM.floorQuestText.text = "Grab the Mop";

        QuestSource.QuestMarkerBig.SetActive(false);

        QuestSource.QuestMarkerSmall.SetActive(true);
    }

    public override void UpdateQuest()
    {
        puddlesCleaned++;
        QM.floorQuestText.text = "Clean all puddles" + $"({puddlesCleaned}/{puddlesToClean}";
    }
    public override void EndQuest()
    {
        Clean_Puddles.OnPuddleDestroy -= UpdateQuest;
        QM.CompleteQuest(1, questVariant - 1, QuestSource.gameObject);
    }

    public override void Interact()
    {
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0.5f));

        if (Physics.Raycast(ray, out RaycastHit hit, 2, QuestSource.interactionLayer))
        {
            if (hit.collider.tag == "Mop")
            {
                objectHeld = GameObject.FindFirstObjectByType<Hand_Actions>().PickUpObject(2, new Vector3(-80, 0, 0)); // 
                Debug.Log(objectHeld);
                Debug.Log(hit.collider.gameObject.layer);
                isHoldingMop = true;
                Debug.Log(isHoldingMop);
                MopAnimator = objectHeld.GetComponent<Animator>();
                MopCollider = objectHeld.GetComponentInChildren<Collider>();
                MopCollider.enabled = false;
                Debug.Log(MopCollider.gameObject);
                GameObject.Destroy(hit.collider.gameObject);
                QM.floorQuestText.text = "Clean all puddles" + $"({puddlesCleaned}/{puddlesToClean}";

                QuestSource.QuestMarkerSmall.SetActive(false);
            }
        }
    }

    public override void HoldingAttack(bool buttonIsPressed)
    {
        if (isHoldingMop && buttonIsPressed)
        {
            MopCollider.enabled = true;
            MopAnimator.SetBool("IsCleaning", true);
        }
        else if(isHoldingMop)
        {
            MopCollider.enabled = false;
            MopAnimator.SetBool("IsCleaning", false);
        }

        if (puddlesCleaned == puddlesToClean)
        {
            MopCollider.enabled = false;
            MopAnimator.SetBool("IsCleaning", false);
            GameObject.Destroy(objectHeld);
            EndQuest();
        }
    }
}
