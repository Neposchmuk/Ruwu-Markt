using UnityEngine;
using TMPro;
using System.Collections;

public class ShowCustomersWaitText : MonoBehaviour
{
    [SerializeField] private TMP_Text customersWaitText;

    private bool coroutineRunning;

    private void OnEnable()
    {
        GameEventsManager.instance.questEvents.onShowCustomersWaitText += ShowText;
    }
    private void OnDisable()
    {
        GameEventsManager.instance.questEvents.onShowCustomersWaitText -= ShowText;
    }

    private void Start()
    {
        customersWaitText.enabled = false;
    }

    private void ShowText()
    {
        if(!coroutineRunning)   StartCoroutine(DisplayText());
    }

    private IEnumerator DisplayText()
    {
        coroutineRunning = true;

        customersWaitText.enabled = true;

        yield return new WaitForSeconds(3);

        customersWaitText.enabled = false;

        coroutineRunning = false;
    }
}
