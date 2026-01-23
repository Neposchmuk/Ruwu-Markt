using UnityEngine;

public class SoundLibrary : MonoBehaviour
{
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
