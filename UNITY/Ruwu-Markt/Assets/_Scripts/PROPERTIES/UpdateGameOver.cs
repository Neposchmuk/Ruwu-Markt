using UnityEngine;

public class UpdateGameOver : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameEventsManager.instance.gameEvents.CheckGameOver();
    }
}
