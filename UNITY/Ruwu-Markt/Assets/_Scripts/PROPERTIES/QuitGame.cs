using UnityEngine;
using UnityEngine.SceneManagement;

public class QuitGame : MonoBehaviour
{
[SerializeField] string scene;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameEventsManager.instance.gameEvents.DestroyDDOLObjects();
        SceneManager.LoadScene(scene);
    }
}
