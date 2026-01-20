using System;
using UnityEngine;

public class Trigger_NPC_Method : MonoBehaviour
{
    public bool CheckoutLine;

    public bool CheckoutSlot;

    public bool FinalDestination;

    [SerializeField] bool IsFinalSlot;

    public bool IsOccupied; //{ get; private set; }

    private Customer_Behaviour _agent;

    private void OnEnable()
    {
        if(!CheckoutLine) return;

        GameEventsManager.instance.checkoutEvents.onRequestSlotUpdate += SendSlotUpdate;
        GameEventsManager.instance.checkoutEvents.onReserveSlot += ReserveSlot;
    }

    private void OnDisable()
    {
        if(!CheckoutLine) return;

        GameEventsManager.instance.checkoutEvents.onRequestSlotUpdate -= SendSlotUpdate;
        GameEventsManager.instance.checkoutEvents.onReserveSlot -= ReserveSlot;
    }

    private void SendSlotUpdate(GameObject slot, GameObject agent)
    {
        if(slot != this.gameObject) return;

        GameEventsManager.instance.checkoutEvents.SendSlotUpdate(this.gameObject, IsOccupied, agent);

        Debug.Log(gameObject + " is Occupied: " + IsOccupied + " / parameter: " + slot);
    }

    private void ReserveSlot(GameObject slot)
    {
        if(slot != this.gameObject) return;

        IsOccupied = true;
    }

    void GetAgent(Collider other)
    {
        _agent = other.gameObject.GetComponent<Customer_Behaviour>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("NPC_Customer"))
        {
            if (CheckoutLine && CheckoutSlot)
            {
                GameEventsManager.instance.npcEvents.FaceDirection(other.gameObject, transform.rotation);

                GameEventsManager.instance.checkoutEvents.StartCheckoutGame(other.gameObject);
                GameEventsManager.instance.checkoutEvents.RecalculateCheckoutSlot(other.gameObject);
                Debug.Log("Sent Checkout event");
            }
            else if(CheckoutLine && IsFinalSlot)
            {
                GameEventsManager.instance.checkoutEvents.EnteredCheckoutLine(other.gameObject);
            }
            else if (FinalDestination)
            {
                GameEventsManager.instance.checkoutEvents.KillAgent(other.gameObject);
                Debug.Log("Sent kill event");
            }
            else if (!CheckoutLine)
            {
                IsOccupied = true;

                GameEventsManager.instance.npcEvents.FaceDirection(other.gameObject, transform.rotation);

                GameEventsManager.instance.checkoutEvents.SetNPCTrigger(other.gameObject, true);
                GameEventsManager.instance.checkoutEvents.ArrivedAtTarget(other.gameObject);
            }
        }   
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("NPC_Customer"))
        {
            IsOccupied = false;

            GameEventsManager.instance.checkoutEvents.SetNPCTrigger(other.gameObject, false);

            if (CheckoutLine && CheckoutSlot)
            {
                GameEventsManager.instance.checkoutEvents.MoveUpNPCs();
            }
        }
        
    }
}
