using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(ButtonEnterChangeImage), typeof(InventorySlotCounter))]
public class InventorySlot : MonoBehaviour
{
    [SerializeField] private bool _isOccupied; // temporary
    [Space]
    [SerializeField] private Image _buttonIconImage;
    [SerializeField] private ButtonEnterChangeImage _buttonEnterChangeImage;
    [SerializeField] private InventorySlotCounter _slotCounter;
    [SerializeField] private Button _button;
    [Space]
    [SerializeField] private Sprite _icon;

    public bool IsOccupied { get; private set; }

    public void Initialized()
    {
        if (_button == null || _buttonIconImage == null) return;


        IsOccupied = _isOccupied;// temporary

        if (IsOccupied)
        {
            _button.interactable = true;
            _button.enabled = true;
            _buttonIconImage.gameObject.SetActive(true);
            _buttonEnterChangeImage.Activate();
            _buttonIconImage.sprite = _icon;
        }
        else
        {

            _buttonEnterChangeImage.enabled = false;
            _buttonIconImage.gameObject.SetActive(false);
        }
    }

    public void SetDecor(Decor decor)
    {
        _button.interactable = true;
        _buttonEnterChangeImage.enabled = true;
        IsOccupied = true; 
        _buttonIconImage.gameObject.SetActive(true);
        _buttonIconImage.sprite = decor.GetIcon();
        _buttonEnterChangeImage.Activate();
        _slotCounter.SetCount(1);
    }

    public void Deactivate()
    {
        _buttonEnterChangeImage.enabled = false;
        IsOccupied = false;
        _buttonIconImage.sprite = null;
        _buttonEnterChangeImage.DeActivate();
        _buttonIconImage.gameObject.SetActive(false);
        _button.interactable = false;
    }
}

