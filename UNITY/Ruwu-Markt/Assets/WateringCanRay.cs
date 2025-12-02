using UnityEngine;

public class WateringCanRay : MonoBehaviour
{
    public Ray ray;

    public bool drawRay = false;

    void Update()
    {
        if (drawRay)
        {
            ray = new Ray(transform.position, transform.up);
        }
    }
    
}
