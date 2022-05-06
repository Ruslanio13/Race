using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

[Serializable]
public abstract class Map : IPurchasable<Map>
{
    protected string _name;

    public bool IsBought{get; set;}
    public int Price{get; set;}
    [NonSerialized]
    protected GameObject _model;
    [NonSerialized]
    protected Image _preview;

    public GameObject GetModel() => _model;
    public abstract void ReloadModel();

    public string GetName() => _name;
    public int GetPrice() => Price;

    public Map GetWithID(int id) => GameStateManager._instance.AvailableMaps[id];
    public void SetGlobalSelectedID(int id) => GameStateManager._instance.SelectedMapID = id;

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
}
[Serializable]
public class Holes : Map
{
    public Holes()
    {
        Price = 0;
        IsBought = true;
    }
    public override void ReloadModel()
    {
        _model = Resources.Load<GameObject>("Prefabs/Holes");
        _preview = Resources.Load<Image>("Prefabs/HolesPreview");
    }
}
[Serializable]
public class Forest : Map
{
    public Forest()
    {
        Price = 10;
        IsBought = false;
    }
    public override void ReloadModel()
    {
        _model = Resources.Load<GameObject>("Prefabs/Forest");
        _preview = Resources.Load<Image>("Prefabs/ForestPreview");
    }
}