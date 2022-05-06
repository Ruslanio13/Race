using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;


public class MainPauseWindow : Window
{
    [SerializeField] private Button _settingsButton;
    [SerializeField] private Button _returnButton;

    void Start()
    {
        _settingsButton.onClick.AddListener(() => { UIWindowManager._instance.MoveCamera(1, this, _rightWindow); });
        _returnButton.onClick.AddListener(() => { GameStateManager._instance.OnGameUnpaused.Invoke(); });
    }
}
