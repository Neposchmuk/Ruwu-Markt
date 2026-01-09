using System;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class SmashThings : MonoBehaviour
{
    public static Action OnDestroy;

    public GameObject Particles;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Destructible"))
        {
            Instantiate(Particles, other.transform.position, other.transform.rotation);
            Destroy(other.gameObject);
            OnDestroy?.Invoke();
        }
        else if (other.CompareTag("Enemy"))
        {
            GameEventsManager.instance.questEvents.HitEnemy();
            Instantiate(Particles, other.transform.position + new Vector3( 0,1,0), other.transform.rotation);
            Destroy(other.gameObject);
        }
    }
}
