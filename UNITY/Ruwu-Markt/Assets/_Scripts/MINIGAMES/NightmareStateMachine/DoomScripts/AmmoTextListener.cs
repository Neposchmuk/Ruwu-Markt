using TMPro;
using UnityEngine;

public class AmmoTextListener : MonoBehaviour
{
    [SerializeField] TMP_Text ammoText;


    void OnEnable()
    {
        GameEventsManager.instance.questEvents.onUpdateAmmoText += UpdateText;
    }
    void OnDisable()
    {
        GameEventsManager.instance.questEvents.onUpdateAmmoText -= UpdateText;
    }

    void UpdateText(string text)
    {
        ammoText.text = text;
    }
}
