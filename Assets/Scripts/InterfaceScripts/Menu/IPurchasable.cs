using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPurchasable<T>
{
    public int Price{get; set;}
    public void ReloadModel();
    public GameObject GetModel();
    public string GetName();
    public int GetPrice();
    public bool IsBought { get; set; }

    public T GetWithID(int id);
    public bool Buy();
    public void SetGlobalSelectedID(int id);
    
}
