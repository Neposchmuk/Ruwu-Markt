using NUnit.Framework;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Quest_Manager : MonoBehaviour
{
    private QuestType currentQuest;

    public bool isDoingQuest;

    public TMP_Text shelfQuestText;

    public TMP_Text floorQuestText;

    public TMP_Text pfandQuestText;

    public TMP_Text flowersQuestText;

    public bool shelfQuestCompleted;

    public bool floorQuestCompleted;

    public bool pfandQuestCompleted;

    public bool flowersQuestCompleted;

    public bool customersQuestCompleted;

    public bool DayComplete;

    public int stepsDone;

    public int stepsToDo;

    public GameObject endDayText;

    //public string NextScene;

    private string defaultQuestText;

    private int customersServed;

    private InputAction endDay;

    private Sanity_Manager SM;

    private Day_Manager _dayManager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Debug.Log(shelfQuestText.text);

        endDayText.SetActive(false);

        endDay = GameObject.Find("PlayerCapsule").GetComponent<PlayerInput>().actions.FindAction("TriggerSceneChange");

        SM = GameObject.FindFirstObjectByType<Sanity_Manager>();

        _dayManager = GameObject.FindFirstObjectByType<Day_Manager>();

        CashRegister_MiniGame.OnPay += CheckCustomersServed;

        ResetQuests();
    }

    public void CompleteDay()
    {
        if (_dayManager.IsFinalDay)
        {
            StartFinalQuest();
        }
        else
        {
            ResetQuests();
            _dayManager.IsDay = false;
            SceneManager.LoadScene("home");
        }
    }

    //Completes Quests by setting bools and resets doingQuest bool, so other quests ca nbe started
    public void CompleteQuest (int questType, int questVariant, GameObject questObject)
    {
        MiniGame_Caller MGC = questObject.GetComponent<MiniGame_Caller>();
        switch (questType)
        {
            case 0:
                shelfQuestCompleted = true;
                shelfQuestText.text = defaultQuestText;
                shelfQuestText.fontStyle = FontStyles.Strikethrough;
                isDoingQuest = false;
                switch (questVariant)
                {
                    case 0:
                        SM.ChangeSanity(MGC.sanityChange[questVariant], MGC.jobSecurityChange[questVariant]);
                        break;
                    case 1:
                        SM.ChangeSanity(MGC.sanityChange[questVariant], MGC.jobSecurityChange[questVariant]);
                        break;
                    case 2:
                        SM.ChangeSanity(MGC.sanityChange[questVariant], MGC.jobSecurityChange[questVariant]);
                        break;
                    case 3:
                        SM.ChangeSanity(MGC.sanityChange[questVariant], MGC.jobSecurityChange[questVariant]);
                        break;
                }
                break;
            case 1:
                floorQuestCompleted = true;
                floorQuestText.text = defaultQuestText;
                floorQuestText.fontStyle = FontStyles.Strikethrough;
                isDoingQuest = false;
                switch (questVariant)
                {
                    case 0:
                        SM.ChangeSanity(MGC.sanityChange[questVariant], MGC.jobSecurityChange[questVariant]);
                        break;
                    case 1:
                        SM.ChangeSanity(MGC.sanityChange[questVariant], MGC.jobSecurityChange[questVariant]);
                        break;
                }
                break;
            case 2:
                pfandQuestCompleted = true;
                pfandQuestText.text = defaultQuestText;
                pfandQuestText.fontStyle = FontStyles.Strikethrough;
                isDoingQuest = false;
                switch(questVariant)
                {
                    case 0:
                        SM.ChangeSanity(MGC.sanityChange[questVariant], MGC.jobSecurityChange[questVariant]);
                        break;
                    case 1:
                        SM.ChangeSanity(MGC.sanityChange[questVariant], MGC.jobSecurityChange[questVariant]);
                        break;
                    case 2:
                        SM.ChangeSanity(MGC.sanityChange[questVariant], MGC.jobSecurityChange[questVariant]);
                        break;
                    case 3:
                        SM.ChangeSanity(MGC.sanityChange[questVariant], MGC.jobSecurityChange[questVariant]);
                        break;
                }
                break;
            case 3:
                flowersQuestCompleted = true;
                flowersQuestText.text = defaultQuestText;
                flowersQuestText.fontStyle = FontStyles.Strikethrough;
                isDoingQuest = false;
                SM.ChangeSanity(MGC.sanityChange[questVariant], MGC.jobSecurityChange[questVariant]);
                break;
                
        }

        CheckDayCompletion();
    }

    //Resets Quest bools so quests need to be redone
    private void ResetQuests()
    {
        Debug.Log("Called Reset");
        isDoingQuest = false;

        shelfQuestCompleted = false;
        floorQuestCompleted = false;
        shelfQuestText.fontStyle = FontStyles.Normal;
        floorQuestText.fontStyle = FontStyles.Normal;

        endDayText.SetActive(false);
    }

    public void SetQuestText(int questType)
    {
        switch (questType)
        {
            case 0:
                defaultQuestText = shelfQuestText.text;

                break;
            case 1:
                defaultQuestText = floorQuestText.text;
                Debug.Log(defaultQuestText);
                break;
            case 2:
                defaultQuestText = pfandQuestText.text;
                break;
            case 3:
                defaultQuestText = flowersQuestText.text;
                break;
        }
        
    }

    //Checks if all quests have been completed to unlock Scene Change
    private void CheckDayCompletion()
    {
        if (shelfQuestCompleted && floorQuestCompleted && pfandQuestCompleted && flowersQuestCompleted && customersQuestCompleted && !SM.isGameOver)
        {
            DayComplete = true;
        }
    }

    void CheckCustomersServed()
    {
        customersServed++;

        if(customersServed == 3)
        {
            customersQuestCompleted = true;
            CheckDayCompletion();
        }
    }

    void StartFinalQuest()
    {

    }
}
