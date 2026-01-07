using TMPro;
using UnityEngine;
using Ink.Runtime;
using System.Collections;

public class FinalQuest : MonoBehaviour
{
    private bool _hasGoodEnding;

    private void OnEnable()
    {
        GameEventsManager.instance.questEvents.onStartFinalQuest += StartQuest;
        GameEventsManager.instance.questEvents.onAdvanceFinalQuest += AdvanceQuest;
    }

    private void OnDisable()
    {
        GameEventsManager.instance.questEvents.onStartFinalQuest -= StartQuest;
        GameEventsManager.instance.questEvents.onAdvanceFinalQuest -= AdvanceQuest;
    }

    private void StartQuest(bool goodEnding)
    {
        Debug.Log("Entered Start Quest");
        _hasGoodEnding = goodEnding;

        //Event show 'Talk To Boss' Text

        if (_hasGoodEnding)
        {
            GameEventsManager.instance.dialogueEvents.UpdateInkDialogueVariable("FinalQuestState", new StringValue("GOOD ENDING"));
        }
        else
        {
            GameEventsManager.instance.dialogueEvents.UpdateInkDialogueVariable("FinalQuestState", new StringValue("BAD ENDING"));
        }
    }

    private void AdvanceQuest(int endingType)
    {
        switch (endingType)
        {
            case 1:
                //Good Ending
                break;
            case 2:
                //Bad Ending
                break;
            case 3:
                //Neutral Ending
                break;
        }
            
    }

    private void EndQuest()
    {
        //Load MainMenu
    }
}
