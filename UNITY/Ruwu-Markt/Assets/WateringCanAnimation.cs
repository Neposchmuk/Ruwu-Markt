using UnityEngine;

public class WateringCanAnimation : MonoBehaviour
{
    [SerializeField] Animator animator;

    [SerializeField] ParticleSystem particles;

    bool canPour;

    void OnEnable()
    {
        GameEventsManager.instance.playerEvents.onPressedAttack += StartPour;
        GameEventsManager.instance.playerEvents.onReleasedAttack += StopPour;
        GameEventsManager.instance.questEvents.onWateringFillState += ToggleParticles;
    }
    void OnDisable()
    {
        GameEventsManager.instance.playerEvents.onPressedAttack -= StartPour;
        GameEventsManager.instance.playerEvents.onReleasedAttack -= StopPour;
        GameEventsManager.instance.questEvents.onWateringFillState -= ToggleParticles;
    }

    void StartPour(InputEventContext context)
    {
        if(context != InputEventContext.DEFAULT) return;

        if (canPour)
        {
            particles.Play();
        }   

        animator.SetBool("Pour", true);
    }

    void StopPour(InputEventContext context)
    {
        if(context != InputEventContext.DEFAULT) return;

        particles.Stop();

        animator.SetBool("Pour", false);
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
                }            
                break;
        }
    }
}
