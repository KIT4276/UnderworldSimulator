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
    [Space]
    [SerializeField] private ParameterUnit[] _parameters;

    [Inject] private CraftSystem _craftSystem;
    [Inject] private MaterialsData _materialsData;
    [Inject] private InventorySystem _inventorySystem;

    private void Start()
    {
        _craftSystem.ChangeCount += FillSign;

        foreach (var slot in _inventorySystem.InventorySlots)
            slot.ChangeCraftCount += FillSign;
    }

    public void FillSign()
    {
        //Debug.Log(_craftSystem.ActiveDrawing);

        if (_craftSystem.ActiveDrawing == null)
        {
            _name.text = string.Empty;
            _icon.gameObject.SetActive(false);
        }
        else
        {
            _icon.gameObject.SetActive(true);
            _name.text = _craftSystem.ActiveDrawing.Name;
            _icon.sprite = _craftSystem.ActiveDrawing.Icon;
            Debug.Log(_parameters.Length);
            foreach (var param in _parameters)
            {
                param.FillEmpty();
            }

            for (int j = 0; j < _parameters.Length; j++)
            {
                if (_craftSystem.ActiveDrawing.Decor.Parameters.Parameters[j].Value > 0)
                    _parameters[j].Fill(_craftSystem.ActiveDrawing.Decor.Parameters.Parameters[j]);
            }

            int i = 0;
            for (; i < _craftSystem.ActiveDrawing.DrawingComponents.Length; i++)
            {
                //_mainDrawingFields[i].Material.text = _materialsData.GetMaterialsHint(_craftSystem.ActiveDrawing.DrawingComponents[i].Material)/*.ToString()*/;
                //_mainDrawingFields[i].MaterialsCount.text =(_craftSystem.ActiveDrawing.DrawingComponents[i].Count * _craftSystem.Count).ToString();
                //_mainDrawingFields[i].MaterialsIcon.gameObject.SetActive(true);
                //_mainDrawingFields[i].MaterialsIcon.sprite = _materialsData.GetMaterialsIcon(_craftSystem.ActiveDrawing.DrawingComponents[i].Material);
                //_mainDrawingFields[i].AvailableCount.text = _inventorySystem.CalculateAvailableMaterial(_craftSystem.ActiveDrawing.DrawingComponents[i].Material).ToString();
                //_mainDrawingFields[i].Slash.SetActive(true);

                string matName = _materialsData.GetMaterialsHint(_craftSystem.ActiveDrawing.DrawingComponents[i].Material);
                Sprite matIcon = _materialsData.GetMaterialsIcon(_craftSystem.ActiveDrawing.DrawingComponents[i].Material);
                float neededCount = (_craftSystem.ActiveDrawing.DrawingComponents[i].Count * _craftSystem.Count);
                float AvailableCount = _inventorySystem.CalculateAvailableMaterial(_craftSystem.ActiveDrawing.DrawingComponents[i].Material);



                _mainDrawingFields[i].Fill(matName, matIcon, neededCount, AvailableCount);
            }

            if (_mainDrawingFields.Length > _craftSystem.ActiveDrawing.DrawingComponents.Length)
            {
                for (; i < _mainDrawingFields.Length; i++)
                {
                    //_mainDrawingFields[i].Material.text = string.Empty;
                    //_mainDrawingFields[i].MaterialsCount.text = string.Empty;
                    //_mainDrawingFields[i].AvailableCount.text = string.Empty;
                    //_mainDrawingFields[i].MaterialsIcon.gameObject.SetActive(false);
                    //_mainDrawingFields[i].Slash.SetActive(false);

                    _mainDrawingFields[i].FillEmpty();
                }
            }
        }
    }

    private void OnDestroy()
    {
        _craftSystem.ChangeCount -= FillSign;
    }
}
