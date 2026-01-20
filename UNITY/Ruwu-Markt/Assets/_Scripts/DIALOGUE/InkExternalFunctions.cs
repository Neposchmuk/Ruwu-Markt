using Ink.Runtime;
using UnityEngine;

public class InkExternalFunctions
{
    public void Bind(Story story)
    {
        story.BindExternalFunction("AdvanceQuest", (int endingType) => AdvanceQuest(endingType));
    }

    public void Unbind(Story story)
    {
        story.UnbindExternalFunction("AdvanceQuest");
    }

    public void AdvanceQuest(int endingType)
    {
        GameEventsManager.instance.questEvents.AdvanceFinalQuest(endingType);
    }
}
