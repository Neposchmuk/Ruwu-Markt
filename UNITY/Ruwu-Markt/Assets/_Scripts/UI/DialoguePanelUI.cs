using TMPro;
using UnityEngine;
using System.Collections.Generic;
using Ink.Runtime;

public class DialoguePanelUI : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private GameObject contentParent;
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private DialogueChoiceButton[] choiceButtons;

    private void Awake()
    {
        contentParent.SetActive(false);
        ResetPanel();
    }

    private void OnEnable()
    {
        GameEventsManager.instance.dialogueEvents.onDialogueStarted += DialogueStarted;
        GameEventsManager.instance.dialogueEvents.onDialogueFinished += DialogueFinished;
        GameEventsManager.instance.dialogueEvents.onDisplayDialogue += DisplayDialogue;
    }

    private void OnDisable()
    {
        GameEventsManager.instance.dialogueEvents.onDialogueStarted -= DialogueStarted;
        GameEventsManager.instance.dialogueEvents.onDialogueFinished -= DialogueFinished;
        GameEventsManager.instance.dialogueEvents.onDisplayDialogue -= DisplayDialogue;
    }

    private void DialogueStarted()
    {
        contentParent.SetActive(true);
    }

    private void DialogueFinished()
    {
        contentParent.SetActive(false);
    }

    private void DisplayDialogue(string dialogueLine, List<Choice> dialogueChoices)
    {
        dialogueText.text = dialogueLine;

        if(dialogueChoices.Count > choiceButtons.Length)
        {
            Debug.LogError("More Dialogue Choices ("
                + dialogueChoices.Count + ") came through than are supported ("
                + choiceButtons.Length + ")");
        }

        foreach(DialogueChoiceButton choiceButton in choiceButtons) 
        {
            choiceButton.gameObject.SetActive(false);
        }

        int choiceButtonIndex = dialogueChoices.Count - 1;
        for(int inkChoiceIndex = 0;  inkChoiceIndex < dialogueChoices.Count; inkChoiceIndex++)
        {
            Choice dialogueChoice = dialogueChoices[inkChoiceIndex];
            DialogueChoiceButton choiceButton = choiceButtons[choiceButtonIndex];

            choiceButton.gameObject.SetActive(true);
            choiceButton.SetChoiceText(dialogueChoice.text);
            choiceButton.SetChoiceIndex(inkChoiceIndex);

            choiceButtonIndex--;
        }
    }

    private void ResetPanel()
    {
        dialogueText.text = "";
    }
}
