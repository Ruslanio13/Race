using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBonusAcceleration : MonoBehaviour
{
    [SerializeField] private GameObject[] _spawnPoints;
    [SerializeField] private GameObject _acceleration;
    [SerializeField] private int _intervalOfSpawn;
    [SerializeField] private Player _player;
    private GameObject _spawnedBonusAcceleration;
    private int _randomSpawn;
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
            _spawnedBonusAcceleration = Instantiate(_acceleration, _spawnPoints[_randomSpawn].transform);
            _spawnedBonusAcceleration?.GetComponent<Acceleration>().SetInformation(_player);
            _timer = Time.time;
            _intervalOfSpawn = Random.Range(5, 26);
        }
    }
}
