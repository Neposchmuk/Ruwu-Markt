using System;
using UnityEngine;

public class DialogueEvents
{
    public event Action<string> onEnterDialogue;

    public void EnterDialogue(string knowName)
    {
        if (onEnterDialogue != null)
        {
            onEnterDialogue(knowName);
        }
    }
}
