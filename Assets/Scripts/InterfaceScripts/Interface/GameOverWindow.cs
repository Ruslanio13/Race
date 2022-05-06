using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverWindow : Window
{
    [Header ("Background goes last")]
    [Header("Dist goes first")]
    [Header("Speed goes second")]
    [SerializeField] private List<Text> _texts;
    [SerializeField] private Button _restartButton;
    [SerializeField] private Button _menuButton;

    private void OnEnable()
    {
        _restartButton.onClick.AddListener(() => { GameStateManager._instance.RestartGame(); });
        _menuButton.onClick.AddListener(() => { SceneManager.LoadScene(2); });
        StartCoroutine(SmoothAppearing());
        _texts[0].text += GameStateManager._instance.Distance.ToString();
        _texts[1].text += String.Format("{0:.00}",GameStateManager._instance.TopSpeedForRide);
    }

    private IEnumerator SmoothAppearing()
    {
        while (_texts[0].color.a < 1f)
        {
            foreach (Text txt in _texts)
            {
                txt.color += new Color(0, 0, 0, Time.deltaTime * UIWindowManager._instance.GetAppearingSpeed());
            }
            _restartButton.image.color += new Color(0, 0, 0, Time.deltaTime * UIWindowManager._instance.GetAppearingSpeed());
            _menuButton.image.color += new Color(0, 0, 0, Time.deltaTime * UIWindowManager._instance.GetAppearingSpeed());
            yield return new WaitForEndOfFrame();
        }
    }
}
