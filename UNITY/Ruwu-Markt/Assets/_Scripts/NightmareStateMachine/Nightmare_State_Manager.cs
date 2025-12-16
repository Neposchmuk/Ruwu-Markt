using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem.XR.Haptics;

public class Nightmare_State_Manager : MonoBehaviour
{
    public GameObject JumpLevel;

    public GameObject SmashLevel;

    public GameObject EscapeLevel;

    public NightmareBaseState CurrentState;
    public Nightmare_Escape_State EscapeState = new Nightmare_Escape_State();
    public Nightmare_Checkout_State CheckoutState = new Nightmare_Checkout_State();
    public Nightmare_Smash_State SmashState = new Nightmare_Smash_State();
    public Nightmare_Jump_State JumpState = new Nightmare_Jump_State();

    Day_Manager _dayManager;

    Sanity_Manager _sanityManager;

    int _timeInSeconds;

    public bool TimerIsRunning { get; private set; }

    private void Awake()
    {
        _dayManager = FindFirstObjectByType<Day_Manager>();

        _sanityManager = FindFirstObjectByType<Sanity_Manager>();

        switch (_dayManager.Night)
        {
            case 1:
                CurrentState = JumpState;
                _timeInSeconds = 90;
                StartCoroutine(DelayTimer(5));
                break;
            case 2:
                CurrentState = SmashState;
                break;
            case 3:
                CurrentState = EscapeState;
                break;
            case 4:
                CurrentState = EscapeState;
                break;
        }

        CurrentState.EnterState(this);
    }

    // Update is called once per frame
    void Update()
    {
        CurrentState.UpdateState();
    }

    public void EndNight(bool survived, int sanityChange)
    {
        if(TimerIsRunning) StopCoroutine(Timer());

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

        StartCoroutine(Timer());
    }

    IEnumerator Timer()
    {
        if(!TimerIsRunning)
        TimerIsRunning = true;

        yield return new WaitForSeconds(1);

        _timeInSeconds--;

        if(_timeInSeconds == 0)
        {
            EndNight(true, 10);
        }
    }
}
