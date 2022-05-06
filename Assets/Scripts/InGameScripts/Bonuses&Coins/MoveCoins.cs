using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoveCoins : MonoBehaviour
{

    [SerializeField] private int _nominallo;
    private float _speedCoinBeforePause;
    private float _speedCoin;


    private void Start()
    {
        GameStateManager._instance.OnGamePaused += OnPause;
        GameStateManager._instance.OnGameUnpaused += OnUnpause;
    }
    void Update()
    {
        if(GameStateManager._instance.IsGamePaused)
            return;
        gameObject.transform.position += new Vector3(0, -_speedCoin * Time.deltaTime, 0);
    }

    public void SetInformation(float playerSpeed)
    {
        _speedCoin = playerSpeed;
    }

    private void OnPause()
    {
        _speedCoinBeforePause = _speedCoin;
        _speedCoin = 0;

    }

    private void OnUnpause() => _speedCoin = _speedCoinBeforePause;

    private void OnDestroy()
    {
        GameStateManager._instance.OnGamePaused -= OnPause;
        GameStateManager._instance.OnGameUnpaused -= OnUnpause;
    }
    public int GetNominallo() => _nominallo;
}
