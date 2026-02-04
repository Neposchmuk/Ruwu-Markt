using UnityEngine;
using UnityEngine.Rendering;

public class EscapeEffectsController : MonoBehaviour
{
    [SerializeField] GameObject Enemy;

    [SerializeField] GameObject Player;

    [SerializeField] Volume volume;

    private Vector3 distance;

    void Update()
    {
        distance = Player.transform.position - Enemy.transform.position;

        volume.weight = Mathf.Lerp(0, 1, 1 - (distance.magnitude/10));
    }

}
