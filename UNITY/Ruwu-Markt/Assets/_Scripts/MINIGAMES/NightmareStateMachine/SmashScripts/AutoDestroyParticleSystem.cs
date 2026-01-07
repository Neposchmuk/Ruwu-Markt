using UnityEngine;

public class AutoDestroyParticleSystem : MonoBehaviour
{
    ParticleSystem _particleSystem;
    private void Start()
    {
        _particleSystem = GetComponent<ParticleSystem>();
    }

    void Update()
    {
        if(_particleSystem != null)
        {
            if (!_particleSystem.IsAlive())
            {
                Destroy(gameObject);
            }
        }
    }
}
