using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class RayCast : MonoBehaviour
{
    public float rayLength;

    public LayerMask layerMask;

    private Camera mainCamera;

    private InputAction interact;

    private Quest_Manager QM;

    private GameObject questObject;

    private GameObject instanceObject;

    private void FindQM()
    {

    }

    private void Start()
    {
        mainCamera = Camera.main;

        interact = GameObject.Find("PlayerCapsule").GetComponent<PlayerInput>().actions.FindAction("Interact");

        QM = GameObject.Find("Quest_Manager").GetComponent<Quest_Manager>();

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
                hit.collider.gameObject.GetComponent<Interaction_MenuTest>().Show_UI();
                questObject = hit.collider.gameObject;
                instanceObject = hit.collider.gameObject.GetComponent<Instantiate_Collection>().intanceObjects[0];
                //Debug.Log(QM.isDoingQuest);
            }
            else if(hit.collider.tag == "Shelf" && interact.WasPressedThisFrame() && QM.isDoingQuest && !QM.shelfQuestCompleted)
            {
                Instantiate(instanceObject, hit.point, transform.rotation);
                questObject.GetComponent<actionsScript>().PlaceObject();

            }

            /*if(hit.collider.tag == "shelfProduce" && interact.WasPressedThisFrame())
            {
                hit.collider.gameObject.GetComponent<Renderer>().material = hit.collider.GetComponent<Material_Collector>().Materials[1];
                questObject.GetComponent<actionsScript>().PlaceObject();
            }*/
        }
    }
}
