using UnityEngine;

public class Player_DDOL : MonoBehaviour
{
    public bool dontDestroyOnLoad;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (dontDestroyOnLoad)
        {
            DontDestroyOnLoad(gameObject);
        }
    }

}
