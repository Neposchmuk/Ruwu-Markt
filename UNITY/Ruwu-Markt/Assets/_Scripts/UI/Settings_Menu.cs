using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using TMPro;
using UnityEngine.UI;

//https://youtu.be/YOaYQrN1oYQ
public class Settings_Menu : MonoBehaviour
{
    [SerializeField] private AudioMixer Volume_Master;

    [SerializeField] private TMP_Dropdown resolutionDropdown;

    [SerializeField] private Button returnButton;

    [SerializeField] private GameObject settingsMenu;

    private Resolution[] resolutions;


    private void Awake()
    {
        GameEventsManager.instance.gameEvents.onShowSettings += SwitchSettingsMenu;
    }
    private void OnDestroy()
    {
        GameEventsManager.instance.gameEvents.onShowSettings -= SwitchSettingsMenu;
    } 

    void Start()
    {
        UpdateResolutions();

        returnButton.onClick.AddListener(() => SwitchSettingsMenu(false));

        SetQuality(0);
    }

    public void SetVolume(float volume)
    {
        Volume_Master.SetFloat("MasterVolume", volume);
    }

    public void SetFullscreen(bool toggle)
    {
        GameEventsManager.instance.soundEvents.TriggerSound(SoundType.UI_CLICK);
        Screen.fullScreen = toggle;
    }

    public void SetQuality(int qualityIndex)
    {
        GameEventsManager.instance.soundEvents.TriggerSound(SoundType.UI_CLICK);
        QualitySettings.SetQualityLevel(qualityIndex);
        Debug.Log(QualitySettings.GetQualityLevel());
    }

    private void UpdateResolutions()
    {
        resolutions = Screen.resolutions;

        resolutionDropdown.ClearOptions();

        List<string> resolutionOptions = new List<string>();

        int currentResolutionIndex = 0;

        for(int i = 0; i < resolutions.Length; i++)
        {
           string option = resolutions[i].width + " x " + resolutions[i].height;
           resolutionOptions.Add(option);

           if(resolutions[i].width == Screen.currentResolution.width &&
              resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }

        resolutionDropdown.AddOptions(resolutionOptions);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }

    public void SetResolution(int resolutionIndex)
    {
        GameEventsManager.instance.soundEvents.TriggerSound(SoundType.UI_CLICK);
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    private void SwitchSettingsMenu(bool toggle)
    {
        GameEventsManager.instance.soundEvents.TriggerSound(SoundType.UI_CLICK);
        Debug.Log("Called SwitchSettings: " + toggle);
        if(toggle == false)
        {
            GameEventsManager.instance.gameEvents.ReturnToPreviousMenu();
            settingsMenu.SetActive(false);
        }
        else
        {
            settingsMenu.SetActive(true);
        }
        
    }
}
