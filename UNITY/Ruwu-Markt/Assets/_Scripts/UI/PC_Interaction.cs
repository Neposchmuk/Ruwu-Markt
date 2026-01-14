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

    public GameObject[] Mail_UI_Groups;

    public Image Inbox;

    public Image Mail;

    public Button CloseUI;

    public Button CloseMail;

    [SerializeField] private GameObject PC_UI_Parent;

    private GameObject Mail_UI;


    Day_Manager _dayManager;

    private void OnEnable()
    {
        GameEventsManager.instance.gameEvents.onSendSanityUpdate += SetInboxGroups;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        CloseUI.onClick.AddListener(() => CloseInbox());

        CloseMail.onClick.AddListener(() => CloseMailWindow());

        _dayManager = FindFirstObjectByType<Day_Manager>();

        PC_UI_Parent.SetActive(false);

        Mail.gameObject.SetActive(false);

        Mail_UI = Mail_UI_Groups[0];

        if (_dayManager.IsFinalDay)
        {
            GameEventsManager.instance.gameEvents.RequestSanityUpdate();
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
        PC_UI_Parent.SetActive(false);
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

    public void OpenMailWindow(int mailType)
    {
        SetMailSprites(mailType);

        Mail.gameObject.SetActive(true);
    }

    void SetInboxGroups(int sanity, int jobSecurity)
    {
        if (!_dayManager.IsFinalDay)
        {
            Mail_UI = Mail_UI_Groups[0];
            return;
        }

        if(sanity >= 50)
        {
            Mail_UI = Mail_UI_Groups[1];
        }
        else
        {
            Mail_UI = Mail_UI_Groups[2];
        }
    }

    void SetMailSprites(int spriteIndex)
    {
        Mail.sprite = Mails[spriteIndex];
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
        PC_UI_Parent.SetActive(true);
        Mail_UI.SetActive(true);
        ToggleCursorLockmode(true);

        GameEventsManager.instance.gameEvents.ToggleSanityWidget(false);
    }
}
