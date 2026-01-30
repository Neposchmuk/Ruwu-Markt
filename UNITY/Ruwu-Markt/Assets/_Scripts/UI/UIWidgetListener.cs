using System;
using UnityEngine;
using UnityEngine.UI;

public class UIWidgetListener : MonoBehaviour
{
    [SerializeField] private Image interactionImage;

    [SerializeField] private Image actionWidget_1;

    [SerializeField] private Image actionWidget_2;

    [SerializeField] private Image actionWidget_3;

    private void Awake()
    {
        GameEventsManager.instance.uiEvents.onShowActionWidget += ShowActionWidget;
        GameEventsManager.instance.uiEvents.onShowInteractionWidget += ShowInteractionWidget;
        GameEventsManager.instance.uiEvents.onHideActionWidget += HideActionWidget;
        GameEventsManager.instance.uiEvents.onHideInteractionWidget += HideInteractionWidget;
        Debug.Log("Subscribed to events");
    }
    private void OnDisable()
    {
        GameEventsManager.instance.uiEvents.onShowActionWidget -= ShowActionWidget;
        GameEventsManager.instance.uiEvents.onShowInteractionWidget -= ShowInteractionWidget;
        GameEventsManager.instance.uiEvents.onHideActionWidget -= HideActionWidget;
        GameEventsManager.instance.uiEvents.onHideInteractionWidget -= HideInteractionWidget;
    }

    private void ShowActionWidget(int index, Sprite sprite)
    {
        Debug.Log("Received event");
        switch (index)
        {
            case 0:
                actionWidget_1.sprite = sprite;
                actionWidget_1.enabled = true;
                break;
            case 1:
                actionWidget_2.sprite = sprite;
                actionWidget_2.enabled = true;
                break;
            case 2:
                actionWidget_3.sprite = sprite;
                actionWidget_3.enabled = true;
                break;
        }
    }

    private void ShowInteractionWidget(Sprite sprite)
    {
        Debug.Log("Showing "+sprite);

        interactionImage.sprite = sprite;
        interactionImage.enabled = true;

        Debug.Log(interactionImage.enabled);
    }

    private void HideActionWidget()
    {
        if(actionWidget_1.enabled) actionWidget_1.enabled = false;

        if(actionWidget_2.enabled) actionWidget_2.enabled = false;

        if(actionWidget_3.enabled) actionWidget_3.enabled = false;
    }

    private void HideInteractionWidget()
    {
        interactionImage.enabled = false;
        Debug.Log(interactionImage.enabled);
    }

}
