using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class TableScript : MonoBehaviour
{
    [SerializeField] private Text _distTxt;
    [SerializeField] private Text _speedTxt;
    [SerializeField] private Text _coinsTxt;
    [SerializeField] private int _koef;
    [SerializeField] private int _timeKoef;
    [SerializeField] private Player _player;

    private float _dist;
    private float _topSpeed;
    private bool _distanceLocked;

    void Start()
    {
        PickUpsManager._instance.OnCoinPickUp += UpdateBalance;
        UpdateBalance();
        _distanceLocked = false;
        _dist = 0;
        GameStateManager._instance.OnGameOver += LockHighScores;
    }

    private void LockHighScores()
    {
        _distanceLocked = true;
    }
    void FixedUpdate()
    {
        _speedTxt.text = string.Format("{0:.00}", _player.GetNormalizedPlayerSpeed());
        _distTxt.text = GameStateManager._instance.Distance.ToString();
    }

    public void UpdateBalance(int nominallo = 0)
    {
        _coinsTxt.text = (GameStateManager._instance.MoneyAmount + nominallo).ToString();
    }
    public string StrGetScore() => _dist.ToString();
    public string StrGetNormalizedSpeed() => _speedTxt.text;
    public string StrGetTopSpeed() => string.Format("{0:.00}",_topSpeed);
}