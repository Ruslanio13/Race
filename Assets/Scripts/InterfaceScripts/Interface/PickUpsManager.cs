using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PickUpsManager : MonoBehaviour
{

    public UnityAction<int> OnCoinPickUp;
    public UnityAction<float> OnAccelerationPickUp;

    public float BonusTimeLeft { get; set; }

    public static PickUpsManager _instance;
    private void Awake()
    {
        if (_instance == null)
            _instance = this;
    }

    private void Start()
    {
        OnCoinPickUp += (int nominallo) =>
        {
            GameStateManager._instance.MoneyAmount += nominallo;
        };
    }
}