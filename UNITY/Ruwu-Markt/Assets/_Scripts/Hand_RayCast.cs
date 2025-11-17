using UnityEngine;
using UnityEngine.InputSystem;

public class Hand_Raycast: MonoBehaviour
{
    private InputAction LMB;

    private InputAction KeyE;

    private Quest_Manager QM;

    public GameObject PlayerCapsule;

    public GameObject[] instanceObject;

    public LayerMask layerMask;

    public bool rayIsActive;

    private float timeToPour = 5;

    private int questType;

    private int questVariant;

    public GameObject objectHolding;

    private GameObject questObject;

    private RayCast PlayerRC;

    private bool pourQuestActive;

    private bool placeQuestActive;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        LMB = GameObject.Find("PlayerCapsule").GetComponent<PlayerInput>().actions.FindAction("Attack");

        KeyE = GameObject.Find("PlayerCapsule").GetComponent<PlayerInput>().actions.FindAction("Interact");

        QM = GameObject.Find("Quest_Manager").GetComponent<Quest_Manager>();

        PlayerRC = gameObject.GetComponentInParent<RayCast>();

        Debug.DrawRay(transform.position, Vector3.down, Color.green);

        objectHolding = instanceObject[0];
    }

    // Update is called once per frame
    void Update()
    {
        /*if(QM.isDoingQuest && (pourQuestActive || placeQuestActive))
        {
            UnlockRays();
        }*/
        if (QM.isDoingQuest && pourQuestActive)
        {
            if (rayIsActive && timeToPour > 0)
            {
                Pour();
            }
            else if (rayIsActive)
            {
                rayIsActive = false;
                pourQuestActive = false;

                QM.CompleteQuest(questType, questVariant, questObject);
            }
        }

        /*if(QM.isDoingQuest && placeQuestActive)
        {
            Place();
        }*/

    }

    //Instantiates Object on Ray hit position, if Instance Object is already present, increases size of Object
    private void Pour()
    {
        if(Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, 2) && LMB.IsPressed())
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
    }

    private void Place()
    {
        if(Physics.Raycast(Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0.5f)), out RaycastHit hit, 2, layerMask))
        {
            if(hit.collider.gameObject.tag == "Shelf" && KeyE.WasPressedThisFrame() /*&& QM.isDoingQuest && !QM.shelfQuestCompleted*/)
            {
                Instantiate(objectHolding, hit.point, transform.rotation);
                questObject.GetComponent<actionsScript>().PlaceObject();
            }
        }
    }

    public void UnlockPour(int i, int j, GameObject qO)
    {
        questObject = qO;
        pourQuestActive = true;
        rayIsActive = true;
        questType = i;
        questVariant = j;
    }

    public void PickUpObject(int PickUp)
    {
        Debug.Log("Called PickUpObject()");
        questObject = PlayerRC.questObject;
        Debug.Log("Set questObject");
        switch (PickUp)
        {
            case 0:
                Instantiate(instanceObject[PickUp], transform.position, transform.rotation, gameObject.transform);
                objectHolding = instanceObject[PickUp];
                placeQuestActive = true;
                Debug.Log("Instantiated Object");
                break;
        }    

    }

    private void ThrowObject()
    {

    }    
    
}
