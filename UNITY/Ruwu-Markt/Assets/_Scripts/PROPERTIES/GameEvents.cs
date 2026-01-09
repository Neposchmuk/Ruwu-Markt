using System;
using UnityEngine;

public class GameEvents
{
    public event Action<int> onUpdateSanity;

    public void UpdateSanity(int sanity)
    {
        if(onUpdateSanity != null)
        {
            onUpdateSanity(sanity);
        }
    }


    public event Action<string> onChangeScene;

    public void ChangeScene(string scene)
    {
        if (onChangeScene != null)
        {
            onChangeScene(scene);
        }
        else Debug.Log("No listeners for onChangeScene");
    }


    public event Action<bool> onToggleSanityWidget;

    public void ToggleSanityWidget(bool toggle)
    {
        if(onToggleSanityWidget != null)
        {
            onToggleSanityWidget(toggle);
        }
    }

    public event Action onDestroyDDOLObjects;

    public void DestroyDDOLObjects()
    {
        if(onDestroyDDOLObjects != null)
        {
            onDestroyDDOLObjects();
        }
    }

    public event Action onRequestSanityUpdate;

    public void RequestSanityUpdate()
    {
        if(onRequestSanityUpdate != null)
        {
            onRequestSanityUpdate();
        }
    }

    public event Action<int, int> onSendSanityUpdate;

    public void SendSanityUpdate(int sanity, int jobSecurity)
    {
        if(onSendSanityUpdate != null)
        {
            onSendSanityUpdate(sanity, jobSecurity);
        }
    }
}
