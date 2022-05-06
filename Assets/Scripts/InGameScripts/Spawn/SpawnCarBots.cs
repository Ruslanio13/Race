using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SpawnCarBots : MonoBehaviour
{
    [SerializeField] private GameObject[] _spawnPoints;
    [SerializeField] private CarBots[] _cars;
    [SerializeField] private int _difficulty;
    [Header("Убедись, что player не null, так как это не приводит к ошибке выполнения")]
    [SerializeField] private Player _player;
    [SerializeField] private List<GameObject> _freeSpawns = new List<GameObject>();
    [SerializeField] private List<CarBots> _carsBuffer = new List<CarBots>();

    public int _activeCars;
    private int _randomSpawn;

    private void Start()
    {
        foreach (GameObject i in _spawnPoints)
            _freeSpawns.Add(i);

        StartCoroutine(SpawnCars());
    }

    private IEnumerator SpawnCars()
    {
        while (true)
        {
            while (_difficulty > _activeCars)
            {
                SpawnNewCar();
                yield return new WaitForSeconds(0.5f);
            }
            yield return new WaitForSeconds(0.5f);
        }
    }

    public List<GameObject> GetFreeSpawns() => _freeSpawns;

    public void AddFreeSpawnToList(GameObject spawn) => _freeSpawns.Add(spawn);

    void SpawnNewCar()
    {
        int _randomCar;

        if (_freeSpawns.Count == 0)
            return;

        _randomSpawn = UnityEngine.Random.Range(0, _freeSpawns.Count);

            _randomCar = UnityEngine.Random.Range(0, _carsBuffer.Count);
            _carsBuffer[_randomCar].transform.position = _freeSpawns[_randomSpawn].transform.position;
            _carsBuffer[_randomCar].IsBuffered = false;
            _carsBuffer[_randomCar].SetInformation(_player, this, _freeSpawns[_randomSpawn].GetComponent<SpawnPointScript>().OnComing);
            _carsBuffer.RemoveAt(_randomCar);
        
        _freeSpawns.RemoveAt(_randomSpawn);
        _activeCars++;


    }

    public void AddCarToBuffer(CarBots car)
    {
        _activeCars--;
        _carsBuffer.Add(car);
        car.gameObject.transform.position += new Vector3(20 * (car.CarID + 1), 20 * (car.CarID + 1));
    }
}
