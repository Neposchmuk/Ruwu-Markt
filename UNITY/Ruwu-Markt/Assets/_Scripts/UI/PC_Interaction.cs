using System;
using System.Net.Mail;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class PC_Interaction : MonoBehaviour
{
    public static Action OnCloseUI;


    public Sprite[] Inboxes;

    public Sprite[] Mails;

    public GameObject Mail_UI;

    public Image Inbox;

    public Image Mail;

    public Button CloseUI;

    public Button CloseMail;

    public Button OpenMail;


    Day_Manager _dayManager;

    Sanity_Manager _sanityManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        CloseUI.onClick.AddListener(() => CloseInbox());

        CloseMail.onClick.AddListener(() => CloseMailWindow());

        OpenMail.onClick.AddListener(() => OpenMailWindow());

        _dayManager = FindFirstObjectByType<Day_Manager>();

        _sanityManager = FindFirstObjectByType<Sanity_Manager>();

        Mail.gameObject.SetActive(false);

        OpenMail.gameObject.SetActive(false);

        if (_dayManager.IsFinalDay)
        {
            SetMailSprites();
            CloseUI.interactable = false;
        }

        Mail_UI.SetActive(false);
    }

    void CloseInbox()
    {
        Mail_UI.SetActive(false);
        ToggleCursorLockmode(false);
        OnCloseUI?.Invoke();
    }

    void CloseMailWindow()
    {
        Mail.gameObject.SetActive(false);
        CloseUI.interactable = true;
    }

    void OpenMailWindow()
    {
        Mail.gameObject.SetActive(true);
    }

    void SetMailSprites()
    {
        if(_sanityManager.sanity >= 50)
        {
            Inbox.sprite = Inboxes[1];
            Mail.sprite = Mails[0];
        }
        else
        {
            Inbox.sprite = Inboxes[2];
            Mail.sprite = Mails[1];
        }

        OpenMail.gameObject.SetActive(true);
    }

    void ToggleCursorLockmode(bool lockCursor)
    {
        if (!lockCursor)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            GameEventsManager.instance.playerEvents.CameraLock(lockCursor);
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            GameEventsManager.instance.playerEvents.CameraLock(lockCursor);
        }
    }

    public void OpenInbox()
    {
        Mail_UI.SetActive(true);
        ToggleCursorLockmode(true);
    }
}
