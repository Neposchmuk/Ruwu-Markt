using UnityEngine;
using UnityEngine.InputSystem;

public class Pour_Out: MonoBehaviour
{
    private InputAction interact;

    private Quest_Manager QM;

    public GameObject instanceObject;

    public bool rayIsActive;

    private float timeToPour = 5;

    private int questVariant;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        interact = GameObject.Find("PlayerCapsule").GetComponent<PlayerInput>().actions.FindAction("Attack");

        QM = GameObject.Find("Quest_Manager").GetComponent<Quest_Manager>();

        Debug.DrawRay(transform.position, Vector3.down, Color.green);
    }

    // Update is called once per frame
    void Update()
    {
        if (rayIsActive && timeToPour > 0)
        {
            Pour();
        }
        else if (rayIsActive)
        {
            rayIsActive = false;
            QM.CompleteQuest(questVariant);
        }
    }

    //Instantiates Object on Ray hit position, if Instance Object is already present, increases size of Object
    private void Pour()
    {
        if(Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, 2) && interact.IsPressed())
        {
            if(hit.collider.tag == "PourPuddle")
            {
                hit.collider.gameObject.transform.localScale *= 1 + 0.25f * Time.deltaTime;
            }
            else
            {
                Instantiate(instanceObject, hit.point + new Vector3 (0,0.02f,0), Quaternion.Euler (new Vector3 (0, Random.Range(0, 359), 0)));
            }

            timeToPour -= 1 * Time.deltaTime;
        }
    }

    public void enableRay(int i)
    {
        rayIsActive = true;
        questVariant = i;
    }
}
