using UnityEngine;

public class WidgetTutorial : MonoBehaviour
{
    [SerializeField] GameObject sanityTutorialGroup;

    private void OnEnable()
    {
        GameEventsManager.instance.playerEvents.onPressedInteract += CloseWidgetTutorial;
        GameEventsManager.instance.playerEvents.onPressedEscape += CloseWidgetTutorial;
        GameEventsManager.instance.uiEvents.onShowWidgetTutorial += ShowTutorial;
    }
    private void OnDisable()
    {
        GameEventsManager.instance.playerEvents.onPressedInteract -= CloseWidgetTutorial;
        GameEventsManager.instance.playerEvents.onPressedEscape -= CloseWidgetTutorial;
        GameEventsManager.instance.uiEvents.onShowWidgetTutorial -= ShowTutorial;
    }

    private void ShowTutorial(bool toggle)
    {
        if (!toggle)
        {
            CloseWidgetTutorial(InputEventContext.TUTORIAL);
            return;
        } 

        Debug.Log("Showing Tutorial");

        GameEventsManager.instance.playerEvents.ChangeInputEventContext(InputEventContext.TUTORIAL);
        GameEventsManager.instance.uiEvents.ToggleSanityWidget(false);
        GameEventsManager.instance.gameEvents.KeepPlayerLocked(true);
        GameEventsManager.instance.playerEvents.LockPlayerMovement(true);
        GameEventsManager.instance.playerEvents.LockCamera(true);
        sanityTutorialGroup.SetActive(true);
    }

    private void CloseWidgetTutorial(InputEventContext context)
    {
        if(context != InputEventContext.TUTORIAL) return;

        if(sanityTutorialGroup.activeSelf == true)
        {
            sanityTutorialGroup.SetActive(false);
            GameEventsManager.instance.uiEvents.ToggleSanityWidget(true);
            GameEventsManager.instance.gameEvents.KeepPlayerLocked(false);
            GameEventsManager.instance.playerEvents.LockPlayerMovement(false);
            GameEventsManager.instance.playerEvents.LockCamera(false);

            GameEventsManager.instance.playerEvents.ChangeInputEventContext(InputEventContext.DEFAULT);
        }
    }

}
