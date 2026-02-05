using System;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class SmashThings : MonoBehaviour
{
    public static Action OnDestroy;

    public GameObject ParticlesSmash;

    public GameObject ParticlesEnemy;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Destructible"))
        {
            GameEventsManager.instance.soundEvents.TriggerSound(SoundType.BAT_HIT);
            Instantiate(ParticlesSmash, other.transform.position, other.transform.rotation);
            Destroy(other.gameObject);
            OnDestroy?.Invoke();
        }
        else if (other.CompareTag("Enemy"))
        {
            GameEventsManager.instance.soundEvents.TriggerSound(SoundType.BAT_HIT);
            GameEventsManager.instance.questEvents.HitEnemy(other.gameObject);
            Instantiate(ParticlesEnemy, other.transform.position + new Vector3( 0,1,0), other.transform.rotation);
            Destroy(other.gameObject);
        }
    }
}
