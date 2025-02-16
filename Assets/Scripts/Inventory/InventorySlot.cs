using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(ButtonEnterChangeImage))]
public class InventorySlot : MonoBehaviour
{
    [SerializeField] protected Image _buttonIconImage;
    [SerializeField] protected ButtonEnterChangeImage _buttonEnterChangeImage;
    [SerializeField] protected Button _button;
    [Space]
    [SerializeField] protected TMP_Text _textMeshPro;
    [SerializeField] protected GameObject _x_TextTablet;

    public bool IsOccupied { get; protected set; }

    public event Action InitializedAction;

    protected List<BaseItem> Items;
    protected Sprite _icon;

    public void Initialize()
    {
        if (Items == null)
        {
            Items = new List<BaseItem>();
        }
        SettingParameters();
        InitializedAction?.Invoke();
    }
    public BaseItem GetLastItems()
    {
        if (Items == null)
            Initialize();
        var item = Items[^1];
        return item;
    }

    public BaseItem TakeLastItem()
    {
        var inventObj = Items[^1];
        Items.Remove(Items[^1]);
        CheckingAndShow();
        return inventObj;
    }

    public void SetItem(BaseItem item)
    {
        Items ??= new List<BaseItem>();

        IsOccupied = true;
        _icon = item.GetIcon();
        Items.Add(item);
        SettingParameters();
        CheckingAndShow();
    }

    protected void SettingParameters()
    {
        if (IsOccupied)
        {
            _button.interactable = true;
            _button.enabled = true;
            _buttonIconImage.gameObject.SetActive(true);
            _buttonEnterChangeImage.enabled = true;
            _buttonEnterChangeImage.Activate();
            _buttonIconImage.sprite = _icon;
        }
        else
        {
            Deactivate();
        }
        CheckingAndShow();
    }

    protected void Deactivate()
    {
        _buttonEnterChangeImage.enabled = false;
        IsOccupied = false;
        _buttonIconImage.sprite = null;
        _buttonEnterChangeImage.DeActivate();
        _buttonIconImage.gameObject.SetActive(false);
        _button.interactable = false;
        _icon = null;
    }

    protected void CheckingAndShow()
    {
        if (Items.Count > 1)
        {
            _textMeshPro.gameObject.SetActive(true);
            _x_TextTablet.SetActive(true);
            _textMeshPro.text = Items.Count.ToString();
        }
        else
        {
            _textMeshPro.gameObject.SetActive(false);
            _x_TextTablet.SetActive(false);

            if (Items.Count <= 0)
            {
                Deactivate();
            }
        }
    }
}