using NUnit.Framework;
using StarterAssets;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections.Generic;
using Unity.VisualScripting;
using System;

public class Sanity_Manager : MonoBehaviour
{
    public TMP_Text sanityCounter;

    public TMP_Text jobSecCounter;

    public MatchSanitySpriteFill sanityBar;

    public MatchSanitySpriteFill jobSecBar;

    public SanitySpriteChanger SanitySprite;

    public GameObject GO_Insane;

    public GameObject GO_LostJob;

    public Button Restart;

    public int sanity;

    public int jobSecurity;

    public bool isGameOver;

    



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        sanity = Mathf.Clamp(50, 0, 100);

        jobSecurity = Mathf.Clamp(50, 0, 100);

        Restart.onClick.AddListener(() => RestartGame());

        GameEventsManager.instance.gameEvents.UpdateSanity(sanity);

        GO_Insane.SetActive(false);

        GO_LostJob.SetActive(false);

        Restart.gameObject.SetActive(false);

        isGameOver = false;

        sanityBar.AdjustSanityFill(sanity);

        jobSecBar.AdjustSanityFill(jobSecurity);
    }

    private void Update()
    {
        sanityCounter.text = $"{sanity}";

        jobSecCounter.text = $"{jobSecurity}";
    }

    public void ChangeSanity(int addSanity, int addJobSecurity)
    {
        sanity = Mathf.Clamp(sanity + addSanity, 0, 100);

        jobSecurity = Mathf.Clamp(jobSecurity + addJobSecurity, 0, 100);

        GameEventsManager.instance.gameEvents.UpdateSanity(sanity);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        CheckGameOver();

        sanityBar.AdjustSanityFill(sanity);

        jobSecBar.AdjustSanityFill(jobSecurity);

    }

    private void CheckGameOver()
    {
        if(sanity <= 0)
        {
            isGameOver = true;
            GameObject.FindFirstObjectByType<FirstPersonController>().enabled = false;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            GO_Insane.SetActive(true);
            Restart.gameObject.SetActive(true);
        }
        else if(jobSecurity <= 0)
        {
            isGameOver = true;
            GameObject.FindFirstObjectByType<FirstPersonController>().enabled = false;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            Restart.gameObject.SetActive(true);
            GO_LostJob.SetActive(true);
        }
    }

    public void RestartGame()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        
        List<Player_DDOL> DDOL_Objects = new List<Player_DDOL>();
        try
        {
            DDOL_Objects.AddRange(GameObject.FindObjectsByType<Player_DDOL>(FindObjectsSortMode.None));
        }
        catch(ArgumentException)
        {
            Debug.Log("Argument Exception bei ADDRange");
        }
        

        for(int i = 0; i < DDOL_Objects.Count; i++)
        {
            Destroy(DDOL_Objects[i].gameObject);
        }

        SceneManager.LoadScene("MAIN_MENU");
    }
}
