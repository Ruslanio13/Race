using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SettingsPauseWindow : Window
{

    [SerializeField] private Slider _musicVolumeSlider;
    [SerializeField] private Slider _SFXVolumeSlider;
    [SerializeField] private Button _backButton;
    
    
    private UnityAction BackButtonPressed;

    void Start()
    {
        _musicVolumeSlider.value = SaveManager._instance.SavedMusicVolume;
        _SFXVolumeSlider.value = SaveManager._instance.SavedSFXVolume;

        SaveManager._instance.MusicVolumeChange(_musicVolumeSlider.value);
        SaveManager._instance.SFXVolumeChange(_SFXVolumeSlider.value);

        _musicVolumeSlider.onValueChanged.AddListener(SaveManager._instance.MusicVolumeChange);
        _SFXVolumeSlider.onValueChanged.AddListener(SaveManager._instance.SFXVolumeChange);

        BackButtonPressed += onClicked;
    
        _backButton.onClick.AddListener(BackButtonPressed);
    }

    private void onClicked()
    {
        UIWindowManager._instance.MoveCamera(-1, this, _leftWindow);
    }
}
