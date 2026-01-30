using UnityEngine;
using VolumetricFogAndMist2;
using VolumetricFogAndMist2.Demos;

public class Flashlight : MonoBehaviour
{
    public Collider FlashConeCollider;

    public GameObject FogOfwar;

    Light _flashlight;

    private void Start()
    {
        _flashlight = GetComponentInChildren<Light>();

        gameObject.SetActive(false);
    }

    public void ToggleFlashlight(bool isOn)
    {
        GameEventsManager.instance.soundEvents.TriggerSound(SoundType.FLASHLIGHT);

        if (isOn)
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
