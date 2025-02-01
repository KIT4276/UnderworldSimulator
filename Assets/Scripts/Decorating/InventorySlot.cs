using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(ButtonEnterChangeImage), typeof(InventorySlotCounter))]
public class InventorySlot : MonoBehaviour
{
    [SerializeField] private bool _isOccupied; // temporary
    [Space]
    [SerializeField] private Image _buttonIconImage;
    [SerializeField] private ButtonEnterChangeImage _button;
    [SerializeField] private InventorySlotCounter _slotCounter;
    [Space]
    [SerializeField] private Sprite _icon;

    public bool IsOccupied { get; private set; }

    public void Initialized()
    {
        if (_button == null || _buttonIconImage == null) return;


        IsOccupied = _isOccupied;// temporary

        if (IsOccupied)
        {
            _button.enabled = true;
            _buttonIconImage.gameObject.SetActive(true);
            _button.Activate();
            _buttonIconImage.sprite = _icon;
        }
        else
        {
            
            _button.enabled = false;
            _buttonIconImage.gameObject.SetActive(false);
        }
    }

    public void SetDecor(Decor decor)
    {
        _button.enabled = true;
        IsOccupied = true; 
        _buttonIconImage.gameObject.SetActive(true);
        _buttonIconImage.sprite = decor.GetIcon();
        _button.Activate();
        _slotCounter.SetCount(1);
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

