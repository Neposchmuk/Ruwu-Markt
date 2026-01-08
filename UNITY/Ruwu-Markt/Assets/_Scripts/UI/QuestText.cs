using TMPro;
using UnityEngine;

public class QuestText : MonoBehaviour
{
    [SerializeField] TMP_Text questText;

    private void Awake()
    {
        GameEventsManager.instance.questEvents.onUpdateQuestText += UpdateText;
    }

    private void OnDestroy()
    {
        GameEventsManager.instance.questEvents.onUpdateQuestText -= UpdateText;
    }

    private void UpdateText(string text)
    {
        questText.text = text;
    }
}
