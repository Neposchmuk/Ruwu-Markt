using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Hand_Actions: MonoBehaviour
{
    public GameObject[] instanceObject;

    public Animator MopAnimator;

    public GameObject objectHolding;

    public float throwStrength;

    private float timeToPour = Mathf.Clamp(5, 0, 5); 

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
                Instantiate(instanceObject[1], hit.point + new Vector3 (0,0.02f,0), Quaternion.Euler (new Vector3 (0, UnityEngine.Random.Range(0, 359), 0)));
            }

            timeToPour -= 1 * Time.deltaTime;
            
        }
        return timeToPour;
    }

    public void FlowerPour()
    { 
        if(Physics.Raycast(Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0.5f)), out RaycastHit hit, 2))
        {
            if(hit.collider.tag == "Flowers")
            {
                if(timeToPour >= 0)
                {
                    hit.collider.gameObject.GetComponent<FlowersWatering>().AddWaterSaturation();
                    timeToPour -= 1 * Time.deltaTime;
                }              
            }
            else if (hit.collider.tag == "PourPuddle" && timeToPour >= 0)
            {
                hit.collider.gameObject.transform.localScale *= 1 + 0.5f * Time.deltaTime;
                timeToPour -= 1 * Time.deltaTime;
            }
            else if (timeToPour >= 0)
            {
                Instantiate(instanceObject[1], hit.point + new Vector3(0, 0.02f, 0), Quaternion.Euler(new Vector3(0, UnityEngine.Random.Range(0, 359), 0)));
                timeToPour -= 1 * Time.deltaTime;
            }

            
        }

        objectHolding.GetComponentInChildren<TMP_Text>().text = $"{Mathf.CeilToInt(timeToPour * 20f)}";
    }

    public void SetPourTime(float time)
    {
        timeToPour = time;
        objectHolding.GetComponentInChildren<TMP_Text>().text = $"{Mathf.CeilToInt(timeToPour * 20f)}";
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
