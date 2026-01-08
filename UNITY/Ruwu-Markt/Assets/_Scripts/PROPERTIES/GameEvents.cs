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


    public event Action onEnableSanityWidget;

    public void EnableSanityWidget()
    {
        if(onEnableSanityWidget != null)
        {
            onEnableSanityWidget();
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
}
