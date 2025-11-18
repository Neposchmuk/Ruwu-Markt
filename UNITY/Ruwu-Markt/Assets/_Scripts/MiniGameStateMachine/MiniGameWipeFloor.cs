using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class MiniGameWipeFloor : MiniGameBaseState
{
    private Quest_Manager QM;

    private MiniGameStateManager QuestSource;

    private GameObject[] Puddles;

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



    public override void StartQuest(MiniGameStateManager Quest, int questVariant)
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
        Puddles = GameObject.FindGameObjectsWithTag("Puddle");

        puddlesCleaned = 0;

        puddlesToClean = Puddles.Length;

        QM.isDoingQuest = true;
    }

    public override void UpdateQuest()
    {
        puddlesCleaned++;
        QM.floorQuestText.text = "Clean all puddles" + $"({puddlesCleaned}/{puddlesToClean}";
    }
    public override void EndQuest()
    {
        QM.CompleteQuest(1, questVariant - 1, QuestSource.gameObject);
    }

    public override void Interact()
    {
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0.5f));

        if (Physics.Raycast(ray, out RaycastHit hit, 2, QuestSource.interactionLayer))
        {
            if (hit.collider.tag == "Mop")
            {
                GameObject objectHeld = GameObject.FindFirstObjectByType<Hand_Actions>().PickUpObject(2);
                Debug.Log(hit.collider.gameObject.layer);
                isHoldingMop = true;
                Debug.Log(isHoldingMop);
                MopAnimator = objectHeld.GetComponent<Animator>();
                MopCollider = objectHeld.GetComponentInChildren<Collider>();
                GameObject.Destroy(hit.collider.gameObject);
            }
        }
    }

    public override void Attack()
    {
        if (isHoldingMop)
        {
            MopCollider.enabled = true;
            MopAnimator.SetBool("IsCleaning", true);
        }
        else
        {
            MopCollider.enabled = false;
            MopAnimator.SetBool("IsCleaning", false);
        }

        if (puddlesCleaned == puddlesToClean)
        {
            EndQuest();
        }
    }
}
