using System;
using UnityEngine;

public class Clean_Puddles : MonoBehaviour
{
    public static event Action OnPuddleDestroy;

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Mop"))
        {
            transform.localScale *= 0.8f;
            if(transform.localScale.magnitude < 0.17f)
            {
                OnPuddleDestroy?.Invoke();
                Destroy(gameObject);
            }
        }
    }

}
