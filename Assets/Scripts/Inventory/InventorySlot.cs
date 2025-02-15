using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(ButtonEnterChangeImage))]
public abstract class InventorySlot : MonoBehaviour
{
    [SerializeField] protected Image _buttonIconImage;
    [SerializeField] protected ButtonEnterChangeImage _buttonEnterChangeImage;
    [SerializeField] protected Button _button;
    [Space]
    [SerializeField] protected TMP_Text _textMeshPro;
    [SerializeField] protected GameObject _x_TextTablet;

    public bool IsOccupied { get; protected set; }
    public List<IInventoryObject> nventoryObject { get; protected set; }

    public event Action InitializedAction;

    protected Sprite _icon;

    public IInventoryObject TakeLastDecor()
    {
        var decor = nventoryObject[^1];
        nventoryObject.Remove(nventoryObject[^1]);
        CheckingAndShow();
        return decor;
    }

    public IInventoryObject GetLastInventoryObject()
    {
        if (nventoryObject == null)
            Initialize();
        var decor = nventoryObject[^1];
        return decor;
    }


    protected void CheckingAndShow()
    {
        if (nventoryObject.Count > 1)
        {
            _textMeshPro.gameObject.SetActive(true);
            _x_TextTablet.SetActive(true);
            _textMeshPro.text = nventoryObject.Count.ToString();
        }
        else
        {
            _textMeshPro.gameObject.SetActive(false);
            _x_TextTablet.SetActive(false);

            if (nventoryObject.Count <= 0)
            {
                Deactivate();
            }
        }
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

    public void Initialize()
    {
        if (nventoryObject == null)
        {
            nventoryObject = new List<IInventoryObject>();
        }
        SettingParameters();
        InitializedAction?.Invoke();
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
}