using UnityEngine.UI;
using UnityEngine;

public class WateringCanAnimation : MonoBehaviour
{
    [SerializeField] Animator animator;

    [SerializeField] ParticleSystem particles;

    [SerializeField] Image fillBar;

    bool canPour;

    void OnEnable()
    {
        GameEventsManager.instance.playerEvents.onPressedAttack += StartPour;
        GameEventsManager.instance.playerEvents.onReleasedAttack += StopPour;
        GameEventsManager.instance.questEvents.onWateringFillState += ToggleParticles;
        GameEventsManager.instance.questEvents.onCanPourTime += SetPourFill;
    }
    void OnDisable()
    {
        GameEventsManager.instance.playerEvents.onPressedAttack -= StartPour;
        GameEventsManager.instance.playerEvents.onReleasedAttack -= StopPour;
        GameEventsManager.instance.questEvents.onWateringFillState -= ToggleParticles;
        GameEventsManager.instance.questEvents.onCanPourTime -= SetPourFill;
    }

    void StartPour(InputEventContext context)
    {
        if(context != InputEventContext.DEFAULT) return;

        if (canPour)
        {
            particles.Play();
        }   

        animator.SetBool("Pour", true);

        GameEventsManager.instance.soundEvents.TriggerSound(SoundType.WATER);
    }

    void StopPour(InputEventContext context)
    {
        if(context != InputEventContext.DEFAULT) return;

        particles.Stop();

        animator.SetBool("Pour", false);

        GameEventsManager.instance.soundEvents.StopSound();
    }

    void ToggleParticles(bool toggle)
    {
        canPour = toggle;

        switch (toggle)
        {
            case true:
                if (!particles.isPlaying)
                {
                    particles.Play();
                }
                break;
            case false:
                if (particles.isPlaying)
                {
                    particles.Stop();
                    
                    GameEventsManager.instance.soundEvents.StopSound();
                }            
                break;
        }
    }

    void SetPourFill(float time)
    {
        fillBar.fillAmount = time/100;
    }
}
