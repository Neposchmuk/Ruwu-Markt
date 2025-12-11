using System;
using System.Collections;
using UnityEngine;

public class SmashThings_Animation : MonoBehaviour
{
    public GameObject Bat;

    public AnimationClip AnimationClip;

    Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();

        Nightmare_Smash_State.OnAttack += StartAnimatorCoroutine;

        Nightmare_Smash_State.OnFinish += UnsubscribeEvents;

        Bat.SetActive(false);
    }

    void UnsubscribeEvents()
    {
        Nightmare_Smash_State.OnAttack -= StartAnimatorCoroutine;

        Nightmare_Smash_State.OnFinish -= UnsubscribeEvents;
    }

    public void StartAnimatorCoroutine()
    {
        StartCoroutine(WaitForAnimation());
    }

    IEnumerator WaitForAnimation()
    {
        Bat.SetActive(true);

        _animator.SetTrigger("Hit");

        yield return new WaitForSeconds(AnimationClip.length);

        _animator.ResetTrigger("Hit");

        Bat.SetActive(false);
    }
}
