using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGameUIWindow : Window
{
    [SerializeField] List<Image> _images;
    [SerializeField] List<Text> _texts;
    [SerializeField] AccelerationTable _accelTable;
    [SerializeField] MultiplierIcon _multiplierIcon;
    [SerializeField] float _speedDisappearing;
    [SerializeField] Button _pauseButton;


    private void Start()
    {
        GameStateManager._instance.OnGameOver += HideInGameUI;
        GameStateManager._instance.OnGamePaused += HideInGameUI;
        GameStateManager._instance.OnGameUnpaused += ShowInGameUI;
        GameStateManager._instance.OnMultiplierChanged += UpdateMultiplier;
        PickUpsManager._instance.OnAccelerationPickUp += ShowNewAccelTable;

        _pauseButton.onClick.AddListener(() => { GameStateManager._instance.OnGamePaused?.Invoke(); });


    }

    private void UpdateMultiplier(float curVal)
    {
        _multiplierIcon.gameObject.SetActive(true);
        _multiplierIcon.UpdateMultiplier(curVal);
    }
    private void ShowNewAccelTable(float time)
    {
        _accelTable.gameObject.SetActive(true);
        _accelTable.SetMaxTime(time);
    }

    private void HideInGameUI()
    {
        _pauseButton.interactable = false;
        StartCoroutine(SmoothDisappearing());
        _accelTable.gameObject.SetActive(false);
        _multiplierIcon.gameObject.SetActive(false);
    }
    private void ShowInGameUI()
    {
        _pauseButton.interactable = true;
        foreach (Text txt in _texts)
        {
            txt.color += new Color(0, 0, 0, 1);
        }

        foreach (Image img in _images)
        {
            img.color += new Color(0, 0, 0, 1);
        }
        //if (PickUpsManager._instance.BonusTimeLeft > 0f)

        _accelTable.gameObject.SetActive(true);
        _multiplierIcon.gameObject.SetActive(true);
    }

    private IEnumerator SmoothDisappearing()
    {
        while (_texts[0].color.a > 0)
        {
            foreach (Text txt in _texts)
            {
                txt.color -= new Color(0, 0, 0, Time.deltaTime * _speedDisappearing);
            }

            foreach (Image img in _images)
            {
                img.color -= new Color(0, 0, 0, Time.deltaTime * _speedDisappearing);
            }

            yield return new WaitForEndOfFrame();
        }
    }
}

