using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Day_Manager : MonoBehaviour
{
    public string dayScene;

    public string nightScene;

    public bool IsDay;

    public bool CheckedPC;

    public bool IsFinalDay;

    public bool GetsGoodEnding;

    public int Day = 1; 

    public int Night = 1;

    private void Awake()
    {
        GameEventsManager.instance.gameEvents.onSendSanityUpdate += UpdateEndingBool;
    }

    private void Start()
    {
        PC_Interaction.OnCloseUI += CheckPC;

        IsDay = true;

        CheckedPC = false;
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
        CheckedPC = false;

        if(Day == 5)
        {
            IsFinalDay = true;
            GameEventsManager.instance.gameEvents.RequestSanityUpdate();
            Debug.Log("Gets good ending : " + GetsGoodEnding);
            GameEventsManager.instance.gameEvents.onSendSanityUpdate -= UpdateEndingBool;
        }

        LoadNextScene(1);
    }

    public void LoadNextScene(int setScene)
    {
        switch (setScene)
        {
            case 1:
                GameEventsManager.instance.gameEvents.ChangeScene(dayScene);
                break;
            case 2:
                GameEventsManager.instance.gameEvents.ChangeScene(nightScene);
                break;
        }
    }

    void CheckPC()
    {
        CheckedPC = true;
    }

    private void UpdateEndingBool(int sanity, int jobSecurity)
    {
        if(sanity >= 50)
        {
            GetsGoodEnding = true;
        }
        else GetsGoodEnding = false;
        
    }
}
