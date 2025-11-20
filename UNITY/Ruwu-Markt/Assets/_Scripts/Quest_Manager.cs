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

    public bool shelfQuestCompleted;

    public bool floorQuestCompleted;

    public int stepsDone;

    public int stepsToDo;

    public GameObject endDayText;

    public string NextScene;

    private string defaultQuestText;

    private InputAction endDay;

    private Sanity_Manager SM;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Debug.Log(shelfQuestText.text);

        endDayText.SetActive(false);

        endDay = GameObject.Find("PlayerCapsule").GetComponent<PlayerInput>().actions.FindAction("TriggerSceneChange");

        SM = GameObject.FindFirstObjectByType<Sanity_Manager>();

        ResetQuests();
    }

    private void Update()
    {
        //Loads next Scene, depending on sanity value
        if(endDayText.activeSelf && endDay.WasCompletedThisFrame())
        {
            ResetQuests();
            SceneManager.LoadScene(NextScene);
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
                
        }

        CheckDayCompletion();
    }
    //Updates Quest Text descriptions depending on Quest Variant selected
    public void UpdateQuest(int questType, int questVariant)
    {       
        switch (questType)
        {
            case 0:
                if (!isDoingQuest)
                {
                    
                    Debug.Log(defaultQuestText);
                    isDoingQuest = true;
                }

                switch (questVariant)
                {
                    case 0:
                        shelfQuestText.text = "Restock the shelves (" + $"{stepsDone}" + "/" + $"{stepsToDo}" + ")";
                        break;
                    case 1:
                        shelfQuestText.text = "Restock the shelves (" + $"{stepsDone}" + "/" + $"{stepsToDo}" + ")";
                        break;
                    case 2:
                        shelfQuestText.text = "Take a sip";
                        break;
                    case 3:
                        shelfQuestText.text = "Chug it all out";
                        break;               
                }
                break;
            
        }
            
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
        }
        
    }

    //Checks if all quests have been completed to unlock Scene Change
    private void CheckDayCompletion()
    {
        if (shelfQuestCompleted && floorQuestCompleted && !SM.isGameOver)
        {
            endDayText.SetActive(true);
        }
    }
}
