using Cinemachine;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum ButtonType
{
    START,
    SETTINGS,
    QUIT
}

public class Main_Menu_Manager : MonoBehaviour
{
    [SerializeField] Button PlayButton;

    [SerializeField] Button SettingsButton;

    [SerializeField] Button QuitButton;

    [SerializeField] GameObject MainMenuGroup;

    [SerializeField] GameObject SettingsMenuGroup;

    private void OnEnable()
    {
        GameEventsManager.instance.gameEvents.onReturnToPreviousMenu += EnableMenu;
    }
    private void OnDisable()
    {
        GameEventsManager.instance.gameEvents.onReturnToPreviousMenu -= EnableMenu;
    }

    private void Start()
    {
        PlayButton.onClick.AddListener(() => LoadScene("Initiate_DDOL_Objects"));

        SettingsButton.onClick.AddListener(() => SwitchMenu(true));

        QuitButton.onClick.AddListener(() => QuitGame());

        MainMenuGroup.SetActive(true);

        SettingsMenuGroup.SetActive(false);
    }

    private void LoadScene(string name)
    {
        GameEventsManager.instance.gameEvents.ChangeScene(name);
    }

    private void SwitchMenu(bool toggle)
    {
        MainMenuGroup.SetActive(!toggle);
        //SettingsMenuGroup.SetActive(toggle);
        GameEventsManager.instance.gameEvents.ShowSettings(true);
    }   

    private void QuitGame()
    {
        GameEventsManager.instance.gameEvents.QuitGame();
    }

    private void EnableMenu()
    {
        MainMenuGroup.SetActive(true);
    }
}
