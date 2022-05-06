using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MultiplierIcon : MonoBehaviour
{
    [SerializeField] private Text _text;

    private void OnEnable()
    {
        UpdateMultiplier(GameStateManager._instance.ScoreMultiplier);
    }
    public void UpdateMultiplier(float curVal)
    {
        if(curVal > 1)
            _text.text = "x" + string.Format("{0:0.0}", curVal);
        else
            _text.text = "";
    }   
}
