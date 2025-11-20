using StarterAssets;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Rendering;

public class FacePlayer : MonoBehaviour
{
    private GameObject Player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Player = FindFirstObjectByType<FirstPersonController>().gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.activeSelf)
        {
            RotateToTarget();
        }

    }

    //Rotates Object toward Player without changing y Rotation https://discussions.unity.com/t/rotate-only-y-axis-of-some-object-toward-another/58155
    private void RotateToTarget()
    {
        Vector3 PlayerDirection = new Vector3(Player.transform.position.x, transform.position.y, Player.transform.position.z);

        transform.LookAt(PlayerDirection);

        //Quaternion LookDirection = Quaternion.LookRotation(PlayerDirection);

        //Debug.Log(LookDirection.y);

        //transform.rotation = Quaternion.Euler(PlayerDirection);
    }
}
