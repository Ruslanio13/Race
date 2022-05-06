using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class Car : IPurchasable<Car>
{
    protected float _speed;
    [System.NonSerialized]
    protected GameObject _carModel;
    protected float _strafeSpeed;
    protected string _name;
    protected string _modelDir;

    public int Price{get; set;}
    public bool IsBought { get; set; }

    public int SpeedUpgradeLVL { get; protected set; }
    public int StrafeUpgradeLVL;
    public int BonusAccelUpgradeLVL;

    public Car()
    {
        StrafeUpgradeLVL = 0;
        SpeedUpgradeLVL = 0;
        BonusAccelUpgradeLVL = 0;
    }
    public bool Buy()
    {
        if (GameStateManager._instance.MoneyAmount >= Price)
        {
            GameStateManager._instance.MoneyAmount -= Price;
            IsBought = true;
            return true;
        }
        return false;
    }
    public int GetPrice() => Price;
    public float GetCarStrafeSpeed() => _strafeSpeed;
    public GameObject GetModel() => _carModel;
    public void ReloadModel()
    {
        if (_modelDir != null)
            _carModel = Resources.Load<GameObject>(_modelDir);
    }



    public string GetName()
    {
        return _name;
    }
    public void SetGlobalSelectedID(int id) => GameStateManager._instance.SelectedCarID = id;
    public Car GetWithID(int id) => GameStateManager._instance.AvailableCars[id];
    public float GetCarSpeed() => _speed;
    public void ShowCar(bool isShown) => _carModel.SetActive(isShown);

    public bool BuyUpgrade(int upgradeType)
    {
        switch (upgradeType)
        {
            case 1:
                if (SpeedUpgradeLVL * Mathf.CeilToInt(Price * 0.05f) <= GameStateManager._instance.MoneyAmount)
                {
                    GameStateManager._instance.MoneyAmount -= SpeedUpgradeLVL * Mathf.CeilToInt(Price * 0.05f);
                    SpeedUpgradeLVL++;
                    _speed *= 1.05f;
                    
                    return true;
                    
                }
                else
                {
                    return false;
                }
            case 2:
                if (StrafeUpgradeLVL * Mathf.CeilToInt(Price * 0.05f) <= GameStateManager._instance.MoneyAmount)
                {
                    GameStateManager._instance.MoneyAmount -= StrafeUpgradeLVL * Mathf.CeilToInt(Price * 0.05f);
                    StrafeUpgradeLVL++;
                    _strafeSpeed *= 1.05f;
                    return true;
                }
                else
                {
                    return false;
                }
            case 3:
                if (BonusAccelUpgradeLVL * Mathf.CeilToInt(Price * 0.05f) <= GameStateManager._instance.MoneyAmount)
                {
                    GameStateManager._instance.MoneyAmount -= BonusAccelUpgradeLVL * Mathf.CeilToInt(Price * 0.05f);
                    BonusAccelUpgradeLVL++;
                    return true;
                }
                else
                {
                    return false;
                }
        }
        return false;
    }
}

[System.Serializable]
public class Skyline : Car
{
    public Skyline()
    {
        _name = "Skyline";
        _modelDir = "Prefabs/Skyline";
        _speed = 5f;
        _strafeSpeed = 2;
        Price = 200;
        IsBought = false;
        ReloadModel();
        ShowCar(true);
        Debug.Log("Speed is " + _speed);
    }
}

[System.Serializable]
public class Supra : Car
{
    public Supra()
    {
        _name = "Supra";
        _modelDir = "Prefabs/supra";
        _speed = 3.5f;
        _strafeSpeed = 2;
        Price = 50;
        IsBought = false;
        ReloadModel();
        ShowCar(true);
        Debug.Log("Speed is " + _speed);
    }
}

[System.Serializable]
public class Kopeyka : Car
{
    public Kopeyka()
    {
        _name = "2101";
        _modelDir = "Prefabs/Kopeyka";
        _speed = 3.5f;
        _strafeSpeed = 2;
        Price = 0;
        IsBought = true;
        ReloadModel();
        ShowCar(true);
        Debug.Log("Speed is " + _speed);
    }
}

[System.Serializable]
public class Camaro : Car
{
    public Camaro()
    {
        _name = "Camaro";
        _modelDir = "Prefabs/Camaro";
        _speed = 3.5f;
        _strafeSpeed = 2;
        Price = 2;
        IsBought = false;
        ReloadModel();
        ShowCar(true);
        Debug.Log("Speed is " + _speed);
    }
}

[System.Serializable]
public class Dodge : Car
{
    public Dodge()
    {
        _name = "Dodge";
        _modelDir = "Prefabs/Dodge";
        _speed = 3.5f;
        _strafeSpeed = 2;
        Price = 1;
        IsBought = false;
        ReloadModel();
        ShowCar(true);
        Debug.Log("Speed is " + _speed);
    }
}

[System.Serializable]
public class Golf : Car
{
    public Golf()
    {
        _name = "Golf";
        _modelDir = "Prefabs/Golf";
        _speed = 3.5f;
        _strafeSpeed = 2;
        Price = 4;
        IsBought = false;
        ReloadModel();
        ShowCar(true);
        Debug.Log("Speed is " + _speed);
    }
}
[System.Serializable]
public class Lambo : Car
{
    public Lambo()
    {
        _name = "Lambo";
        _modelDir = "Prefabs/Lambo";
        _speed = 3.5f;
        _strafeSpeed = 2;
        Price = 5;
        IsBought = false;
        ReloadModel();
        ShowCar(true);
        Debug.Log("Speed is " + _speed);
    }
}

[System.Serializable]
public class Lancer : Car
{
    public Lancer()
    {
        _name = "Lancer";
        _modelDir = "Prefabs/Lancer";
        _speed = 3.5f;
        _strafeSpeed = 2;
        Price = 6;
        IsBought = false;
        ReloadModel();
        ShowCar(true);
        Debug.Log("Speed is " + _speed);
    }
}

[System.Serializable]
public class Pickup : Car
{
    public Pickup()
    {
        _name = "Pickup";
        _modelDir = "Prefabs/Pickup";
        _speed = 3.5f;
        _strafeSpeed = 2;
        Price = 7;
        IsBought = false;
        ReloadModel();
        ShowCar(true);
        Debug.Log("Speed is " + _speed);
    }
}