using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CheatMenu : MonoBehaviour
{
    [SerializeField] GameObject Menu;

    [SerializeField] Toggle pauseGame;

    [SerializeField] Toggle skipDay;

    [SerializeField] TMP_InputField dayField;

    [SerializeField] TMP_InputField nightField;

    [SerializeField] Button submitDayNight;

    [SerializeField] Toggle playerInvincible;

    [SerializeField] Toggle isDay;


    bool menuActive = false;

    void Awake()
    {
        GameEventsManager.instance.cmEvents.onToggleCheatMenu += ToggleMenu;
    }

    void OnDestroy()
    {
        GameEventsManager.instance.cmEvents.onToggleCheatMenu -= ToggleMenu;
    }


    void Start()
    {
        submitDayNight.onClick.AddListener(() => SubmitDayNight());
    }

    void ToggleMenu()
    {
        menuActive = !menuActive;

        Menu.SetActive(menuActive);

        if (menuActive)
        {
            GameEventsManager.instance.playerEvents.LockCamera(true);
            GameEventsManager.instance.playerEvents.LockPlayerMovement(true);

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
           GameEventsManager.instance.playerEvents.LockCamera(false);
            GameEventsManager.instance.playerEvents.LockPlayerMovement(false);

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false; 
        }
    }

    void SubmitDayNight()
    {
        int day = Mathf.Clamp(int.Parse(dayField.text), 1, 5);

        int night = Mathf.Clamp(int.Parse(nightField.text), 1, 4);

        GameEventsManager.instance.cmEvents.ChangeDayNight(day, night);
    }

    public void PauseGame(bool toggle)
    {
        GameEventsManager.instance.cmEvents.ToggleGamePause(toggle);
    }

    public void SkipDay()
    {
        GameEventsManager.instance.cmEvents.SkipDay();
    }

    public void TogglePlayerInvincible(bool toggle)
    {
        GameEventsManager.instance.cmEvents.TogglePlayerInvincible(toggle);
    }

    public void ToggleIsDay(bool toggle)
    {
        GameEventsManager.instance.cmEvents.ToggleIsDay(toggle);
    }

    public void TogglePauseGame(bool toggle)
    {
        if (toggle)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }
}
