using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverScreenInfo : MonoBehaviour
{
    [SerializeField] private Text _finalScore;
    [SerializeField] private Text _finalSpeed;
    [SerializeField] private List<Image> _imageTransparency;
    [SerializeField] private List<Text> _textTransparency;
    [SerializeField] private float appearingSpeed;

    public void SetInfo(TableScript table)
    {
        _finalScore.text += table.StrGetScore();
        _finalSpeed.text += table.StrGetTopSpeed();
    }

    public void ShowMainInfo()
    {
        StartCoroutine(SmoothApearing());
    }

    IEnumerator SmoothApearing()
    {
        while (_imageTransparency[0].color.a < 1f)
        {
            foreach (Image i in _imageTransparency)
                i.color += new Color(0, 0, 0, appearingSpeed * Time.deltaTime);
            foreach (Text i in _textTransparency)
                i.color += new Color(0, 0, 0, appearingSpeed * Time.deltaTime);

            yield return new WaitForEndOfFrame();
        }
    }


}
