using System.Collections;
using UnityEngine;

public class SkipDayButton : MonoBehaviour
{
    [SerializeField] Animator animator;

    [SerializeField] Collider buttonCollider;

    void OnEnable()
    {
        GameEventsManager.instance.gameEvents.onSkipDay += Animate;
    }
    void OnDisable()
    {
        GameEventsManager.instance.gameEvents.onSkipDay -= Animate;
    }

    void Animate()
    {
        animator.SetTrigger("PressButton");
        buttonCollider.enabled = false;
        animator.ResetTrigger("PressButton");
    }
}
