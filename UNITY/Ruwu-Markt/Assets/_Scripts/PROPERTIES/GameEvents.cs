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

    public event Action onQuitGame;

    public void QuitGame()
    {
        if(onQuitGame != null)
        {
            onQuitGame();
        }
    }

    public event Action onReturnToPreviousMenu;

    public void ReturnToPreviousMenu()
    {
        if(onReturnToPreviousMenu != null)
        {
            onReturnToPreviousMenu();
        }
    }

    public event Action<bool> onShowSettings;

    public void ShowSettings(bool toggle)
    {
        Debug.Log(onShowSettings);
        if(onShowSettings != null)
        {
            onShowSettings(toggle);
        }
    }

    public event Action<GameObject> onRequestPlayerObject;

    public void RequestPlayerObject(GameObject requester)
    {
        if(onRequestPlayerObject != null)
        {
            onRequestPlayerObject(requester);
        }
    }

    public event Action<GameObject, GameObject> onSendPlayerObject;

    public void SendPlayerObject(GameObject requester, GameObject playerObject)
    {
        if(onSendPlayerObject != null)
        {
            onSendPlayerObject(requester, playerObject);
        }
    }

    public event Action onSkipDay;

    public void SkipDay()
    {
        if(onSkipDay != null)
        {
            onSkipDay();
        }
    }

    public event Action onCheckGameOver;

    public void CheckGameOver()
    {
        if(onCheckGameOver != null)
        {
            onCheckGameOver();
        }
    }

    public event Action onIsGameOver;

    public void IsGameOver()
    {
        if(onIsGameOver != null)
        {
            onIsGameOver();
        }
    }

    public event Action<bool> onKeepPlayerLocked;

    public void KeepPlayerLocked(bool toggle)
    {
        if(onKeepPlayerLocked != null)
        {
            onKeepPlayerLocked(toggle);
        }
    }
}
