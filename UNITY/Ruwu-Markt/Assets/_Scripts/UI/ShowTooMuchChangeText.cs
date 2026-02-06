using UnityEngine;
using TMPro;
using System.Collections;

public class ShowTooMuchChangeText : MonoBehaviour
{
    [SerializeField] private TMP_Text showKeyText;

    private bool coroutineRunning;

    private void OnEnable()
    {
        GameEventsManager.instance.questEvents.onShowTooMuchChangeText += ShowText;
    }
    private void OnDisable()
    {
        GameEventsManager.instance.questEvents.onShowTooMuchChangeText -= ShowText;
    }

    private void Start()
    {
        showKeyText.enabled = false;
    }

    private void ShowText()
    {
        if(!coroutineRunning)   StartCoroutine(DisplayText());
    }

    private IEnumerator DisplayText()
    {
        coroutineRunning = true;

        showKeyText.enabled = true;

        yield return new WaitForSeconds(3);

        showKeyText.enabled = false;

        coroutineRunning = false;
    }
}
