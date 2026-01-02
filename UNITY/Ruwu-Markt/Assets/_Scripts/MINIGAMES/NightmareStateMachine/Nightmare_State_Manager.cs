using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem.XR.Haptics;

public class Nightmare_State_Manager : MonoBehaviour
{
    public static Action OnTimerUpdate;

    public GameObject JumpLevel;

    public GameObject SmashLevel;

    public GameObject EscapeLevel;

    public GameObject DoomLevel;

    public NightmareBaseState CurrentState;
    public Nightmare_Escape_State EscapeState = new Nightmare_Escape_State();
    public Nightmare_Doom_State DoomState = new Nightmare_Doom_State();
    public Nightmare_Smash_State SmashState = new Nightmare_Smash_State();
    public Nightmare_Jump_State JumpState = new Nightmare_Jump_State();

    Day_Manager _dayManager;

    Sanity_Manager _sanityManager;

    public int TimeInSeconds { get; private set; }

    public bool TimerIsRunning { get; private set; }

    private void Awake()
    {
        _dayManager = FindFirstObjectByType<Day_Manager>();

        _sanityManager = FindFirstObjectByType<Sanity_Manager>();

        switch (_dayManager.Night)
        {
            case 1:
                JumpLevel.SetActive(true);
                CurrentState = JumpState;
                TimeInSeconds = 60;
                StartCoroutine(DelayTimer(5));
                break;
            case 2:
                SmashLevel.SetActive(true);
                CurrentState = SmashState;
                break;
            case 3:
                DoomLevel.SetActive(true);
                CurrentState = DoomState;
                break;
            case 4:
                EscapeLevel.SetActive(true);
                CurrentState = EscapeState;
                break;
        }

        CurrentState.EnterState(this);
        Debug.Log("StateManager EnterState");
    }

    // Update is called once per frame
    void Update()
    {
        CurrentState.UpdateState();
    }

    public void EndNight(bool survived, int sanityChange)
    {
        Debug.Log("Called EndNight");
        if(TimerIsRunning) CancelInvoke("Timer");

        if(_sanityManager.sanity + sanityChange <= 0)
        {
            sanityChange -= _sanityManager.sanity + sanityChange + 1;
        }
        _sanityManager.ChangeSanity(sanityChange, 0);

        _dayManager.AddNight();
    }

    IEnumerator DelayTimer(int timeToDelay)
    {
        yield return new WaitForSeconds(timeToDelay);

        InvokeRepeating("Timer", 0, 1);
    }

    void Timer()
    {
        if(!TimerIsRunning)
        TimerIsRunning = true;

        TimeInSeconds--;

        if(CurrentState == JumpState)
        {
            OnTimerUpdate?.Invoke();
        }

        if(TimeInSeconds == 0)
        {
            EndNight(true, 10);
        }  
    }
}
