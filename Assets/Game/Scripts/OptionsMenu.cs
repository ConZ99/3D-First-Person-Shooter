using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;
    private Resolution[] resolutions;
    public Dropdown resolutionDropdown;
    public Slider volumeSlider;
    public Toggle fullscreenToggle;
    public Dropdown qualityDropdown;

    private int currentResolution = -1;

    private void Start()
    {
        //Setare rezolutii posibile
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();
        int currentIndex = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width && 
                resolutions[i].height == Screen.currentResolution.height)
                currentIndex = i;
        }
        resolutionDropdown.AddOptions(options);

        //Setare rezolutie curenta
        if (currentResolution != -1)
        {
            SetResolution(currentResolution);
            resolutionDropdown.value = currentResolution;
        }
        else
        {
            SetResolution(currentIndex);
            resolutionDropdown.value = currentIndex;
        }
        resolutionDropdown.RefreshShownValue();

        //Setare grafica
        qualityDropdown.value = QualitySettings.GetQualityLevel();
        qualityDropdown.RefreshShownValue();

        //Setare volum
        audioMixer.GetFloat("volume", out float soundVal);
        volumeSlider.value = soundVal;

        //Setare fullscreen
        bool isFullscreen = Screen.fullScreen;
        fullscreenToggle.isOn = isFullscreen;
    }

    public void SetVolume(float amount)
    {
        audioMixer.SetFloat("volume", amount);
    }

    public void SetFulscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
        currentResolution = resolutionIndex;
    }

    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
        Debug.Log("setat:" + QualitySettings.GetQualityLevel());
    }
}
