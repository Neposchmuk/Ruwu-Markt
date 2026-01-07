using StarterAssets;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class MissionText : MonoBehaviour
{
    public string[] MissionTexts;

    TMP_Text _text;

    PlayerInput _playerInput;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _text = GetComponent<TMP_Text>();

        _playerInput = FindFirstObjectByType<PlayerInput>();

        _text.enabled = false;

        StartCoroutine(AnimateText());
    }

    IEnumerator AnimateText()
    {
        int index = 0;

        _playerInput.DeactivateInput();

        yield return new WaitForSeconds(1);

        _text.enabled = true;

        foreach (string missionText in MissionTexts)
        {
            _text.text = MissionTexts[index];

            index++;

            yield return new WaitForSeconds(0.5f);

        }

        _text.enabled = false;

        _playerInput.ActivateInput();
    }
}
