using System;
using UnityEngine;

public class Trigger_NPC_Method : MonoBehaviour
{
    public static Action OnCheckoutLeave;

    public bool CheckoutLine;

    public bool FinalSlot;

    public bool FinalDestination;

    public bool IsOccupied { get; private set; }

    private Customer_Behaviour _agent;

    private void Start()
    {
        CashRegister_MiniGame.OnPay += SetAgentFinalDestination;
    }

    void GetAgent(Collider other)
    {
        _agent = other.gameObject.GetComponent<Customer_Behaviour>();
    }

    void SetAgentFinalDestination()
    {
        Debug.Log(_agent);
        if (_agent != null) _agent.FinalDestination();  
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("NPC_Customer"))
        {
            IsOccupied = true;

            other.GetComponent<Customer_Behaviour>().IsInTrigger = true;

            GetAgent(other);

            Debug.Log(_agent);

            if (CheckoutLine && FinalSlot)
            {
                other.GetComponent<Customer_Behaviour>().StartCheckoutGame();
            }
            else if (CheckoutLine)
            {
                other.GetComponent<Customer_Behaviour>().CheckSlotAhead();
            }
            else if (FinalDestination)
            {
                other.GetComponent<Customer_Behaviour>().Kill();
            }
            else
            {
                other.GetComponent<Customer_Behaviour>().CheckFinalDestination();
            }
        }   
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("NPC_Customer"))
        {
            IsOccupied = false;

            other.GetComponent<Customer_Behaviour>().IsInTrigger = false;

            if (CheckoutLine && FinalSlot)
            {
                OnCheckoutLeave?.Invoke();
            }
        }
        
    }
}
