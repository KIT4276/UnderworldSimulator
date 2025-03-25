using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class MainDrawingSign : MonoBehaviour
{
    [SerializeField] private TMP_Text _name;
    [SerializeField] private Image _icon;
    [Space, Tooltip("Материалы")]
    [SerializeField] private MateialsField[] _mainDrawingFields;

    [Inject] private CraftSystem _craftSystem;
    [Inject] private MaterialsData _materialsData;
    [Inject] private InventorySystem _inventorySystem;

    private void Start()
    {
        _craftSystem.ChangeCount += FillSign;

        foreach(var slot in _inventorySystem.InventorySlots)
            slot.ChangeCraftCount += FillSign;
    }

    public void FillSign()
    {
        //Debug.Log(_craftSystem.ActiveDrawing);
        _name.text = _craftSystem.ActiveDrawing.Name;
        _icon.sprite = _craftSystem.ActiveDrawing.Icon;

        int i = 0;
        for (; i < _craftSystem.ActiveDrawing.DrawingComponents.Length; i++)
        {
            _mainDrawingFields[i].Material.text = _craftSystem.ActiveDrawing.DrawingComponents[i].Material.ToString();
            _mainDrawingFields[i].MaterialsCount.text = (_craftSystem.ActiveDrawing.DrawingComponents[i].Count * _craftSystem.Count).ToString();
            _mainDrawingFields[i].MaterialsIcon.gameObject.SetActive(true);
            _mainDrawingFields[i].MaterialsIcon.sprite = _materialsData.GetMaterialsIcon(_craftSystem.ActiveDrawing.DrawingComponents[i].Material);
            _mainDrawingFields[i].AvailableCount.text = _inventorySystem.CalculateMaterial(_craftSystem.ActiveDrawing.DrawingComponents[i].Material).ToString();
            _mainDrawingFields[i].Slash.SetActive(true);
        }

        if (_mainDrawingFields.Length > _craftSystem.ActiveDrawing.DrawingComponents.Length)
        {
            for (; i < _mainDrawingFields.Length; i++)
            {
                _mainDrawingFields[i].Material.text = string.Empty;
                _mainDrawingFields[i].MaterialsCount.text = string.Empty;
                _mainDrawingFields[i].AvailableCount.text = string.Empty;
                _mainDrawingFields[i].MaterialsIcon.gameObject.SetActive(false);
                _mainDrawingFields[i].Slash.SetActive(false);
            }
        }
    }

    private void OnDestroy()
    {
        _craftSystem.ChangeCount -= FillSign;
    }
}
