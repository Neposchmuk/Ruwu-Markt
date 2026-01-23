using UnityEngine;

public class AudioSourceListener : MonoBehaviour
{
    [SerializeField] private AudioSource source;

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
        source.clip = clip;

        source.Play();
    }

    private void StopSound()
    {
        source.Stop();
    }
}
