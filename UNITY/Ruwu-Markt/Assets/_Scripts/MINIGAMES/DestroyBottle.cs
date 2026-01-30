using UnityEngine;

public class DestroyBottle : MonoBehaviour
{
    [SerializeField] private ParticleSystem particles;
    private bool canExplode;

    void OnEnable()
    {
        GameEventsManager.instance.questEvents.onAllowBottleExplode += AllowExplode;
    }

    void OnDisable()
    {
        GameEventsManager.instance.questEvents.onAllowBottleExplode -= AllowExplode;
    }

    void AllowExplode(GameObject bottle)
    {
        if(bottle != this.gameObject) return;

        canExplode = true;
    }

    void OnCollisionEnter(Collision collision)
    {
        if(!canExplode) return;

        Instantiate(particles, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
