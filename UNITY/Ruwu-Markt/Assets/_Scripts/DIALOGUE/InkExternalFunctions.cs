using Ink.Runtime;
using UnityEngine;

public class InkExternalFunctions
{
    public void Bind(Story story)
    {
        story.BindExternalFunction("AdvanceQuest", (int endingType) => AdvanceQuest(endingType));
        story.BindExternalFunction("SendSpitEvent", () => SendSpitEvent());
    }

    public void Unbind(Story story)
    {
        story.UnbindExternalFunction("AdvanceQuest");
        story.UnbindExternalFunction("SendSpitEvent");
    }

    public void AdvanceQuest(int endingType)
    {
        GameEventsManager.instance.questEvents.AdvanceFinalQuest(endingType);
    }

    public void SendSpitEvent()
    {
        Debug.Log("Entered");

        GameEventsManager.instance.gameEvents.SetSanity(99,1);
        GameEventsManager.instance.gameEvents.SendSanityChange(0,0);
    }
}
