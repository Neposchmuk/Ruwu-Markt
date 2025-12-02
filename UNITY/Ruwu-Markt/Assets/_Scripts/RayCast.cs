using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class RayCast : MonoBehaviour
{
    public float rayLength;

    public LayerMask layerMask;

    private Hand_Actions HandRC;

    private Camera mainCamera;

    private InputAction interact;

    private Quest_Manager QM;

    public GameObject questObject;


    

    private void FindQM()
    {

    }

    private void Start()
    {
        mainCamera = Camera.main;

        interact = GameObject.Find("PlayerCapsule").GetComponent<PlayerInput>().actions.FindAction("Interact");

        QM = GameObject.Find("Quest_Manager").GetComponent<Quest_Manager>();

        HandRC = gameObject.GetComponentInChildren<Hand_Actions>();

    }

    private void Update()
    {
        Raycast();
    }

    private void Raycast()
    {
        Ray ray = mainCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0.5f));

        if(Physics.Raycast(ray, out RaycastHit hit, rayLength, layerMask))
        {
            if(hit.collider.tag == "Shelf" && interact.WasPressedThisFrame() && !QM.isDoingQuest && !QM.shelfQuestCompleted)
            {
                hit.collider.gameObject.GetComponent<Interaction_MenuTest>().ToggleUI(true);
                questObject = hit.collider.gameObject;
                Debug.Log(questObject.name);
                //Debug.Log(QM.isDoingQuest);
            }

            if (hit.collider.tag == "FloorQuest" && interact.WasPressedThisFrame() && !QM.isDoingQuest && !QM.floorQuestCompleted)
            {
                hit.collider.gameObject.GetComponent<Interaction_MenuTest>().ToggleUI(true);
                questObject = hit.collider.gameObject;
                Debug.Log(questObject.name);
                //Debug.Log(QM.isDoingQuest);
            }

            if (hit.collider.tag == "PfandQuest" && interact.WasPressedThisFrame() && !QM.isDoingQuest && !QM.pfandQuestCompleted)
            {
                hit.collider.gameObject.GetComponent<Interaction_MenuTest>().ToggleUI(true);
                questObject = hit.collider.gameObject;
                Debug.Log(questObject.name);
                //Debug.Log(QM.isDoingQuest);
            }

            if(hit.collider.tag == "FlowersQuest" && interact.WasPressedThisFrame() && !QM.isDoingQuest && !QM.flowersQuestCompleted)
            {
                hit.collider.gameObject.GetComponent<Interaction_MenuTest>().ToggleUI(true);
                questObject = hit.collider.gameObject;
                Debug.Log(questObject.name);

            }

            /*if(hit.collider.tag == "ProduceCan" && QM.isDoingQuest && !QM.shelfQuestCompleted)
            {
                Debug.Log("RayCast Hit");
                Debug.Log(HandRC.name);
                if (interact.WasPressedThisFrame())
                {
                    Destroy(hit.collider.gameObject);
                    Debug.Log("RayCast executed");
                    HandRC.PickUpObject(0);
                }
                
            }*/
            /*else if(hit.collider.tag == "Shelf" && interact.WasPressedThisFrame() && QM.isDoingQuest && !QM.shelfQuestCompleted)
            {
                Instantiate(instanceObject, hit.point, transform.rotation);
                questObject.GetComponent<actionsScript>().PlaceObject();

            }*/

            /*if(hit.collider.tag == "shelfProduce" && interact.WasPressedThisFrame())
            {
                hit.collider.gameObject.GetComponent<Renderer>().material = hit.collider.GetComponent<Material_Collector>().Materials[1];
                questObject.GetComponent<actionsScript>().PlaceObject();
            }*/
        }
    }
}
