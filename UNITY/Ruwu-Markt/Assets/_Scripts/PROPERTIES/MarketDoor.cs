using UnityEngine;
using System.Collections;

public class MarketDoor : MonoBehaviour
{
    [SerializeField] Animator animator;

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
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("NPC_Customer"))
        {
            if (!doorIsOpen)
            {
                animator.SetBool("OpenDoor", true);
            }
        }

        if (other.CompareTag("Player"))
        {
            if (allowPlayerLeave && !doorIsOpen)
            {
                animator.SetBool("OpenDoor", true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("NPC_Customer"))
        {
            if (doorIsOpen)
            {
                animator.SetBool("OpenDoor", false);
            }
        }

        if (other.CompareTag("Player"))
        {
            if (allowPlayerLeave && doorIsOpen)
            {
                animator.SetBool("OpenDoor", false);
            }
        }
    }
}
