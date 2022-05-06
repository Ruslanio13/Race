using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class CarShop : Shop<Car>
{
    [SerializeField] private Button _upgradeSpeedButton;
    [SerializeField] private Button _upgradeStrafeButton;
    [SerializeField] private Button _upgradeBonusAccelButton;

    [SerializeField] private Slider _speedSlider;
    [SerializeField] private Slider _strafeSlider;

    
    private UnityAction BuyStrafeUpgradeAction;
    private UnityAction BuyBonusAccelAction;
    private UnityAction BuySpeedUpgradeAction;


    private void Start()
    {
        BaseInitialize();

        BuySpeedUpgradeAction += () => { _shownItem.BuyUpgrade(1); UpdateShownItem(false); };
        BuyStrafeUpgradeAction += () => { _shownItem.BuyUpgrade(2); UpdateShownItem(false); };
        BuyBonusAccelAction += () => { _shownItem.BuyUpgrade(3); UpdateShownItem(false); };

        _upgradeSpeedButton.onClick.AddListener(BuySpeedUpgradeAction);
        _upgradeStrafeButton.onClick.AddListener(BuyStrafeUpgradeAction);
        _upgradeBonusAccelButton.onClick.AddListener(BuyBonusAccelAction);


        _shownItemID = GameStateManager._instance.SelectedCarID;
        _shownItem = GameStateManager._instance.SelectedCar;

        _selectItemButton.gameObject.SetActive(false);
        _buyItemButton.gameObject.SetActive(false);
        UpdateShownItem();
    }

    private void BuyUpgrade(int upgradeType)
    {
        _shownItem.BuyUpgrade(upgradeType);
    }

    private void OnDestroy()
    {
        _nextItemButton.onClick.RemoveAllListeners();
        _prevItemButton.onClick.RemoveAllListeners();
        _selectItemButton.onClick.RemoveAllListeners();
        _buyItemButton.onClick.RemoveAllListeners();
    }
}
