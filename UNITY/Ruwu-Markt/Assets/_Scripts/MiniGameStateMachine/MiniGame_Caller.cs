using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class MiniGame_Caller : MonoBehaviour
{
    public QuestType Quest;

    public LayerMask interactionLayer;

    public List<int> sanityChange = new List<int>();

    public List<int> jobSecurityChange = new List<int>();

    public void CallStartQuest(int questVariant)
    {
        FindFirstObjectByType<MiniGameStateManager>().StartQuest(this, Quest, questVariant, sanityChange, jobSecurityChange);
    }
}
