using UnityEngine;
using UnityEngine.SceneManagement;

public class Day_Manager : MonoBehaviour
{
    public string dayScene;

    public string nightScene;

    public bool IsDay;

    public int Day {get; private set;} = 1; 

    public int Night {get; private set;} = 1;

    private void Start()
    {
        IsDay = true;
    }

    public void AddDay()
    {
        Day++;
        IsDay = false;
        LoadNextScene(2);
    }

    public void AddNight()
    {
        Debug.Log("End Night");
        Night++;
        IsDay = true;
        LoadNextScene(1);
    }

    public void LoadNextScene(int setScene)
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
