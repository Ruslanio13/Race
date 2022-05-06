
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CarBots : MonoBehaviour
{
    [SerializeField] private float _defaultSpeed;
    [SerializeField] private int _carID;
    [SerializeField] private float _calculatedSpeed;
    private float _speedCarBotBeforePause;
    private SpawnCarBots _spawnController;
    private bool _isBuffered = false;

    public bool IsOnComing { get; private set; } = true;
    public bool IsBuffered { get { return _isBuffered; } set { _isBuffered = value; } }

    public int CarID { get => _carID; private set => _carID = value; }

    private Player _player;
    private float _directionBotCar;

    public void Start()
    {
        GameStateManager._instance.OnGamePaused += OnPause;
        GameStateManager._instance.OnGameUnpaused += OnUnpause;
        GameStateManager._instance.OnGameOver += OnPlayerDeath;

    }

    private void RecalculateSpeed()
    {
        if (_player == null)
            _calculatedSpeed = _defaultSpeed;
        else
            _calculatedSpeed = _defaultSpeed - _directionBotCar * _player.GetRealPlayerSpeed();
    }

    private void OnPlayerDeath()
    {
        StartCoroutine(SubtractPlayerSpeed());
    }

    private IEnumerator SubtractPlayerSpeed()
    {
        while (Math.Abs(_calculatedSpeed - _defaultSpeed) > 0.01)
        {
            RecalculateSpeed();
            yield return new WaitForEndOfFrame();
        }
    }

    void FixedUpdate()
    {
        if (GameStateManager._instance.IsGamePaused || _isBuffered)
            return;
     
        RecalculateSpeed();
            transform.Translate(Vector3.down * _calculatedSpeed * Time.fixedDeltaTime);
    }

    public void SetInformation(Player player, SpawnCarBots spawnCarBots, bool isOnComing)
    {
        _player = player;
        _spawnController = spawnCarBots;

        if (IsOnComing != isOnComing)
            transform.Rotate(new Vector3(0, 0, 180));

        IsOnComing = isOnComing;
        if (isOnComing)
            _directionBotCar = -1;
        else
            _directionBotCar = 1;

    }

    private void OnPause()
    {
        _speedCarBotBeforePause = _calculatedSpeed;
        _calculatedSpeed = 0;
    }
    private void OnUnpause()
    {
        _calculatedSpeed = _speedCarBotBeforePause;
    }

    public void SetSpeed(float speed) => _defaultSpeed = speed;
    public float GetSpeed() => _defaultSpeed;
    public float GetRefSpeed() => _calculatedSpeed;
    public void SetID(int id) => CarID = id;

    private void OnDestroy()
    {
        GameStateManager._instance.OnGameOver -= OnPlayerDeath;
        GameStateManager._instance.OnGamePaused -= OnPause;
        GameStateManager._instance.OnGameUnpaused -= OnUnpause;
    }
    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "VerticalBorder" && !_isBuffered)
        {
            _isBuffered = true;
            _spawnController.AddCarToBuffer(this);
        }
    }
}
