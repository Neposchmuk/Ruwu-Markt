using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Hand_Actions: MonoBehaviour
{
    public GameObject[] instanceObject;

    public Animator MopAnimator;

    public GameObject objectHolding;

    public float throwStrength;

    private float timeToPour = 5; 

    private GameObject objectToPlace;

    private Collider MopTrigger;

    //Instantiates Object on Ray hit position, if Instance Object is already present, increases size of Object
    public float Pour()
    {
        if(Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, 2))
        {
            if(hit.collider.tag == "PourPuddle")
            {
                hit.collider.gameObject.transform.localScale *= 1 + 0.5f * Time.deltaTime;
            }
            else
            {
                Instantiate(instanceObject[2], hit.point + new Vector3 (0,0.02f,0), Quaternion.Euler (new Vector3 (0, UnityEngine.Random.Range(0, 359), 0)));
            }

            timeToPour -= 1 * Time.deltaTime;
            
        }
        return timeToPour;
    }

    public bool FlowerPour(float timeToWater)
    {
        if(Physics.Raycast(transform.position + new Vector3(0,0.3f,0.3f), new Vector3(0,-1,1), out RaycastHit hit, 2))
        {
            if(hit.collider.tag == "Flowers")
            {
                timeToPour -= 1 * Time.deltaTime;
                if(timeToPour <= 0)
                {
                    return true;
                }              
            }
            else if (hit.collider.tag == "PourPuddle")
            {
                hit.collider.gameObject.transform.localScale *= 1 + 0.5f * Time.deltaTime;
                return false;
            }
            else
            {
                Instantiate(instanceObject[2], hit.point + new Vector3(0, 0.02f, 0), Quaternion.Euler(new Vector3(0, UnityEngine.Random.Range(0, 359), 0)));
                return false;
            }
        }

        return false;
    }

    public void Place(RaycastHit hit)
    {
        GameObject instanceObject = Instantiate(objectHolding, hit.point, transform.localRotation);
        instanceObject.layer = 0;

    }

    public void Place(Vector3 positionOverride, Vector3 rotationOverride, Vector3 scaleOverride)
    {
        GameObject instanceObject = Instantiate(objectHolding, positionOverride, Quaternion.Euler(rotationOverride));
        instanceObject.transform.localScale = scaleOverride;
        instanceObject.layer = 0;

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

    public GameObject ThrowObject(int PickUp)
    {
        Debug.Log("Called throw object");
        GameObject objectThrown = Instantiate(instanceObject[PickUp], transform.position, transform.rotation);
        try
        {
            Debug.Log("Trying to throw object");
            objectThrown.TryGetComponent<Rigidbody>(out Rigidbody thrownRB);
            thrownRB.AddForce(transform.forward * throwStrength, ForceMode.Impulse);
        }
        catch (NullReferenceException)
        {
            Debug.Log("Thrown Object has no Rigidbody");
        }

        return objectThrown;
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
