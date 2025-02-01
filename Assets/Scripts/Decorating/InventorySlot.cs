using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(ButtonEnterChangeImage), typeof(InventorySlotCounter))]
public class InventorySlot : MonoBehaviour
{
    [SerializeField] private bool _isOccupied; // temporary
    //[SerializeField] private GameObject _inventoryGameObject;// temporary
    [Space]
    [SerializeField] private Image _buttonIconImage;
    [SerializeField] private ButtonEnterChangeImage _button;
    [Space]
    [SerializeField] private Sprite _icon;


    //public IInventoryObject InventoryObject { get; private set; }

    public bool IsOccupied { get; private set; }

    //private void Start()
    //{
    //    if(_buttonIconImage == null)return;
    //    Debug.Log(IsOccupied);
    //    _buttonIconImage.gameObject.SetActive(false);
    //    IsOccupied = _isOccupied;// temporary
    //    InventoryObject = _inventoryGameObject.GetComponent<IInventoryObject>();// temporary
    //}

    public void Initialized()
    {
        if (_button == null || _buttonIconImage == null) return;


        IsOccupied = _isOccupied;// temporary
        //InventoryObject = _inventoryGameObject.GetComponent<IInventoryObject>();// temporary

        if (IsOccupied)
        {
            _button.enabled = true;
            _buttonIconImage.gameObject.SetActive(true);
            //if (InventoryObject is Decor)
            //{
            _button.Activate();
            _buttonIconImage.sprite = _icon;
            //}
        }
        else
        {
            
            _button.enabled = false;
            _buttonIconImage.gameObject.SetActive(false);
        }
    }

    public void Deactivate()
    {
        _button.enabled = false;
        IsOccupied = false;
        _buttonIconImage.sprite = null;
        _button.DeActivate();
        _buttonIconImage.gameObject.SetActive(false);
    }
}

