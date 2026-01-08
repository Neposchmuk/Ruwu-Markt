using UnityEngine;

public class Player_DDOL : MonoBehaviour
{
    public bool dontDestroyOnLoad;

    private void Awake()
    {
        GameEventsManager.instance.gameEvents.onDestroyDDOLObjects += DestroyObject;
    }

    private void OnDestroy()
    {
        GameEventsManager.instance.gameEvents.onDestroyDDOLObjects -= DestroyObject;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (dontDestroyOnLoad)
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    private void DestroyObject()
    {
        Destroy(gameObject);
    }
}
