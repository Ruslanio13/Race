using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIWindowManager : MonoBehaviour
{
    [SerializeField] private float _transitionSpeed;
    [SerializeField] private float _appearingSpeed;
    [SerializeField] private List<Window> _pauseWindows;
    [SerializeField] private Window _gameOverWindow;


    public float GetAppearingSpeed() => _appearingSpeed; 

    public static UIWindowManager _instance;

    void Awake()
    {
        if (_instance == null)
            _instance = this;
    }

    public UnityAction OnGameOver;

    private UnityAction OnPause;

    private void Start()
    {
        OnGameOver += () =>
        {
            _gameOverWindow.gameObject.SetActive(true);
        };

        GameStateManager._instance.OnGamePaused += () => {
            foreach (Window w in _pauseWindows)
                w.gameObject.SetActive(true);
        };

        GameStateManager._instance.OnGameUnpaused += () => {
            foreach (Window w in _pauseWindows)
                w.gameObject.SetActive(false);
        };
    }


    public void MoveCamera<T>(int dirCode, T initialWindow, T targetWindow) where T : Window
    {
        StartCoroutine(MoveWindows(dirCode, initialWindow, targetWindow));
    }

    private IEnumerator MoveWindows<T>(int dirCode, T initialWindow, T targetWindow) where T : Window
    {
        RectTransform init = initialWindow.gameObject.GetComponent<RectTransform>();
        RectTransform target = targetWindow.gameObject.GetComponent<RectTransform>();

        float delta = 0;
        target.anchoredPosition = new Vector2(dirCode * init.rect.width + init.anchoredPosition.x, target.anchoredPosition.y);

        float finalPos = target.anchoredPosition.x - dirCode * target.rect.width;

        while (delta < init.rect.width)
        {
            init.anchoredPosition += dirCode * _transitionSpeed * Vector2.left * Time.deltaTime;
            target.anchoredPosition += dirCode * _transitionSpeed * Vector2.left * Time.deltaTime;
            delta += _transitionSpeed * Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        target.anchoredPosition = new Vector2(finalPos, target.anchoredPosition.y);
    }
}