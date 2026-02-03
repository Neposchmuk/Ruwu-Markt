using UnityEngine;
using UnityEngine.InputSystem;

public class Pause_Menu : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;

    private bool showPauseMenu = false;

    private InputEventContext previousContext;

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
        if((context == InputEventContext.UI || context == InputEventContext.DIALOGUE) && context != InputEventContext.MENU_UI) return;

        GameEventsManager.instance.soundEvents.TriggerSound(SoundType.UI_OPEN);

        showPauseMenu = !showPauseMenu;

        if(showPauseMenu == false)
        {
            GameEventsManager.instance.gameEvents.ShowSettings(false);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            TogglePause(false);
            pauseMenu.SetActive(false);
            GameEventsManager.instance.playerEvents.ChangeInputEventContext(previousContext);
            GameEventsManager.instance.soundEvents.TriggerSound(SoundType.UI_CLICK);
        }
        else
        {
            if(context == InputEventContext.TUTORIAL) return;

            previousContext = context;
            TogglePause(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            pauseMenu.SetActive(true);
            GameEventsManager.instance.playerEvents.ChangeInputEventContext(InputEventContext.MENU_UI);
            GameEventsManager.instance.soundEvents.TriggerSound(SoundType.UI_CLICK);
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
        GameEventsManager.instance.soundEvents.TriggerSound(SoundType.UI_CLICK);
        GameEventsManager.instance.gameEvents.ShowSettings(true);
        pauseMenu.SetActive(false);
    }

    public void ReturnToMenu()
    {
        GameEventsManager.instance.soundEvents.TriggerSound(SoundType.UI_CLICK);
        TogglePause(false);
        GameEventsManager.instance.gameEvents.ChangeScene("Main_Menu");
        GameEventsManager.instance.gameEvents.DestroyDDOLObjects();
    }

    public void ShowPauseMenu()
    {
        SwitchPauseMenu(InputEventContext.DEFAULT);
    }
}
