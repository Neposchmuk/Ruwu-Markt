using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ProBuilder.MeshOperations;

public class CheckoutSlotManager : MonoBehaviour
{
    [SerializeField] List<Trigger_NPC_Method> checkoutSlots;

    [SerializeField] Trigger_NPC_Method registerSlot;

    [SerializeField] Trigger_NPC_Method lastSlot;

    [SerializeField] bool checkoutSlot0_occupied;

    [SerializeField] bool checkoutSlot1_occupied;

    [SerializeField] bool checkoutSlot2_occupied;

    [SerializeField] bool checkoutSlot3_occupied;

    [SerializeField] bool checkoutSlot4_occupied;

    //For testing purposes, to check if slots get set as occupied
    [SerializeField] List<Trigger_NPC_Method> occupiedSlots;

    private void OnEnable()
    {
        GameEventsManager.instance.checkoutEvents.onRequestCheckoutSlot += CheckSlots;
    }
    private void OnDisable()
    {
        GameEventsManager.instance.checkoutEvents.onRequestCheckoutSlot -= CheckSlots;
    }

    private void CheckSlots(GameObject agent)
    {
        for(int i = 0; i < checkoutSlots.Count; i++)
        {
            if (!registerSlot.IsOccupied)
            {
                GameEventsManager.instance.checkoutEvents.ReserveSlot(registerSlot.gameObject);

                if(registerSlot.IsOccupied) occupiedSlots.Add(registerSlot);

                GameEventsManager.instance.checkoutEvents.SendCheckoutSlot(agent, registerSlot.gameObject, 0);

                Debug.Log(agent +": is moving to slot: " + registerSlot);

                return;
            }
            else if (!checkoutSlots[i].IsOccupied)
            {
                GameEventsManager.instance.checkoutEvents.ReserveSlot(checkoutSlots[i].gameObject);

                if(checkoutSlots[i].IsOccupied) occupiedSlots.Add(checkoutSlots[i]);

                GameEventsManager.instance.checkoutEvents.SendCheckoutSlot(agent, checkoutSlots[i].gameObject, i+1);

                Debug.Log(agent +": is moving to slot: " + checkoutSlots[i].gameObject);

                return;
            }
        }

        Debug.LogError("All slots came in as occupied");
    }

    private void Update()
    {
        checkoutSlot0_occupied = registerSlot.IsOccupied;

        checkoutSlot1_occupied = checkoutSlots[0].IsOccupied;

        checkoutSlot2_occupied = checkoutSlots[1].IsOccupied;

        checkoutSlot3_occupied = checkoutSlots[2].IsOccupied;

        checkoutSlot4_occupied = checkoutSlots[3].IsOccupied;
    }
}
