using System;
using System.Collections;
using UnityEngine;

public class SmashThings_Animation : MonoBehaviour
{
    public GameObject Bat;

    public AnimationClip AnimationClip;

    Animator _animator;

    private void Awake()
    {
        GameEventsManager.instance.playerEvents.onPressedSpecialPrimary += StartAnimatorCoroutine;
    }

    private void OnDestroy()
    {
        GameEventsManager.instance.playerEvents.onPressedSpecialPrimary -= StartAnimatorCoroutine;
    }


    private void Start()
    {
        _animator = GetComponent<Animator>();

        Bat.SetActive(false);
    }


    public void StartAnimatorCoroutine(InputEventContext context)
    {
        Debug.Log(context);

        if(context != InputEventContext.NIGHTMARE_DOOM && context != InputEventContext.NIGHTMARE_SMASH) return;

        StartCoroutine(WaitForAnimation());
    }

    IEnumerator WaitForAnimation()
    {
        Bat.SetActive(true);

        _animator.SetTrigger("Hit");

        yield return new WaitForSeconds(AnimationClip.length);

        //_animator.ResetTrigger("Hit");

        Bat.SetActive(false);
    }
}
