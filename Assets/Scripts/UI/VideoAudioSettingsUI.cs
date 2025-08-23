using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class VideoAudioSettingsUI : MonoBehaviour
{
    [SerializeField] private Slider volumeSlider;
    [SerializeField] private TMP_Dropdown resolutionDropdown;
    [SerializeField] private TMP_Dropdown qualityDropdown;
    [SerializeField] private Toggle fullScreenToggle;
    [SerializeField] private Toggle vSyncToggle;
    [SerializeField] private Button applyButton;

    private void Start()
    {
        applyButton.onClick.AddListener(() =>
        {
            ApplyChanges();
            SoundManager.Instance.PlaySound(SoundManager.Sound.UIPress);
        });

        SetDefaultValues();
    }

    private void ApplyChanges()
    {
        AudioManager.Instance.SetVolume(volumeSlider.value);

        Resolution resolution = Screen.resolutions[resolutionDropdown.value];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);

        QualitySettings.SetQualityLevel(qualityDropdown.value);

        Screen.fullScreen = fullScreenToggle.isOn;

        QualitySettings.vSyncCount = vSyncToggle.isOn ? 1 : 0;
    }

    private void SetDefaultValues()
    {
        volumeSlider.value = AudioManager.Instance.GetVolume();

        resolutionDropdown.ClearOptions();

        List<string> resolutionList = new List<string>();

        for (int i = 0; i < Screen.resolutions.Length; i++)
        {
            resolutionList.Add(Screen.resolutions[i].ToString());
        }

        resolutionDropdown.AddOptions(resolutionList);

        resolutionDropdown.value = Array.IndexOf(Screen.resolutions, Screen.currentResolution);

        qualityDropdown.value = QualitySettings.GetQualityLevel();

        fullScreenToggle.isOn = Screen.fullScreen;

        vSyncToggle.isOn = QualitySettings.vSyncCount == 1;
    }
}
