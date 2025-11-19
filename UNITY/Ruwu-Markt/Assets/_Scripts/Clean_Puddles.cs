using System;
using UnityEngine;

public class Clean_Puddles : MonoBehaviour
{
    public static event Action OnPuddleDestroy;

    private void OnTriggerStay(Collider other)
    {
        Debug.Log("Colliderentered");
        if (other.CompareTag("Mop"))
        {
            Debug.Log("Mop entered");
            transform.localScale -= new Vector3(0.1f, 0.1f, 0.1f) * Time.deltaTime;
            if(transform.localScale.magnitude < 0.03f)
            {
                OnPuddleDestroy?.Invoke();
                Destroy(gameObject);
            }
        }
    }

}
