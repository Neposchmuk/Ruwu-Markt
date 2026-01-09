using NUnit.Framework;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Quest_Manager : MonoBehaviour
{
    private QuestType currentQuest;

    public bool isDoingQuest;

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

    [SerializeField] private TMP_Text questText;

    private string defaultQuestText;

    private int customersServed;

    private InputAction endDay;

    private Sanity_Manager SM;

    private Day_Manager _dayManager;


    private  void OnEnable()
    {
        GameEventsManager.instance.questEvents.onUpdateQuestText += UpdateQuestText;
    }

    private void OnDisable()
    {
        GameEventsManager.instance.questEvents.onUpdateQuestText -= UpdateQuestText;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        endDayText.SetActive(false);

        endDay = GameObject.Find("PlayerCapsule").GetComponent<PlayerInput>().actions.FindAction("TriggerSceneChange");

        SM = GameObject.FindFirstObjectByType<Sanity_Manager>();

        _dayManager = GameObject.FindFirstObjectByType<Day_Manager>();

        CashRegister_MiniGame.OnPay += CheckCustomersServed;

        ResetQuests();
    }

    private void UpdateQuestText(string text)
    {
        questText.text = text;
    }

    public void CompleteDay()
    {
        if (_dayManager.IsFinalDay)
        {
            GameEventsManager.instance.questEvents.StartFinalQuest(_dayManager.GetsGoodEnding);
        }
        else
        {
            ResetQuests();
            _dayManager.IsDay = false;
            GameEventsManager.instance.gameEvents.ChangeScene("home");
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
                isDoingQuest = false;
                SM.ChangeSanity(MGC.sanityChange[questVariant], MGC.jobSecurityChange[questVariant]);
                break;
                
        }

        GameEventsManager.instance.questEvents.UpdateQuestText("");

        CheckDayCompletion();
    }

    //Resets Quest bools so quests need to be redone
    private void ResetQuests()
    {
        Debug.Log("Called Reset");
        isDoingQuest = false;

        shelfQuestCompleted = false;
        floorQuestCompleted = false;
        flowersQuestCompleted = false;
        pfandQuestCompleted = false;

        endDayText.SetActive(false);
    }

    //Checks if all quests have been completed to unlock Scene Change
    private void CheckDayCompletion()
    {
        if (shelfQuestCompleted && floorQuestCompleted && pfandQuestCompleted && flowersQuestCompleted && customersQuestCompleted && !SM.isGameOver)
        {
            DayComplete = true;
        }
        else if(shelfQuestCompleted && floorQuestCompleted && pfandQuestCompleted && flowersQuestCompleted)
        {
            Debug.Log("Sending Event");
            GameEventsManager.instance.questEvents.AllTasksCompleted();
        }
    }

    void CheckCustomersServed()
    {
        customersServed++;

        if(customersServed == 3)
        {
            StartCoroutine(DelayCustomersBool());
        }
    }

   IEnumerator DelayCustomersBool()
    {
        yield return null;
        customersQuestCompleted = true;
        CheckDayCompletion();
    }
}
