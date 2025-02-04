using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(ButtonEnterChangeImage), typeof(InventorySlotCounter))]
public class InventorySlot : MonoBehaviour
{
    //[SerializeField] private bool _isOccupied; // temporary
    //[Space]
    [SerializeField] private Image _buttonIconImage;
    [SerializeField] private ButtonEnterChangeImage _buttonEnterChangeImage;
    [SerializeField] private InventorySlotCounter _slotCounter;
    [SerializeField] private Button _button;
    //[Space]
    //[SerializeField]
    private Sprite _icon;

    public bool IsOccupied { get; private set; }
    public Decor CurrentDecor { get; private set; }

    public event Action OnInitialized;

    private void Awake()
    {
        SettingParameters();
    }

    public void Initialized()
    {
        SettingParameters();
        OnInitialized?.Invoke();
    }

    private void SettingParameters()
    {
        //Debug.Log("SettingParameters");

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
    }

    public void SetDecor(Decor decor)
    {
        //Debug.Log("SetDecor");

        //_button.interactable = true;
        //_buttonEnterChangeImage.enabled = true;
        IsOccupied = true;
        //_buttonIconImage.gameObject.SetActive(true);
        _icon = decor.GetIcon();
        //_buttonIconImage.sprite = _icon;
        //_buttonEnterChangeImage.Activate();
        _slotCounter.SetCount(1);
        CurrentDecor = decor;

        SettingParameters();
    }

    public void Deactivate()
    {
        _buttonEnterChangeImage.enabled = false;
        IsOccupied = false;
        _buttonIconImage.sprite = null;
        _buttonEnterChangeImage.DeActivate();
        _buttonIconImage.gameObject.SetActive(false);
        _button.interactable = false;
        CurrentDecor = null;
        _icon = null;
    }
}