using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnCoin : MonoBehaviour
{
    [SerializeField] private GameObject[] _spawnPoints;
    [SerializeField] private GameObject[] _coins;
    [SerializeField] private int _intervalOfSpawn;
    [SerializeField] private Player _player;
    private GameObject _spawnedCoin;
    private int _randomSpawn;
    private int _randomCoin;
    private float _timer;

    void Start()
    {
        _timer = -_intervalOfSpawn;
    }

    void FixedUpdate()
    {
        if (Time.time - _timer > _intervalOfSpawn)
        {
            _randomSpawn = Random.Range(0, _spawnPoints.Length);
            _randomCoin = Random.Range(0, _coins.Length);

            _spawnedCoin = Instantiate(_coins[_randomCoin], _spawnPoints[_randomSpawn].transform);
            _spawnedCoin?.GetComponent<MoveCoins>().SetInformation(_player.GetRealPlayerSpeed());
            _timer = Time.time;
        }
    }
}
