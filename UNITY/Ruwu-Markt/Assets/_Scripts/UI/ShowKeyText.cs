using System.Collections;
using TMPro;
using UnityEngine;

public class ShowKeyText : MonoBehaviour
{
    [SerializeField] private TMP_Text showKeyText;

    private bool coroutineRunning;

    private void OnEnable()
    {
        GameEventsManager.instance.questEvents.onShowKeytext += ShowText;
    }
    private void OnDisable()
    {
        GameEventsManager.instance.questEvents.onShowKeytext -= ShowText;
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
