using System;
using UnityEngine;

public class TouchGroundEvent : MonoBehaviour
{
    public static Action OnTouchedGround;


    Nightmare_State_Manager _stateManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _stateManager = FindFirstObjectByType<Nightmare_State_Manager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Entered Trigger");
        if (other.CompareTag("Player") && _stateManager.TimerIsRunning)
        {
            OnTouchedGround?.Invoke();
        }
    }
}
