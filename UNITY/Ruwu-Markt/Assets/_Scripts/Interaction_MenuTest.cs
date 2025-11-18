using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;
using System.Collections.Generic;
using UnityEngine.Rendering;

public class Interaction_MenuTest : MonoBehaviour
{
    public GameObject interactionUI;

    public Button[] buttons;

    public int[] minRequirements;

    public int[] maxRequirements;

    private int[,] sanityRequirements;

    private actionsScript aS;

    private Sanity_Manager SM;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SM = GameObject.Find("Sanity_Manager").GetComponent<Sanity_Manager>();

        aS = GetComponent<actionsScript>();

        interactionUI.SetActive(false);

        sanityRequirements = new int[buttons.Length,buttons.Length];

        for(int i = 0; i < buttons.Length; i++)
        {
            sanityRequirements[i, 0] = minRequirements[i];
            sanityRequirements[i, 1] = maxRequirements[i];

            AddListeners(i);
        }

        //AddListeners();
    }

    /*public void Show_UI()
    {
        CheckSanity();
        interactionUI.SetActive(true);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }*/

    //Checks sanity value to lock or unlock Options
    private void CheckSanity()
    {
        for (int i = 0; i < buttons.Length; i++ )
        {
            if (SM.sanity >= sanityRequirements[i,0] && SM.sanity <= sanityRequirements[i,1])
            {
                buttons[i].interactable = true;
                buttons[i].GetComponent<Image>().color = Color.white;
            }
            else
            {
                buttons[i].interactable = false;
                buttons[i].GetComponent<Image>().color = Color.gray;
            }
        }
    }

    private void AddListeners(int i) //(int i)
    {
        buttons[i].onClick.AddListener(() => GetComponent<MiniGameStateManager>().StartQuest(i+1));

        /*buttons[0].onClick.AddListener(() => GetComponent<MiniGameStateManager>().StartQuest(1));
        buttons[1].onClick.AddListener(() => GetComponent<MiniGameStateManager>().StartQuest(2));
        buttons[2].onClick.AddListener(() => aS.TriggerAction(2));
        buttons[3].onClick.AddListener(() => aS.TriggerAction(3));*/
    }

    public void ToggleUI(bool Toggle)
    {
        if (!Toggle)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            interactionUI.SetActive(false);
        }
        else
        {
            CheckSanity();
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            interactionUI.SetActive(true);
        }
    }
    
}
