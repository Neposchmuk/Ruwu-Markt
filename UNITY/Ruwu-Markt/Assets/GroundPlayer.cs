using StarterAssets;
using UnityEngine;

public class GroundPlayer : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameObject.FindFirstObjectByType<FirstPersonController>().Grounded = true;
    }

}
