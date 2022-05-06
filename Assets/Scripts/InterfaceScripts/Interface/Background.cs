using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Background : MonoBehaviour
{
    [SerializeField] private Image _backGround;

    private void Start()
    {
        GameStateManager._instance.OnGameOver += () => 
        { 
            StopAllCoroutines();
            StartCoroutine(SmoothAppearing()); 
        };
        GameStateManager._instance.OnGamePaused += () => 
        { 
            StopAllCoroutines();
            StartCoroutine(SmoothAppearing()); 
        };
        GameStateManager._instance.OnGameUnpaused += () => 
        {
            StopAllCoroutines(); 
            StartCoroutine(SmoothDisappearing()); 
        };
    }


    private IEnumerator SmoothAppearing()
    {
        while (_backGround.color.a < 0.8f)
        {
            _backGround.color += new Color(0, 0, 0, Time.deltaTime * UIWindowManager._instance.GetAppearingSpeed());
            yield return new WaitForEndOfFrame();
        }
    }
    private IEnumerator SmoothDisappearing()
    {
        while (_backGround.color.a > 0f)
        {
            _backGround.color -= new Color(0, 0, 0, Time.deltaTime * UIWindowManager._instance.GetAppearingSpeed());
            yield return new WaitForEndOfFrame();
        }
    }
}
