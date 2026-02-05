using UnityEngine;

public class AudioSourceListener : MonoBehaviour
{
    [SerializeField] private AudioSource source;
    [SerializeField] private AudioClip mopSoundClip;

    private void OnEnable()
    {
        GameEventsManager.instance.soundEvents.onSendAudioClip += PlaySound;
        GameEventsManager.instance.soundEvents.onStopSound += StopSound;
    }
    private void OnDisable()
    {
        GameEventsManager.instance.soundEvents.onSendAudioClip -= PlaySound;
        GameEventsManager.instance.soundEvents.onStopSound -= StopSound;
    }

    private void PlaySound(AudioClip clip)
    {
        Debug.Log("Received Play Audio event: " + clip);

        source.clip = clip;

        if(clip == mopSoundClip)
        {
            source.Play();  
        }
        else
        {
            source.PlayOneShot(clip);
        }

    }

    private void StopSound()
    {
        source.Stop();
    }
}
