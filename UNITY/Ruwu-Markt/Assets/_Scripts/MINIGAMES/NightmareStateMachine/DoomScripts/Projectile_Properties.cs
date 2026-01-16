using UnityEngine;

public class Projectile_Properties : MonoBehaviour
{
    public ParticleSystem Particles;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            GameEventsManager.instance.questEvents.HitEnemy();
        }
        else if (!collision.gameObject.CompareTag("Gun"))
        {
            Instantiate(Particles, transform.position, transform.rotation);
            Destroy(gameObject);
        }
            Debug.Log(gameObject + "Collided" + collision.gameObject);   
    }
}
