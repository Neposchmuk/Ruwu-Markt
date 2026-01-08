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
}
