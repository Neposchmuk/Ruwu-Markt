using UnityEngine;
using Ink.Runtime;
using System.Collections;
using UnityEngine.InputSystem;


//https://www.youtube.com/watch?v=l8yI_97vjZs&t=1360
public class DialogueManager : MonoBehaviour
{
    [Header("Ink Story")]
    [SerializeField] private TextAsset inkJson;

    private Story _story;

    private int currentChoiceIndex = -1;

    private bool _dialoguePlaying = false;

    private bool _waitForFirstLine = true;

    private InputAction _interact;


    private void Awake()
    {
        _story = new Story(inkJson.text);
    }

    private void OnEnable()
    {
        GameEventsManager.instance.dialogueEvents.onEnterDialogue += EnterDialogue;
        GameEventsManager.instance.playerEvents.onPressedInteract += PressedInteract;
        GameEventsManager.instance.dialogueEvents.onUpdateChoiceIndex += UpdateChoiceIndex;
        GameEventsManager.instance.dialogueEvents.onPressedChoiceButton += PressedButton;
    }

    private void OnDisable()
    {
        GameEventsManager.instance.dialogueEvents.onEnterDialogue -= EnterDialogue;
        GameEventsManager.instance.playerEvents.onPressedInteract -= PressedInteract;
        GameEventsManager.instance.dialogueEvents.onUpdateChoiceIndex -= UpdateChoiceIndex;
        GameEventsManager.instance.dialogueEvents.onPressedChoiceButton -= PressedButton;
    }

    private void UpdateChoiceIndex(int choiceIndex)
    {
       this.currentChoiceIndex = choiceIndex;
    }

    private void PressedInteract(InputEventContext inputContext)
    {
        if (inputContext != InputEventContext.DIALOGUE) return;
        else
        {
            if(_story.currentChoices.Count == 0)
            ContinueOrExitStory();
        }
    }

    private void PressedButton()
    {
        if (_story.currentChoices.Count <= 0) return;
        else
        {
            if (_story.currentChoices.Count > 0)
                ContinueOrExitStory();
        }
    }

    private void EnterDialogue(string knotName)
    {
        if (_dialoguePlaying) return;

        _dialoguePlaying = true;

        GameEventsManager.instance.playerEvents.ChangeInputEventContext(InputEventContext.DIALOGUE);

        if (!knotName.Equals(""))
        {
            _story.ChoosePathString(knotName);

            Debug.Log(knotName);
        }
        else
        {
            Debug.LogWarning("Knot name was empty string when entering Dialogue");
        }

        GameEventsManager.instance.dialogueEvents.DialogueStarted();

        GameEventsManager.instance.playerEvents.LockPlayerMovement(true);

        GameEventsManager.instance.playerEvents.CameraLock(true);

        ContinueOrExitStory();
    }

    private void ContinueOrExitStory()
    {
        if(_story.currentChoices.Count > 0 && currentChoiceIndex != -1)
        {
            _story.ChooseChoiceIndex(currentChoiceIndex);

            currentChoiceIndex = -1;
        }

        if (_story.canContinue)
        {
            string dialogueLine = _story.Continue();

            GameEventsManager.instance.dialogueEvents.DisplayDialogue(dialogueLine, _story.currentChoices);
        }
        else if(_story.currentChoices.Count == 0)
        {
            StartCoroutine(ExitDialogue());
        }
    }

    private IEnumerator ExitDialogue()
    {
        yield return null;

        _dialoguePlaying = false;

        _waitForFirstLine = true;

        _story.ResetState();

        GameEventsManager.instance.playerEvents.ChangeInputEventContext(InputEventContext.DEFAULT);

        Debug.Log("Exit Dialogue");

        GameEventsManager.instance.dialogueEvents.DialogueFinished();

        GameEventsManager.instance.playerEvents.LockPlayerMovement(false);

        GameEventsManager.instance.playerEvents.CameraLock(false);
    }

}
