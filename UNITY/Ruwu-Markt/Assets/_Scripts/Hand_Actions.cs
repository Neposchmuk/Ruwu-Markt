using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Hand_Actions: MonoBehaviour
{
    public GameObject[] instanceObject;

    public Animator MopAnimator;

    private float timeToPour = 5;

    private GameObject objectHolding;

    private GameObject objectToPlace;

    private Collider MopTrigger;

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
        Instantiate(objectHolding, hit.point, transform.localRotation);
    }

    public GameObject PickUpObject(int PickUp)
    {
        objectHolding = Instantiate(instanceObject[PickUp], transform.position, transform.rotation, gameObject.transform);
        objectToPlace = instanceObject[PickUp];

        return objectHolding;
    }

    public GameObject PickUpObject(int PickUp, Vector3 RotationOverride)
    {
        objectHolding = Instantiate(instanceObject[PickUp], transform.position, Quaternion.Euler(transform.eulerAngles + RotationOverride), gameObject.transform);
        objectToPlace = instanceObject[PickUp];

        return objectHolding;
    }

    public void DestroyObjectInHand()
    {
        Destroy(objectHolding);
    }

    public void Clean()
    {
        MopAnimator = objectHolding.GetComponent<Animator>();

    }
}
