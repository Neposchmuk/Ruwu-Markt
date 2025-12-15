using UnityEngine;

public class Flashlight : MonoBehaviour
{
    public Collider FlashConeCollider;

    Light _flashlight;

    private void Start()
    {
        _flashlight = GetComponentInChildren<Light>();

        gameObject.SetActive(false);
    }

    public void ToggleFlashlight(bool isOn)
    {
        if (isOn)
        {
            _flashlight.enabled = true;
            FlashConeCollider.enabled = true;
            gameObject.SetActive(true);
        }
        else
        {
            _flashlight.enabled = false;
            FlashConeCollider.enabled = false;
            gameObject.SetActive(false);
        }
    }
}
