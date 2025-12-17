using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class RayCast : MonoBehaviour
{
    public static Action OnMarketLeave;

    public static Action OnKeyPickup;

    public float rayLength;

    public LayerMask layerMask;

    private Hand_Actions Hand;

    private Camera mainCamera;

    private InputAction interact;

    private Quest_Manager QM;

    private Day_Manager _dayManager;

    public GameObject questObject;

    private bool _carryingCashtray;

    private bool _hasMarketKey;
    private void FindQM()
    {

    }

    private void Start()
    {
        mainCamera = Camera.main;

        interact = GameObject.Find("PlayerCapsule").GetComponent<PlayerInput>().actions.FindAction("Interact");

        _dayManager = GameObject.FindFirstObjectByType<Day_Manager>();

        try
        {
            QM = GameObject.Find("Quest_Manager").GetComponent<Quest_Manager>();
        }
        catch (NullReferenceException)
        {
            Debug.LogError("No QuestManager in scene, if NightScene ignore");
        }
        

        Hand = gameObject.GetComponentInChildren<Hand_Actions>();

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

            if(hit.collider.tag == "Cashtray" && interact.WasPressedThisFrame() && QM.DayComplete)
            {
                GameObject[] castrayObjects = GameObject.FindGameObjectsWithTag("Cashtray");
                foreach(GameObject _object in castrayObjects)
                {
                    _object.SetActive(false);
                }
                Hand.PickUpObject(8);
                _carryingCashtray = true;
                
            }

            if(hit.collider.tag == "Safe" && interact.WasPressedThisFrame() && _carryingCashtray)
            {
                Debug.Log("InteractSafe");
                QM.CompleteDay();
            }

            if(hit.collider.tag == "MarketKey" && interact.WasPressedThisFrame())
            {
                OnKeyPickup?.Invoke();
                _hasMarketKey = true;
                Destroy(hit.collider.gameObject);
            }

            if(hit.collider.tag == "MarketDoor" && interact.WasPressedThisFrame())
            {
                if (_hasMarketKey)
                {
                    OnMarketLeave?.Invoke();
                }
                else
                {
                    //Message Need key
                }
            }

            if(hit.collider.tag == "AmmoStation" && interact.WasPressedThisFrame())
            {
                AmmoStation _ammoStation = hit.collider.GetComponent<AmmoStation>();
                if (!_ammoStation.IsLocked)
                {
                    _ammoStation.AmmoPicked();
                }     
            }

            if(hit.collider.tag == "HomeDoor" && interact.WasPressedThisFrame() && _dayManager.IsDay)
            {
                SceneManager.LoadScene("Greyboxing_Day");
            }

            if(hit.collider.tag == "HomeMatress" && interact.WasPressedThisFrame() && !_dayManager.IsDay)
            {
                Debug.Log("Hit");
                _dayManager.AddDay();
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
