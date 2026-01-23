using System.Runtime.CompilerServices;
using UnityEngine;

public class ProducePour : MonoBehaviour
{
    [SerializeField] Animator animator;

    [SerializeField] ParticleSystem particles;

    private QuestContext questContext;

    void OnEnable()
    {
        GameEventsManager.instance.playerEvents.onPressedAttackQuest += StartPour;
        GameEventsManager.instance.playerEvents.onReleasedAttack += StopPour;
    }
    void OnDisable()
    {
        GameEventsManager.instance.playerEvents.onPressedAttackQuest -= StartPour;
        GameEventsManager.instance.playerEvents.onReleasedAttack -= StopPour;
    }

    void StartPour(InputEventContext inputContext, QuestContext questContext)
    {
        if(inputContext != InputEventContext.DEFAULT) return;

        switch (questContext)
        {
            case QuestContext.SHELF_POUR:
                particles.Play();
                animator.SetBool("Pour", true);
                break;
            case QuestContext.SHELF_SIP:
                animator.SetBool("Sip", true);
                break;
        }

        this.questContext = questContext;
    }

    void StopPour(InputEventContext context)
    {
        if(context != InputEventContext.DEFAULT) return;

        switch (questContext)
        {
            case QuestContext.SHELF_POUR:
                particles.Stop();
                animator.SetBool("Pour", false);
                break;
            case QuestContext.SHELF_SIP:
                animator.SetBool("Sip", false);
                break;
        }
    }
}
