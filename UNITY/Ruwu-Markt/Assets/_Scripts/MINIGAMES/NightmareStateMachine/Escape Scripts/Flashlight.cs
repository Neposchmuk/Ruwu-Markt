using UnityEngine;
using VolumetricFogAndMist2;
using VolumetricFogAndMist2.Demos;

public class Flashlight : MonoBehaviour
{
    public Collider FlashConeCollider;

    public GameObject FogOfwar;

    [SerializeField] MeshRenderer meshRenderer;

    private bool flashActive;

    Light _flashlight;

    private void OnEnable()
    {
        GameEventsManager.instance.playerEvents.onPressedSpecialPrimary += ToggleFlashlight;
    }
    private void OnDisable()
    {
        GameEventsManager.instance.playerEvents.onPressedSpecialPrimary -= ToggleFlashlight;
    }

    private void Start()
    {
        _flashlight = GetComponentInChildren<Light>();

        _flashlight.enabled = false;
        FlashConeCollider.enabled = false;
        FogOfwar.SetActive(false);
        meshRenderer.enabled = false;
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
            meshRenderer.enabled = true;
        }
        else
        {
            _flashlight.enabled = false;
            FlashConeCollider.enabled = false;
            FogOfwar.SetActive(false);
            meshRenderer.enabled = false;
        } 
    }
}
