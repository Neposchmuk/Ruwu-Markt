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

    public event Action<GameObject> onHitEnemy;

    public void HitEnemy(GameObject enemy)
    {
        if(onHitEnemy != null)
        {
            onHitEnemy(enemy);
        }
    }

    public event Action<GameObject> onAllowBottleExplode;

    public void AllowBottleExplode(GameObject bottle)
    {
        if(onAllowBottleExplode != null)
        {
            onAllowBottleExplode(bottle);
        }
    }

    public event Action<bool> onWateringFillState;

    public void WateringFillState(bool toggle)
    {
        if(onWateringFillState != null)
        {
            onWateringFillState(toggle);
        }
    }

    public event Action onplaceObject;

    public void PlaceObject()
    {
        if(onplaceObject != null)
        {
            onplaceObject();
        }
    }

    public event Action onShowKeytext;

    public void ShowKeyText()
    {
        if(onShowKeytext != null)
        {
            onShowKeytext();
        }
    }

    public event Action<float> onCanPourTime;

    public void CanPourTime(float time)
    {
        if(onCanPourTime != null)
        {
            onCanPourTime(time);
        }
    }

    public event Action<string> onUpdateAmmoText;

    public void UpdateAmmoText(string text)
    {
        if(onUpdateAmmoText != null)
        {
            onUpdateAmmoText(text);
        }
    }

    public event Action<string> onUpdateLivesText;

    public void UpdateLivesText(string text)
    {
        if(onUpdateLivesText != null)
        {
            onUpdateLivesText(text);
        }
    }

    public event Action<bool> onWaitForCustomerCheckout;

    public void WaitForCustomerCheckout(bool wait)
    {
        if(onWaitForCustomerCheckout != null)
        {
            onWaitForCustomerCheckout(wait);
        }
    }

    public event Action onShowCustomersWaitText;

    public void ShowCustomersWaitText()
    {
        if(onShowCustomersWaitText != null)
        {
            onShowCustomersWaitText();
        }
    }

    public event Action onAllowPlayerLeave;

    public void AllowPlayerLeave()
    {
        if(onAllowPlayerLeave != null)
        {
            onAllowPlayerLeave();
        }
    }
}
