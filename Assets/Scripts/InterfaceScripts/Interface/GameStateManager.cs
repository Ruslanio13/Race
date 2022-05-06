using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStateManager : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private float _timeKoef;
    [SerializeField] private float _maxOncomingDrivingMultiplier;
    [SerializeField] private float _bonusRisingKoef;
    public static GameStateManager _instance;

    public UnityAction OnGamePaused;
    public UnityAction OnGameUnpaused;
    public UnityAction OnGameOver;
    public UnityAction<float> OnMultiplierChanged;

    public bool IsGamePaused { get; private set; }
    public bool IsGameOver { get; private set; }
    public float TopSpeedForRide { get; set; }
    public float ScoreMultiplier { get; set; }
    public bool DrivingOnOncoming { get; set; }

    public int MoneyAmount { get; set; }
    public float TopSpeed { get; set; }
    public float Distance { get; set; }
    public int SelectedCarID { get; set; }
    public int SelectedMapID { get; set; }

    public Car SelectedCar;
    public Map SelectedMap;
    public List<Car> AvailableCars = new List<Car>();
    public List<Map> AvailableMaps = new List<Map>();


    private void FixedUpdate()
    {
        if (_player == null)
            return;
        _instance.Distance += (int)(_player.GetNormalizedPlayerSpeed()/ _timeKoef * ScoreMultiplier);

        if (TopSpeedForRide < _player.GetNormalizedPlayerSpeed())
            TopSpeedForRide = _player.GetNormalizedPlayerSpeed();

        HandleMultiplier();

    }

    private void HandleMultiplier()
    {
        if (DrivingOnOncoming && ScoreMultiplier < _maxOncomingDrivingMultiplier)
        {
            ScoreMultiplier += Time.fixedDeltaTime * _bonusRisingKoef;
            OnMultiplierChanged?.Invoke(ScoreMultiplier);
        }
        if (!DrivingOnOncoming && ScoreMultiplier > 1)
        {
            ScoreMultiplier -= Time.fixedDeltaTime * _bonusRisingKoef;
            OnMultiplierChanged?.Invoke(ScoreMultiplier);
        }
    }

    void Awake()
    {
        if (_instance == null)
            _instance = this;
        SaveManager._instance.LoadGame();
        SelectedCar.ReloadModel();
        SelectedMap.ReloadModel();
    }

    void Start()
    {   
        IsGamePaused = false;
        IsGameOver = false;
        ScoreMultiplier = 1;
        DrivingOnOncoming = false;

        OnGamePaused += () => { IsGamePaused = true; };
        OnGameUnpaused += () => { IsGamePaused = false; };
        OnGameOver += () =>
        {
            UIWindowManager._instance.OnGameOver?.Invoke();
            IsGameOver = true;
        };

    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void OnDestroy()
    {
        SaveManager._instance.SaveGame();
    }

}
