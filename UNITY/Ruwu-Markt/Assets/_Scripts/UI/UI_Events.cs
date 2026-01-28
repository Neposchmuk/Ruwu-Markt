using System;
using UnityEngine;
using UnityEngine.InputSystem.OnScreen;
using UnityEngine.UI;

public class UI_Events
{
    public event Action<Sprite> onShowInteractionWidget;

    public void ShowInteractionWidget(Sprite sprite)
    {
        if(onShowInteractionWidget != null)
        {
            onShowInteractionWidget(sprite);
        }
    }

    public event Action onHideInteractionWidget;
    
    public void HideInteractionWidget()
    {
        if(onHideInteractionWidget != null)
        {
            onHideInteractionWidget();
        }
    }

    public event Action<int, Sprite> onShowActionWidget;

    public void ShowActionWidget(int index, Sprite sprite)
    {
        if(onShowActionWidget != null)
        {
            onShowActionWidget(index, sprite);
        }
    }

    public event Action onHideActionWidget;

    public void HideActionWidget()
    {
        if(onHideActionWidget != null)
        {
            onHideActionWidget();
        }
    }

    public event Action<UI_Widget> onSendInteractionSprite;

    public void SendIteractionSprite(UI_Widget widget)
    {
        if(onSendInteractionSprite != null)
        {
            onSendInteractionSprite(widget);
        }
    }

    public event Action<UI_Widget, int> onSendActionSprite;

    public void SendActionSprite(UI_Widget widget, int index)
    {
        if(onSendActionSprite != null)
        {
            onSendActionSprite(widget, index);
        }
    }
}
