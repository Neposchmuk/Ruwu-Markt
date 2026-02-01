using UnityEngine;
using VolumetricFogAndMist2;
using VolumetricFogAndMist2.Demos;

public class Flashlight : MonoBehaviour
{
    public Collider FlashConeCollider;

    public GameObject FogOfwar;

    private bool flashActive;

    Light _flashlight;

    private void OnEnable()
    {
        GameEventsManager.instance.playerEvents.onPressedSpecialPrimary += ToggleFlashlight;
    }

    private void Start()
    {
        _flashlight = GetComponentInChildren<Light>();

        gameObject.SetActive(false);
    }

    void ToggleFlashlight(InputEventContext context)
    {
        if(context != InputEventContext.NIGHTMARE_ESCAPE) return;

        flashActive = !flashActive;

        GameEventsManager.instance.soundEvents.TriggerSound(SoundType.FLASHLIGHT);

        if (flashActive)
        {
            _flashlight.enabled = true;
            FlashConeCollider.enabled = true;
            FogOfwar.SetActive(true);
            gameObject.SetActive(true);
        }
        else
        {
            _flashlight.enabled = false;
            FlashConeCollider.enabled = false;
            FogOfwar.SetActive(false);
            gameObject.SetActive(false);
        } 
    }
}
