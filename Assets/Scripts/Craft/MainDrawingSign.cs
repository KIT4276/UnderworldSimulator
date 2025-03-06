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
    [SerializeField] private MainDrawingFields[] _mainDrawingFields;
    //[SerializeField] private TMP_Text[] _material;
    //[SerializeField] private TMP_Text[] _materialscount;
    //[Space]
    //[SerializeField] private TMP_Text[] _availableCount;
    //[SerializeField] private Image[] _materialsIcon;

    [Inject] private CraftSystem _craftSystem;
    [Inject] private MaterialsData _materialsData;
    [Inject] private InventorySystem _inventorySystem;

    private void Start()
    {
        _craftSystem.ChangeCount += FillSign;
    }

    public void FillSign()
    {
        _name.text = _craftSystem.ActiveDrawing.Name;
        _icon.sprite = _craftSystem.ActiveDrawing.Icon;

        int i = 0;
        for (; i < _craftSystem.ActiveDrawing.DrawingComponents.Length; i++)
        {
            _mainDrawingFields[i].Material.text = _craftSystem.ActiveDrawing.DrawingComponents[i].Material.ToString();
            _mainDrawingFields[i].MaterialsCount.text = (_craftSystem.ActiveDrawing.DrawingComponents[i].Count * _craftSystem.Count).ToString();
            _mainDrawingFields[i].MaterialsIcon.sprite = _materialsData.GetMaterialsIcon(_craftSystem.ActiveDrawing.DrawingComponents[i].Material);
            _mainDrawingFields[i].AvailableCount.text = _inventorySystem.CalculateMaterial(_craftSystem.ActiveDrawing.DrawingComponents[i].Material).ToString();
            //Debug.Log(_craftSystem.ActiveDrawing.DrawingComponents[i].Material);
            //Debug.Log(_craftSystem.ActiveDrawing.DrawingComponents[i].Count * _craftSystem.Count);
            //Debug.Log(_inventorySystem.CalculateMaterial(_craftSystem.ActiveDrawing.DrawingComponents[i].Material));
            //Debug.Log("------------------------------------------------------");
        }

        if (_mainDrawingFields.Length > _craftSystem.ActiveDrawing.DrawingComponents.Length)
        {
            for (; i < _mainDrawingFields.Length; i++)
            {
                _mainDrawingFields[i].Material.text = "";
                _mainDrawingFields[i].MaterialsCount.text = "";
                _mainDrawingFields[i].AvailableCount.text = "";
               //_mainDrawingFields[i].MaterialsIcon.sprite = ???;
            }
        }
    }

    private void OnDestroy()
    {
        _craftSystem.ChangeCount -= FillSign;
    }
}

[Serializable]
public class MainDrawingFields
{
    [SerializeField] private TMP_Text _nameText;
    [SerializeField] private TMP_Text _neededCount;
    [SerializeField] private Image _materialsIcon;
    [Space]
    [SerializeField] private TMP_Text _availableCount;

    public TMP_Text Material { get => _nameText; }
    public TMP_Text MaterialsCount { get => _neededCount; }
    public Image MaterialsIcon { get => _materialsIcon; }
    public TMP_Text AvailableCount { get => _availableCount; }


}