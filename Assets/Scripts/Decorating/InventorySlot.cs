using UnityEngine;
using UnityEngine.UI;

[RequireComponent (typeof(ButtonEnterChangeImage), typeof(InventorySlotCounter))]
public class InventorySlot : MonoBehaviour
{
    [SerializeField] private bool _isOccupied; // temporary
    [SerializeField] private GameObject _inventoryGameObject;// temporary
    [Space]
    [SerializeField] private Image _buttonIconImage;
    [SerializeField] private ButtonEnterChangeImage _button;


    public IInventoryObject InventoryObject { get; private set; }

    public bool IsOccupied{ get; private set; }

    private void Start()
    {
        if(_buttonIconImage == null)return;

        _buttonIconImage.gameObject.SetActive(false);
        IsOccupied = _isOccupied;// temporary
        InventoryObject = _inventoryGameObject.GetComponent<IInventoryObject>();// temporary
    }

    public void Initialized()
    {
        if (_button == null) return;
        
        if (IsOccupied)
        {
            _button.enabled = true;
            _buttonIconImage.gameObject.SetActive(true);
            if (InventoryObject is Decor)
            {
                // сделать фон розовым, иконку из декора
                _button.Activate();
                Debug.Log(InventoryObject.GetIcon());
                _buttonIconImage.sprite = InventoryObject.GetIcon();
            }
        }
        else
        {
            _button.enabled = false;

        }
    }
}

