using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class MiniGame_Caller : MonoBehaviour
{
    public QuestType Quest;

    public LayerMask interactionLayer;

    public List<int> sanityChange = new List<int>();

    public List<int> jobSecurityChange = new List<int>();

    public GameObject QuestMarkerBig;

    public GameObject QuestMarkerSmall;

    [SerializeField] private Collider InteractionCollider;

    private bool questComplete;


    private void OnEnable()
    {
        GameEventsManager.instance.questEvents.onToggleQuestmarkers += ToggleQuestmarker;
        GameEventsManager.instance.questEvents.onQuestCompleted += SetCompleteBool;
    }

    private void OnDisable()
    {
        GameEventsManager.instance.questEvents.onToggleQuestmarkers -= ToggleQuestmarker;
        GameEventsManager.instance.questEvents.onQuestCompleted -= SetCompleteBool;
    }


    private void Start()
    {
        QuestMarkerBig.SetActive(true);

        QuestMarkerSmall.SetActive(false);
    }
    public void CallStartQuest(int questVariant)
    {
        GameEventsManager.instance.soundEvents.TriggerSound(SoundType.UI_CLICK);
        FindFirstObjectByType<MiniGameStateManager>().StartQuest(this, Quest, questVariant, sanityChange, jobSecurityChange);
        InteractionCollider.enabled = false;
    }

    private void ToggleQuestmarker(bool toggle)
    {
        if (!questComplete)
        {
            Debug.Log("Toggled Questmarker: " + toggle + " | " + Quest);
            QuestMarkerBig.SetActive(toggle);
        }
    }

    private void SetCompleteBool(QuestType questType)
    {
        if(questType != Quest) return;

        Debug.Log("Completed Quest: " + questType + " | " + Quest);

        questComplete = true;
    }
}
