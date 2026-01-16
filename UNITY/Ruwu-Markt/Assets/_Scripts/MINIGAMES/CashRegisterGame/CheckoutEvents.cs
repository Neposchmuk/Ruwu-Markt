using UnityEngine;
using System;
using Unity.VisualScripting;

public class CheckoutEvents
{
    public event Action<GameObject, GameObject> onRequestSlotUpdate;

    public void RequestSlotUpdate(GameObject slot, GameObject agent)
    {
        if (onRequestSlotUpdate != null)
        {
            onRequestSlotUpdate(slot, agent);
        }
    }

    public event Action<GameObject, bool, GameObject> onSendSlotUpdate;

    public void SendSlotUpdate(GameObject slot, bool occupied, GameObject agent)
    {
        if(onSendSlotUpdate != null)
        {
            onSendSlotUpdate(slot, occupied, agent);
        }
    }

    public event Action<GameObject> onReserveSlot;

    public void ReserveSlot(GameObject slot)
    {
        if(onRequestSlotUpdate != null)
        {
            onReserveSlot(slot);
        }
    }

    public event Action<GameObject> onArrivedAtTarget;

    public void ArrivedAtTarget(GameObject agent)
    {
        if(onArrivedAtTarget != null)
        {
            onArrivedAtTarget(agent);
        }
    }

    public event Action<GameObject> onKillAgent;

    public void KillAgent(GameObject agent)
    {
        if(onKillAgent != null)
        {
            onKillAgent(agent);
        }
    }

    public event Action<GameObject> onStartCheckoutGame;

    public void StartCheckoutGame(GameObject agent)
    {
        if(onStartCheckoutGame != null)
        {
            onStartCheckoutGame(agent);
        }
    }

    public event Action<GameObject> onPay;

    public void Pay(GameObject agent)
    {
        if(onPay != null)
        {
            onPay(agent);
        }
    }

    public event Action<GameObject, bool> onSetNPCTrigger;

    public void SetNPCTrigger(GameObject agent, bool inTrigger)
    {
        if(onSetNPCTrigger != null)
        {
            onSetNPCTrigger(agent, inTrigger);
        }
    }

    public event Action onMoveUpNPCs;
    public void MoveUpNPCs()
    {
        if(onMoveUpNPCs != null)
        {
            onMoveUpNPCs();
        }
    }

    public event Action<GameObject> onEnteredCheckoutLine;
    public void EnteredCheckoutLine(GameObject agent)
    {
        if(onEnteredCheckoutLine != null)
        {
            onEnteredCheckoutLine(agent);
        }
    }
}

