using UnityEngine;
using UnityEngine.SceneManagement;

public class Day_Manager : MonoBehaviour
{
    public string dayScene;

    public string nightScene;

    public int Day {get; private set;} = 1; 

    public int Night {get; private set;} = 1;

    public void AddDay()
    {
        Day++;
        LoadNextScene(2);
    }

    public void AddNight()
    {
        Debug.Log("End Night");
        Night++;
        LoadNextScene(1);
    }

    void LoadNextScene(int setScene)
    {
        switch (setScene)
        {
            case 1:
                SceneManager.LoadScene(dayScene);
                break;
            case 2:
                SceneManager.LoadScene(nightScene);
                break;
        }
    }
}
