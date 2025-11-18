using UnityEngine;
using UnityEngine.InputSystem;

public class Hand_Raycast: MonoBehaviour
{
    public GameObject[] instanceObject;

    private float timeToPour = 5;

    private GameObject objectHolding;

    //Instantiates Object on Ray hit position, if Instance Object is already present, increases size of Object
    public float Pour()
    {
        if(Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, 2))
        {
            if(hit.collider.tag == "PourPuddle")
            {
                hit.collider.gameObject.transform.localScale *= 1 + 0.25f * Time.deltaTime;
            }
            else
            {
                Instantiate(instanceObject[1], hit.point + new Vector3 (0,0.02f,0), Quaternion.Euler (new Vector3 (0, Random.Range(0, 359), 0)));
            }

            timeToPour -= 1 * Time.deltaTime;
            
        }
        return timeToPour;
    }

    public void Place(RaycastHit hit)
    {
        Instantiate(objectHolding, hit.point, transform.rotation);
    }

    public void PickUpObject(int PickUp)
    {
        Instantiate(instanceObject[PickUp], transform.position, transform.rotation, gameObject.transform);
        objectHolding = instanceObject[PickUp];
    }      
}
