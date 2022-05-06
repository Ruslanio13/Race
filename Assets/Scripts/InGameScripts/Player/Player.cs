using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [SerializeField] private float _speedKoef;
    [SerializeField] private float _playerSpeed;
    [SerializeField] private float _playerStrafeSpeed;
    private float _playerSpeedBeforePause;
    [SerializeField] private GameObject _model;
    private IEnumerator _accelCoroutine;

    private void Start()
    {
        GameStateManager._instance.OnGamePaused += OnPause;
        GameStateManager._instance.OnGameUnpaused += OnUnpause;
        GameStateManager._instance.OnGameOver += OnGameOver;

        Instantiate(GameStateManager._instance.SelectedCar.GetModel(), _model.transform);
        
        _playerSpeed = GameStateManager._instance.SelectedCar.GetCarSpeed();
        _playerStrafeSpeed = GameStateManager._instance.SelectedCar.GetCarStrafeSpeed();

        Debug.Log("Speed is " + _playerSpeed);
        Debug.Log("StrafeSpeed is " + _playerStrafeSpeed);


        _accelCoroutine = PlayerNaturalAcceleration();
        StartCoroutine(_accelCoroutine);
    }

    IEnumerator PlayerNaturalAcceleration()
    {
        while (true)
        {
            _playerSpeed += Time.fixedDeltaTime * _speedKoef;
            yield return new WaitForFixedUpdate();
        }
    }

    private void OnPause()
    {
        gameObject.GetComponent<PlayerStrafe>().enabled = false;
        StopCoroutine(_accelCoroutine);
    }

    private void OnUnpause()
    {
        gameObject.GetComponent<PlayerStrafe>().enabled = true;
        StartCoroutine(_accelCoroutine);
    }

    private void OnGameOver()
    {
        _playerStrafeSpeed = 0;
        StartCoroutine(SmoothSlowDown());
        StopCoroutine(_accelCoroutine);
    }

    private IEnumerator SmoothSlowDown()
    {
        while (_playerSpeed > 0f)
        {
            _playerSpeed -=  Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        _playerSpeed = 0;
    }

    public float GetRealPlayerSpeed() => _playerSpeed;

    public float GetNormalizedPlayerSpeed() => GetRealPlayerSpeed() * 10f;
        
    public float GetPlayerStrafeSpeed() => _playerStrafeSpeed;
    public float SetPlayerSpeed(float speed) => _playerSpeed = speed;
}