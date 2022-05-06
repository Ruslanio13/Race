using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverScreenBackground : MonoBehaviour
{
    [SerializeField] private GameObject _gameOverScreenPrefab;
    [SerializeField] private float appearingSpeed;
    [SerializeField] private Player _player;
    [SerializeField] private TableScript _table;
    [SerializeField] private Image[] _otherImages;
    [SerializeField] private Text[] _otherTexts;
    private GameOverScreenInfo _info;
    private Image _background;
    private GameObject _gameOverScreen;

    public void DisplayBGScreen()
    {
        _gameOverScreen = Instantiate(_gameOverScreenPrefab, gameObject.transform);
        _background = _gameOverScreen.GetComponentInChildren<Image>();
        _info = _gameOverScreen.GetComponentInChildren<GameOverScreenInfo>();
        StartCoroutine(SmoothGameOverScreenAppearing());
        StartCoroutine(SmoothDisappearingOfOtherElements());
    }

    IEnumerator SmoothGameOverScreenAppearing()
    {
        while (_background.color.a < 0.8f)
        {
            _background.color += new Color(0, 0, 0, appearingSpeed * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }
        _info.SetInfo(_table);
        _info.ShowMainInfo();
        Debug.Log("Appearing Finished");
    }
    IEnumerator SmoothDisappearingOfOtherElements()
    {
        while (_otherImages[0].color.a > 0f)
        {
            foreach (Image im in _otherImages)
            {
                im.color += new Color(0, 0, 0, -appearingSpeed * Time.deltaTime);
            }
            foreach (Text tx in _otherTexts)
            {
                tx.color += new Color(0, 0, 0, -appearingSpeed * Time.deltaTime);
            }
            yield return new WaitForEndOfFrame();
        }
    }

}
