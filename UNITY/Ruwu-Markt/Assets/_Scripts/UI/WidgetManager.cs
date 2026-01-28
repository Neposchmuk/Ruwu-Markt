using UnityEngine;

public class WidgetManager : MonoBehaviour
{
    [SerializeField] WidgetLibrary library;

    private void Awake()
    {
        GameEventsManager.instance.uiEvents.onSendActionSprite += SendActionSprite;
        GameEventsManager.instance.uiEvents.onSendInteractionSprite += SendInteractionSprite;
    }
    private void OnDestroy()
    {
        GameEventsManager.instance.uiEvents.onSendActionSprite -= SendActionSprite;
        GameEventsManager.instance.uiEvents.onSendInteractionSprite -= SendInteractionSprite;
    }

    private void SendActionSprite(UI_Widget widget, int index)
    {
        switch(widget)
        {
            case UI_Widget.TAKE:
            GameEventsManager.instance.uiEvents.ShowActionWidget(index, library.Take);
                break;
            case UI_Widget.PLACE:
            GameEventsManager.instance.uiEvents.ShowActionWidget(index, library.Place);
                break;
            case UI_Widget.TALK:
            GameEventsManager.instance.uiEvents.ShowActionWidget(index, library.Talk);
                break;
            case UI_Widget.KEY:
            GameEventsManager.instance.uiEvents.ShowActionWidget(index, library.Key);
                break;
            case UI_Widget.THROW:
            GameEventsManager.instance.uiEvents.ShowActionWidget(index, library.Throw);
                break;
            case UI_Widget.WATER:
            GameEventsManager.instance.uiEvents.ShowActionWidget(index, library.Water);
                break;
            case UI_Widget.SIP:
            GameEventsManager.instance.uiEvents.ShowActionWidget(index, library.Sip);
                break;
            case UI_Widget.POUR:
            GameEventsManager.instance.uiEvents.ShowActionWidget(index, library.Pour);
                break;
            case UI_Widget.CLEAN:
            GameEventsManager.instance.uiEvents.ShowActionWidget(index, library.Clean);
                break;
            case UI_Widget.FLASH:
            GameEventsManager.instance.uiEvents.ShowActionWidget(index, library.Flash);
                break;
            case UI_Widget.GUN_R:
            GameEventsManager.instance.uiEvents.ShowActionWidget(index, library.Gun_R);
                break;
            case UI_Widget.GUN_LMB:
            GameEventsManager.instance.uiEvents.ShowActionWidget(index, library.Gun_LMB);
                break;
            case UI_Widget.BAT_F:
            GameEventsManager.instance.uiEvents.ShowActionWidget(index, library.Bat_F);
                break;
            case UI_Widget.BAT_LMB:
            GameEventsManager.instance.uiEvents.ShowActionWidget(index, library.Bat_LMB);
                break;
        }
    }

    private void SendInteractionSprite(UI_Widget widget)
    {
        switch(widget)
        {
            case UI_Widget.TAKE:
            GameEventsManager.instance.uiEvents.ShowInteractionWidget(library.Take);
                break;
            case UI_Widget.PLACE:
            GameEventsManager.instance.uiEvents.ShowInteractionWidget(library.Place);
                break;
            case UI_Widget.TALK:
            GameEventsManager.instance.uiEvents.ShowInteractionWidget(library.Talk);
                break;
            case UI_Widget.KEY:
            GameEventsManager.instance.uiEvents.ShowInteractionWidget(library.Key);
                break;
            case UI_Widget.THROW:
            GameEventsManager.instance.uiEvents.ShowInteractionWidget(library.Throw);
                break;
            case UI_Widget.WATER:
            GameEventsManager.instance.uiEvents.ShowInteractionWidget(library.Water);
                break;
            case UI_Widget.SIP:
            GameEventsManager.instance.uiEvents.ShowInteractionWidget(library.Sip);
                break;
            case UI_Widget.POUR:
            GameEventsManager.instance.uiEvents.ShowInteractionWidget(library.Pour);
                break;
            case UI_Widget.CLEAN:
            GameEventsManager.instance.uiEvents.ShowInteractionWidget(library.Clean);
                break;
            case UI_Widget.FLASH:
            GameEventsManager.instance.uiEvents.ShowInteractionWidget(library.Flash);
                break;
            case UI_Widget.GUN_R:
            GameEventsManager.instance.uiEvents.ShowInteractionWidget(library.Gun_R);
                break;
            case UI_Widget.GUN_LMB:
            GameEventsManager.instance.uiEvents.ShowInteractionWidget(library.Gun_LMB);
                break;
            case UI_Widget.BAT_F:
            GameEventsManager.instance.uiEvents.ShowInteractionWidget(library.Bat_F);
                break;
            case UI_Widget.BAT_LMB:
            GameEventsManager.instance.uiEvents.ShowInteractionWidget(library.Bat_LMB);
                break;
        }
    }
}
