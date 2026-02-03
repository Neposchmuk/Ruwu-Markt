using TMPro;
using UnityEngine;

public class LivesTextListener : MonoBehaviour
{
    [SerializeField] TMP_Text livesText;


    void OnEnable()
    {
        GameEventsManager.instance.questEvents.onUpdateLivesText += UpdateText;
    }
    void OnDisable()
    {
        GameEventsManager.instance.questEvents.onUpdateLivesText -= UpdateText;
    }

    void UpdateText(string text)
    {
        livesText.text = text;
    }
}
