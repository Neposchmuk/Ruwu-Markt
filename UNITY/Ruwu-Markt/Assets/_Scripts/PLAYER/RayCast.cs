using System;
using Unity.VisualScripting;
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

    private bool showInteraction;

    private bool showAction;

    private void OnEnable()
    {
        GameEventsManager.instance.playerEvents.onPressedInteract += Raycast;
        Debug.Log("Added RC Listener");
    }

    private void OnDisable()
    {
        GameEventsManager.instance.playerEvents.onPressedInteract -= Raycast;
        Debug.Log("Removed RC listener");
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
        WidgetRaycast();
    }

    private void Raycast(InputEventContext inputContext)
    {
        if (inputContext == InputEventContext.DEFAULT)
        {
            Ray ray = mainCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0.5f));

            if(Physics.Raycast(ray, out RaycastHit hit, rayLength, layerMask))
            {
                if(hit.collider.tag == "ShelfQuest" && !QM.isDoingQuest && !QM.shelfQuestCompleted)
                {
                    hit.collider.gameObject.GetComponent<Interaction_MenuTest>().ToggleUI(true);
                    questObject = hit.collider.gameObject;
                    Debug.Log(questObject.name);
                    //Debug.Log(QM.isDoingQuest);
                }

                if (hit.collider.tag == "FloorQuest" && !QM.isDoingQuest && !QM.floorQuestCompleted)
                {
                    hit.collider.gameObject.GetComponent<Interaction_MenuTest>().ToggleUI(true);
                    questObject = hit.collider.gameObject;
                    Debug.Log(questObject.name);
                    //Debug.Log(QM.isDoingQuest);
                }

                if (hit.collider.tag == "PfandQuest" && !QM.isDoingQuest && !QM.pfandQuestCompleted)
                {
                    hit.collider.gameObject.GetComponent<Interaction_MenuTest>().ToggleUI(true);
                    questObject = hit.collider.gameObject;
                    Debug.Log(questObject.name);
                    //Debug.Log(QM.isDoingQuest);
                }

                if(hit.collider.tag == "FlowersQuest" && !QM.isDoingQuest && !QM.flowersQuestCompleted)
                {
                    hit.collider.gameObject.GetComponent<Interaction_MenuTest>().ToggleUI(true);
                    questObject = hit.collider.gameObject;
                    Debug.Log(questObject.name);
                }

                if(hit.collider.tag == "Cashtray" && QM.DayComplete)
                {
                    GameObject[] castrayObjects = GameObject.FindGameObjectsWithTag("Cashtray");
                    foreach(GameObject _object in castrayObjects)
                    {
                        _object.SetActive(false);
                    }
                    Hand.PickUpObject(8);
                    _carryingCashtray = true;               
                }

                if(hit.collider.tag == "Safe" && _carryingCashtray)
                {
                    GameEventsManager.instance.soundEvents.TriggerSound(SoundType.SAFE);
                    Hand.DestroyObjectInHand();
                    QM.CompleteDay();
                }

                if(hit.collider.tag == "HomeDoor")
                {
                    if(_dayManager.IsDay && _dayManager.CheckedPC)
                    {
                        GameEventsManager.instance.soundEvents.TriggerSound(SoundType.DOOR_OPEN);
                        GameEventsManager.instance.gameEvents.ChangeScene("Greyboxing_Day");
                    }
                    else
                    {
                        GameEventsManager.instance.soundEvents.TriggerSound(SoundType.DOOR_LOCKED);
                    }
                    
                }

                if(hit.collider.tag == "HomeMatress" && !_dayManager.IsDay)
                {
                    GameEventsManager.instance.soundEvents.TriggerSound(SoundType.MATRESS);
                    Debug.Log("Hit");
                    _dayManager.AddDay();
                }

                if(hit.collider.tag == "Computer")
                {
                    GameEventsManager.instance.soundEvents.TriggerSound(SoundType.PC_CLICK);
                    PC_Interaction _pcInteraction = hit.collider.GetComponent<PC_Interaction>();
                    _pcInteraction.OpenInbox();
                }

                if(hit.collider.tag == "NPC_Boss")
                {
                    EnterDialogue _enterDialogue;
                    hit.collider.TryGetComponent<EnterDialogue>(out _enterDialogue);
                    if (_enterDialogue == null) Debug.LogWarning("NPC has no EnterDialogue Script!");
                    else _enterDialogue.SendDialogueEvent();
                }


                //Possibly add the following interactions to different InputEventContext
                if (hit.collider.CompareTag("UI_Button"))
                {
                    GameEventsManager.instance.questEvents.UIButtonInteract(hit.collider.gameObject);
                }   
            }
        }

        if (inputContext != InputEventContext.DEFAULT && inputContext != InputEventContext.DIALOGUE && inputContext != InputEventContext.UI)
        {
            Ray ray = mainCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0.5f));

            if(Physics.Raycast(ray, out RaycastHit hit, rayLength, layerMask))
            {
        
                if(hit.collider.tag == "MarketKey")
                {
                    GameEventsManager.instance.soundEvents.TriggerSound(SoundType.PICKUP);
                    OnKeyPickup?.Invoke();
                    _hasMarketKey = true;
                    Destroy(hit.collider.gameObject);
                }

                if(hit.collider.tag == "MarketDoor")
                {
                    if (_hasMarketKey)
                    {
                        GameEventsManager.instance.soundEvents.TriggerSound(SoundType.MARKET_DOOR_OPEN);
                        OnMarketLeave?.Invoke();
                    }
                    else
                    {
                        GameEventsManager.instance.soundEvents.TriggerSound(SoundType.MARKET_DOOR_CLOSED);
                        GameEventsManager.instance.questEvents.ShowKeyText();
                    }
                }

                if(hit.collider.tag == "AmmoStation")
                {
                    AmmoStation _ammoStation = hit.collider.GetComponent<AmmoStation>();
                    if (!_ammoStation.IsLocked)
                    {
                        GameEventsManager.instance.soundEvents.TriggerSound(SoundType.PICKUP);
                        _ammoStation.AmmoPicked();
                    }     
                }
            }
        }
    }

    private void WidgetRaycast()
    {
        Ray ray = mainCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0.5f));

            if(Physics.Raycast(ray, out RaycastHit hit, rayLength, layerMask))
            {
                if(showInteraction) return;

                showInteraction = true;

                if(hit.collider.tag == "ShelfQuest" && !QM.isDoingQuest && !QM.shelfQuestCompleted)
                {
                    GameEventsManager.instance.uiEvents.SendIteractionSprite(UI_Widget.TALK);
                }

                if (hit.collider.tag == "FloorQuest" && !QM.isDoingQuest && !QM.floorQuestCompleted)
                {
                    GameEventsManager.instance.uiEvents.SendIteractionSprite(UI_Widget.TALK);
                }

                if (hit.collider.tag == "PfandQuest" && !QM.isDoingQuest && !QM.pfandQuestCompleted)
                {
                    GameEventsManager.instance.uiEvents.SendIteractionSprite(UI_Widget.TALK);
                }

                if(hit.collider.tag == "FlowersQuest" && !QM.isDoingQuest && !QM.flowersQuestCompleted)
                {
                    GameEventsManager.instance.uiEvents.SendIteractionSprite(UI_Widget.TALK);
                }

                if(hit.collider.tag == "Cashtray" && QM.DayComplete)
                {
                    GameEventsManager.instance.uiEvents.SendIteractionSprite(UI_Widget.TAKE);           
                }

                if(hit.collider.tag == "Safe" && _carryingCashtray)
                {
                    GameEventsManager.instance.uiEvents.SendIteractionSprite(UI_Widget.PLACE);
                }

                if(hit.collider.tag == "HomeDoor")
                {
                    GameEventsManager.instance.uiEvents.SendIteractionSprite(UI_Widget.TAKE);
                }

                if(hit.collider.tag == "HomeMatress" && !_dayManager.IsDay)
                {
                    GameEventsManager.instance.uiEvents.SendIteractionSprite(UI_Widget.TAKE);
                }

                if(hit.collider.tag == "Computer")
                {
                    GameEventsManager.instance.uiEvents.SendIteractionSprite(UI_Widget.TALK);
                }

                if(hit.collider.tag == "NPC_Boss")
                {
                    GameEventsManager.instance.uiEvents.SendIteractionSprite(UI_Widget.TALK);
                }

                if (hit.collider.CompareTag("UI_Button"))
                {
                    GameEventsManager.instance.uiEvents.SendIteractionSprite(UI_Widget.TAKE);
                }   

        
                if(hit.collider.tag == "MarketKey")
                {
                    GameEventsManager.instance.uiEvents.SendIteractionSprite(UI_Widget.TAKE);
                }

                if(hit.collider.tag == "MarketDoor")
                {
                    GameEventsManager.instance.uiEvents.SendIteractionSprite(UI_Widget.TAKE);
                }

                if(hit.collider.tag == "AmmoStation")
                {
                    GameEventsManager.instance.uiEvents.SendIteractionSprite(UI_Widget.TAKE);    
                }
            }
        else
        {
            GameEventsManager.instance.uiEvents.HideInteractionWidget();
            showInteraction = false;
        }
        
        
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
