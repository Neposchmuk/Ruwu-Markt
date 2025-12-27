using System;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.DeviceSimulation;
using UnityEngine;

public class Nightmare_Jump_State : NightmareBaseState
{
    Nightmare_State_Manager _stateManager;

    TMP_Text _timer;

    int _minutes;

    int _seconds;


    public override void EnterState(Nightmare_State_Manager stateManager)
    {
        Debug.Log("Entered Jump State");
        _stateManager = stateManager;

        _timer = GameObject.FindGameObjectWithTag("JumpNightmareTimer").GetComponent<TMP_Text>();

        TouchGroundEvent.OnTouchedGround += EndState;

        Nightmare_State_Manager.OnTimerUpdate += UpdateTimer;
    }

    public override void UpdateState()
    {
        //throw new NotImplementedException();
    }

    public override void EndState()
    {
        Debug.Log("Called EndState");
        TouchGroundEvent.OnTouchedGround -= EndState;
        Nightmare_State_Manager.OnTimerUpdate -= UpdateTimer;
        _stateManager.EndNight(false, -10);
    }

    public void UpdateTimer()
    {
        if (_timer == null) return;

        _minutes = Mathf.FloorToInt(_stateManager.TimeInSeconds / 60);

        _seconds = Mathf.FloorToInt(_stateManager.TimeInSeconds - _minutes * 60);

        _timer.text = string.Format("{0:0}:{1:00}", _minutes, _seconds);

    }
}
