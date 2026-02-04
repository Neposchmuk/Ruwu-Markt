using TMPro;
using UnityEngine;

public class QuestText : MonoBehaviour
{
    [SerializeField] TMP_Text questText;

    private void Awake()
    {
        GameEventsManager.instance.questEvents.onUpdateQuestText += UpdateText;
        GameEventsManager.instance.uiEvents.onToggleSanityWidget += ToggleText;
    }

    private void OnDestroy()
    {
        GameEventsManager.instance.questEvents.onUpdateQuestText -= UpdateText;
        GameEventsManager.instance.uiEvents.onToggleSanityWidget -= ToggleText;
    }

    private void UpdateText(string text)
    {
        questText.text = text;
    }

    private void ToggleText(bool toggle)
    {
        questText.enabled = toggle;
    }
}
