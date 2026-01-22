using UnityEngine;
using UnityEngine.InputSystem;

public class Pause_Menu : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;

    private bool showPauseMenu = false;

    private void Awake()
    {
        GameEventsManager.instance.gameEvents.onReturnToPreviousMenu += EnableMenu;
        GameEventsManager.instance.playerEvents.onPressedEscape +=SwitchPauseMenu;
        pauseMenu.SetActive(false);
    }
    private void OnDestroy()
    {
        GameEventsManager.instance.gameEvents.onReturnToPreviousMenu -= EnableMenu;
        GameEventsManager.instance.playerEvents.onPressedEscape -=SwitchPauseMenu;
    }

    private void SwitchPauseMenu(InputEventContext context)
    {
        if(context != InputEventContext.DEFAULT) return;

        showPauseMenu = !showPauseMenu;

        if(showPauseMenu == false)
        {
            GameEventsManager.instance.gameEvents.ShowSettings(false);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            TogglePause(false);
            pauseMenu.SetActive(false);
            GameEventsManager.instance.playerEvents.ChangeInputEventContext(InputEventContext.DEFAULT);
        }
        else
        {
            TogglePause(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            pauseMenu.SetActive(true);
            GameEventsManager.instance.playerEvents.ChangeInputEventContext(InputEventContext.MENU_UI);
        }
    }

    private void TogglePause(bool toggle)
    {
        GameEventsManager.instance.playerEvents.LockCamera(toggle);
        GameEventsManager.instance.playerEvents.LockPlayerMovement(toggle);
    }

    private void EnableMenu()
    {
        pauseMenu.SetActive(true);
    }

    public void SwitchSettingsMenu()
    {
        GameEventsManager.instance.gameEvents.ShowSettings(true);
        pauseMenu.SetActive(false);
    }

    public void ReturnToMenu()
    {
        TogglePause(false);
        GameEventsManager.instance.gameEvents.ChangeScene("Main_Menu");
        GameEventsManager.instance.gameEvents.DestroyDDOLObjects();
    }

    public void ShowPauseMenu()
    {
        SwitchPauseMenu(InputEventContext.DEFAULT);
    }
}
