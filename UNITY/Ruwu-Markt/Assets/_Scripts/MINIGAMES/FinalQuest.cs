using TMPro;
using UnityEngine;
using Ink.Runtime;
using System.Collections;

public class FinalQuest : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] Animator animator;
    [SerializeField] AnimationClip[] animations;


    private bool _hasGoodEnding;

    private bool _canEndGame;

    private int _endingType;

    private void OnEnable()
    {
        GameEventsManager.instance.questEvents.onStartFinalQuest += StartQuest;
        GameEventsManager.instance.questEvents.onAdvanceFinalQuest += AdvanceQuest;
    }

    private void OnDisable()
    {
        GameEventsManager.instance.questEvents.onStartFinalQuest -= StartQuest;
        GameEventsManager.instance.questEvents.onAdvanceFinalQuest -= AdvanceQuest;
    }

    private void StartQuest(bool goodEnding)
    {
        Debug.Log("Entered Start Quest");
        _hasGoodEnding = goodEnding;

        //Event show 'Talk To Boss' Text

        if (_hasGoodEnding)
        {
            GameEventsManager.instance.dialogueEvents.UpdateInkDialogueVariable("FinalQuestState", new StringValue("GOOD ENDING"));
        }
        else
        {
            GameEventsManager.instance.dialogueEvents.UpdateInkDialogueVariable("FinalQuestState", new StringValue("BAD ENDING"));
        }
    }

    private void AdvanceQuest(int endingType)
    {
        _canEndGame = true;

        _endingType = endingType;
    }

    private void EndQuest()
    {
        GameEventsManager.instance.gameEvents.ChangeScene("End_Scene");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!_canEndGame) return;

        GameEventsManager.instance.playerEvents.LockPlayerMovement(true);
        GameEventsManager.instance.playerEvents.LockCamera(true);

        StartCoroutine(PlayEndingAnimation());
    }

    IEnumerator PlayEndingAnimation()
    {
        //Animator.Clip = animations[_endingType-1];
        //Animator.Play();

        //yield return new WaitForSeconds(animations[_endingType - 1].length);

        yield return null;

        EndQuest();
    }
}
