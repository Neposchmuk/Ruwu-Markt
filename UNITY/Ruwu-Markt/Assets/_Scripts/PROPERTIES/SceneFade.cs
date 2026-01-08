using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEditor.VersionControl;

public class SceneFade : MonoBehaviour
{
    [SerializeField] private CanvasGroup blackImage;

    private void OnEnable()
    {
        GameEventsManager.instance.gameEvents.onChangeScene += ChangeScene;
    }

    private void OnDisable()
    {
        GameEventsManager.instance.gameEvents.onChangeScene -= ChangeScene;
    }

    private void Start()
    {
        GameEventsManager.instance.gameEvents.ToggleSanityWidget(true);
        LockPlayer(true);

        blackImage.gameObject.SetActive(true);
        StartCoroutine(FadeInScene(1));
    }

    private IEnumerator FadeOutScene(float duration, string scene)
    {
        float t = 0f;
        while (t < duration)
        {
            t += Time.deltaTime;
            blackImage.alpha = Mathf.Clamp01(t / duration);
            yield return null;

        }
        AudioListener.volume = 0f;
        blackImage.alpha = 1f;
        SceneManager.LoadScene(scene);
    }

    private IEnumerator FadeInScene(float duration)
    {
        float t = 0f;
        while (t < duration)
        {
            t += Time.deltaTime;
            blackImage.alpha = 1f - Mathf.Clamp01(t / duration);
            yield return null;

        }
        blackImage.alpha = 0f;
        blackImage.gameObject.SetActive(false);
        LockPlayer(false);
        Debug.Log("Ended Scene FadeIn");
    }

    private void ChangeScene(string scene)
    {
        LockPlayer(true);

        blackImage.gameObject.SetActive(true);
        StartCoroutine(FadeOutScene(1, scene));
    }

    private void LockPlayer(bool toggle)
    {
        GameEventsManager.instance.playerEvents.LockPlayerMovement(toggle);
        GameEventsManager.instance.playerEvents.LockCamera(toggle);
    }
}
