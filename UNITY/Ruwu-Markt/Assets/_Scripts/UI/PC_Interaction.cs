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

    private void OnEnable()
    {
        GameEventsManager.instance.gameEvents.onSendSanityUpdate += SetMailSprites;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        CloseUI.onClick.AddListener(() => CloseInbox());

        CloseMail.onClick.AddListener(() => CloseMailWindow());

        OpenMail.onClick.AddListener(() => OpenMailWindow());

        _dayManager = FindFirstObjectByType<Day_Manager>();

        Mail.gameObject.SetActive(false);

        OpenMail.gameObject.SetActive(false);

        if (_dayManager.IsFinalDay)
        {
            CloseUI.interactable = false;
        }

        Mail_UI.SetActive(false);

        if (_dayManager.IsDay)
        {
            GameEventsManager.instance.questEvents.UpdateQuestText("Check your mails");
        }
        else
        {
            GameEventsManager.instance.questEvents.UpdateQuestText("Go to sleep");
        }
    }

    void CloseInbox()
    {
        Mail_UI.SetActive(false);
        ToggleCursorLockmode(false);
        OnCloseUI?.Invoke();
        GameEventsManager.instance.gameEvents.ToggleSanityWidget(true);
        GameEventsManager.instance.questEvents.UpdateQuestText("Go to work");
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

    void SetMailSprites(int sanity, int jobSecurity)
    {
        if(sanity >= 50)
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
            GameEventsManager.instance.playerEvents.LockCamera(lockCursor);
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            GameEventsManager.instance.playerEvents.LockCamera(lockCursor);
        }
    }

    public void OpenInbox()
    {
        Mail_UI.SetActive(true);
        ToggleCursorLockmode(true);

        GameEventsManager.instance.gameEvents.ToggleSanityWidget(false);
    }
}
