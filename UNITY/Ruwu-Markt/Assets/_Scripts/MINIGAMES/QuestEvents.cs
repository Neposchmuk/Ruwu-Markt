using System;
using UnityEngine;

public class QuestEvents
{
    public event Action<bool> onStartFinalQuest;

    public void StartFinalQuest(bool goodEnding)
    {
        Debug.Log(onStartFinalQuest);
        if(onStartFinalQuest != null)
        {
            onStartFinalQuest(goodEnding);
            Debug.Log("Sent event");
        }
    }

    public event Action<int> onAdvanceFinalQuest;

    public void AdvanceFinalQuest(int endingType)
    {
        if(onAdvanceFinalQuest != null)
        {
            onAdvanceFinalQuest(endingType);
        }
    }

    public event Action onEndFinalQuest;

    public void EndFinalQuest()
    {
        if(onEndFinalQuest != null)
        {
            onEndFinalQuest();
        }
    }

    public event Action<string> onUpdateQuestText;

    public void UpdateQuestText(string text)
    {
        if(onUpdateQuestText != null)
        {
            onUpdateQuestText(text);
        }
    }

    public event Action<GameObject> onUIButtonInteract;

    public void UIButtonInteract(GameObject gameObject)
    {
        if(onUIButtonInteract != null)
        {
            onUIButtonInteract(gameObject);
        }
    }

    public event Action<int> onButtonAddChange;

    public void ButtonAddChange(int amount)
    {
        if(onButtonAddChange != null)
        {
            onButtonAddChange(amount);
        }
    }

    public event Action onPayCard;

    public void PayCard()
    {
        if(onPayCard != null)
        {
            onPayCard();
        }
    }

    public event Action onPayCash;

    public void PayCash()
    {
        if(onPayCash != null)
        {
            onPayCash();
        }
    }

    public event Action<UIButtonType, bool> onToggleButtonInteractable;

    public void ToggleButtonInteractable(UIButtonType buttonType, bool toggle)
    {
        if(onToggleButtonInteractable != null)
        {
            onToggleButtonInteractable(buttonType, toggle);
        }
    }

    public event Action onAllTasksCompleted;

    public void AllTasksCompleted()
    {
        if(onAllTasksCompleted != null)
        {
            onAllTasksCompleted();
        }
    }

    public event Action<bool> onToggleQuestmarkers;

    public void ToggleQuestmarkers(bool toggle)
    {
        if(onToggleQuestmarkers != null)
        {
            onToggleQuestmarkers(toggle);
        }
    }

    public event Action<QuestType> onQuestCompleted;

    public void QuestCompleted(QuestType questType)
    {
        if(onQuestCompleted != null)
        {
            onQuestCompleted(questType);
        }
    }

    public event Action onHitEnemy;

    public void HitEnemy()
    {
        if(onHitEnemy != null)
        {
            onHitEnemy();
        }
    }
}
