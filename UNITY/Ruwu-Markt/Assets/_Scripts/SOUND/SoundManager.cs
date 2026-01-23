using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private SoundLibrary library;

    private void OnEnable()
    {
        GameEventsManager.instance.soundEvents.onTriggerSound += SendAudioClip;
    }
    private void OnDisable()
    {
        GameEventsManager.instance.soundEvents.onTriggerSound -= SendAudioClip;
    }


    private void SendAudioClip(SoundType sound)
    {
        switch (sound)
        {
            
        }
    }
}
