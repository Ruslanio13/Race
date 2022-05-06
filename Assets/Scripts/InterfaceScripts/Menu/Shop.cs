using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Shop<T> : MonoBehaviour where T:IPurchasable<T>
{
    [SerializeField] protected Text _itemName;
    [SerializeField] protected Text _itemPrice;

    [SerializeField] protected float _switchAnimationSpeed;

    [SerializeField] protected Button _nextItemButton;
    [SerializeField] protected Button _prevItemButton;
    [SerializeField] protected Button _selectItemButton;
    [SerializeField] protected Button _buyItemButton;

    [SerializeField] private Sprite _selectButtonActive;
    [SerializeField] private Sprite _selectButtonInactive;

    [SerializeField] protected int _shownItemID;

    [SerializeField] private GameObject _shownItemModelParent;

    protected UnityAction<int> ShowNewItem;

    protected UnityAction GoToNextItemAction;
    protected UnityAction GoToPrevItemAction;
    protected UnityAction SelectItemAction;
    protected UnityAction BuyItemAction;

    protected T _shownItem;

    private GameObject _shownItemModel;

    protected void BaseInitialize()
    {
        ShowNewItem += (int dir) => { StartCoroutine(MoveItemFromSideIE(dir)); };
        
        GoToNextItemAction += () => SwitchItem(1);
        _nextItemButton.onClick.AddListener(GoToNextItemAction);
        
        GoToPrevItemAction += () => SwitchItem(-1);
        _prevItemButton.onClick.AddListener(GoToPrevItemAction);
 
        BuyItemAction += BuyItem;
        _buyItemButton.onClick.AddListener(BuyItemAction);
 
        SelectItemAction += SelectItem;
        _selectItemButton.onClick.AddListener(SelectItemAction);
    }

    private void SwitchItem(int dir)
    {
        if (GameStateManager._instance.AvailableCars.Count > (_shownItemID + dir))
        {
            _shownItemID += dir;

            _shownItem = _shownItem.GetWithID(_shownItemID);

            _selectItemButton.gameObject.SetActive(false);
            _buyItemButton.gameObject.SetActive(false);
            ShowNewItem.Invoke(-dir);
        }
    }


    public void UpdateShownItem(bool updateModel = true)
    {
        if (updateModel)
        {
            if (_shownItemModel != null)
                _shownItemModel.SetActive(false);
            _shownItem.ReloadModel();
            _shownItemModel = Instantiate(_shownItem.GetModel(), _shownItemModelParent.transform);
            _shownItemModel.SetActive(true);
        }
        _itemName.text = _shownItem.GetName();

        if (!_shownItem.IsBought)
        {
            _buyItemButton.gameObject.SetActive(true);
            _itemPrice.text = "Price: " + _shownItem.GetPrice().ToString();
        }
        else
        {
            _selectItemButton.gameObject.SetActive(true);
            _itemPrice.text = "Bought";

            _selectItemButton.enabled = !(GameStateManager._instance.SelectedCarID == _shownItemID);
            if (_selectItemButton.enabled)
                _selectItemButton.image.sprite = _selectButtonActive;
            else
                _selectItemButton.image.sprite = _selectButtonInactive;
        }


    }
    IEnumerator MoveItemFromSideIE(int dir) //1 - from left to right | -1 - from right to left
    {
        _nextItemButton.enabled = false;
        _prevItemButton.enabled = false;
        while (dir * _shownItemModelParent.transform.localPosition.x < 3000)
        {
            _shownItemModelParent.transform.localPosition += Vector3.right * dir * _switchAnimationSpeed * Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
        }
        UpdateShownItem();
        _shownItemModelParent.transform.localPosition = new Vector3(-dir * 3000f, 0f, _shownItemModelParent.transform.localPosition.z);
        while (-dir * _shownItemModelParent.transform.localPosition.x > 0)
        {
            _shownItemModelParent.transform.localPosition += Vector3.right * dir * _switchAnimationSpeed * Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
        }

        _nextItemButton.enabled = true;
        _prevItemButton.enabled = true;
        _shownItemModelParent.transform.localPosition = new Vector3(0, 0, _shownItemModelParent.transform.localPosition.z);

    }

    private void BuyItem()
    {

        if (_shownItem.Buy())
        {
            Debug.Log("Car Bought!");
            _buyItemButton.gameObject.SetActive(false);
            _selectItemButton.gameObject.SetActive(true);
            _selectItemButton.enabled = true;
            _selectItemButton.image.sprite = _selectButtonActive;
        }
        else
            Debug.Log("Not Enough Money");
    }
    private void SelectItem()
    {
        _shownItem.SetGlobalSelectedID(_shownItemID);
        _selectItemButton.enabled = false;
        _selectItemButton.image.sprite = _selectButtonInactive;
    }
}
