using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(ButtonEnterChangeImage), typeof(InventorySlotCounter))]
public class InventorySlot : MonoBehaviour
{
    [SerializeField] private Image _buttonIconImage;
    [SerializeField] private ButtonEnterChangeImage _buttonEnterChangeImage;
    [SerializeField] private InventorySlotCounter _slotCounter;
    [SerializeField] private Button _button;
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
        //Debug.Log(decor);
        IsOccupied = true;
        _icon = decor.GetIcon();
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