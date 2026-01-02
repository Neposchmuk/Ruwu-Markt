using UnityEngine;
using Ink.Runtime;


//https://www.youtube.com/watch?v=l8yI_97vjZs&t=1360
public class DialogueManager : MonoBehaviour
{
    [Header("Ink Story")]
    [SerializeField] private TextAsset inkJson;

    private Story _story;

    private bool _dialoguePlaying = false;


    private void Awake()
    {
        _story = new Story(inkJson.text);
    }

    private void OnEnable()
    {
        GameEventsManager.instance.dialogueEvents.onEnterDialogue += EnterDialogue;
    }

    private void OnDisable()
    {
        GameEventsManager.instance.dialogueEvents.onEnterDialogue -= EnterDialogue;
    }

    private void EnterDialogue(string knotName)
    {
        if (_dialoguePlaying) return;

        _dialoguePlaying = true;

        if (!knotName.Equals(""))
        {
            _story.ChoosePathString(knotName);
        }
        else
        {
            Debug.LogWarning("Knot name was empty string when entering Dialogue");
        }

        ContinueOrExitStory();
    }

    private void ContinueOrExitStory()
    {
        if (_story.canContinue)
        {
            string dialogueLine = _story.Continue();
        }
        else
        {
            ExitDialogue();
        }
    }

    private void ExitDialogue()
    {
        _dialoguePlaying = false;

        _story.ResetState();
    }
}
