using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;

    public Dropdown resolutionDropdown;
    public Dropdown graphicsDropdown;
    public Slider backgroundVol;
    public Slider sfxVol;
    public Toggle fullScreen;

    Resolution[] resolutions;

    void Start()
    {
        /*Get an array of available resolutions to display in the dropdown*/
        resolutions = Screen.resolutions;

        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();

        int currentResolutionIndex = 0;

        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.width && resolutions[i].height == Screen.height)
            {
                currentResolutionIndex = i;
            }
        }
        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();

        RetrieveSavedSettings();
    }

    public void RetrieveSavedSettings()
    {
        //get our last set resolution
        Resolution res = resolutions[PlayerPrefs.GetInt("SavedResolutionIndex")];
        Screen.SetResolution(res.width, res.height, Screen.fullScreen);
        resolutionDropdown.value = PlayerPrefs.GetInt("SavedResolutionIndex");
        //get our last set quality settings
        QualitySettings.SetQualityLevel(PlayerPrefs.GetInt("SavedQualityIndex"));
        graphicsDropdown.value = PlayerPrefs.GetInt("SavedQualityIndex");
        //get our last set volume settings
        audioMixer.SetFloat("BackgroundVol", PlayerPrefs.GetFloat("SavedBackgroundVol"));
        backgroundVol.value = PlayerPrefs.GetFloat("SavedBackgroundVol");
        audioMixer.SetFloat("SFXVol", PlayerPrefs.GetFloat("SavedSFXVol"));
        sfxVol.value = PlayerPrefs.GetFloat("SavedSFXVol");

        //get our last fullscreen settings
        if (PlayerPrefs.GetInt("SavedFullScreen") == 1)
        {
            Screen.fullScreen = true;
            fullScreen.isOn = true;
        }
        else
        {
            Screen.fullScreen = false;
            fullScreen.isOn = false;
        }


    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution res = resolutions[resolutionIndex];
        Screen.SetResolution(res.width, res.height, Screen.fullScreen);
        PlayerPrefs.SetInt("SavedResolutionIndex", resolutionIndex);
    }
    public void SetBackgroundVolume(float volume)
    {
        audioMixer.SetFloat("BackgroundVol", volume);
        PlayerPrefs.SetFloat("SavedBackgroundVol", volume);

    }
    public void SetSFXVolume(float volume)
    {
        audioMixer.SetFloat("SFXVol", volume);
        PlayerPrefs.SetFloat("SavedSFXVol", volume);

    }
    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
        PlayerPrefs.SetInt("SavedQualityIndex", qualityIndex);
    }
    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
        if (isFullscreen == true) {
            PlayerPrefs.SetInt("SavedFullScreen", 1);
        }
        else{
            PlayerPrefs.SetInt("SavedFullScreen", 0);
        }
    }
    
}
