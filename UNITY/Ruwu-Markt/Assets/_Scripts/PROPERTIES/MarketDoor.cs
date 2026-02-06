using UnityEngine;
using System.Collections;

public class MarketDoor : MonoBehaviour
{
    [SerializeField] Animator animator;

    [SerializeField] BoxCollider playerBarrier;

    private bool doorIsOpen;

    private bool allowPlayerLeave;

    private void OnEnable()
    {
        GameEventsManager.instance.questEvents.onAllowPlayerLeave += SetLeaveBool;
    }
    private void OnDisable()
    {
        GameEventsManager.instance.questEvents.onAllowPlayerLeave -= SetLeaveBool;
    }

    private void SetLeaveBool()
    {
        allowPlayerLeave = true;

        playerBarrier.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("NPC_Customer"))
        {
            if (!doorIsOpen)
            {
                animator.SetBool("IsOpen", true);
                doorIsOpen = true;
            }
        }

        if (other.CompareTag("Player"))
        {
            if (allowPlayerLeave && !doorIsOpen)
            {
                animator.SetBool("IsOpen", true);
                doorIsOpen = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("NPC_Customer"))
        {
            if (doorIsOpen)
            {
                animator.SetBool("IsOpen", false);
                doorIsOpen = false;
            }
        }

        if (other.CompareTag("Player"))
        {
            if (allowPlayerLeave && doorIsOpen)
            {
                animator.SetBool("IsOpen", false);
                doorIsOpen = false;
            }
        }
    }
}
