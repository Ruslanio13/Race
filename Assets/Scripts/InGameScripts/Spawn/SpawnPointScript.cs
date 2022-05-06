using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPointScript : MonoBehaviour
{
    [SerializeField] private SpawnCarBots _spawnArray;
    [SerializeField] private bool _onComing;
    [SerializeField] private Player _player;


    public bool OnComing { get { return _onComing; } }



    private void OnTriggerExit(Collider carCol)
    {
        if (carCol.tag == "Car")
        {

            if (!_spawnArray.GetFreeSpawns().Contains(gameObject))
            {
                _spawnArray.AddFreeSpawnToList(gameObject);
            }
        }
    }

}