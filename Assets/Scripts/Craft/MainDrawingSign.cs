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
    [SerializeField] private TMP_Text[] _material;
    [SerializeField] private TMP_Text[] _materialscount;
    [Space]
    [SerializeField] private TMP_Text[] _availableCount;
    [SerializeField] private Image[] _materialsIcon;

    [Inject] private CraftSystem _craftSystem;
    [Inject] private MaterialsData _materialsData;

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
            _material[i].text = _craftSystem.ActiveDrawing.DrawingComponents[i].Material.ToString();
            _materialscount[i].text = (_craftSystem.ActiveDrawing.DrawingComponents[i].Count*_craftSystem.Count).ToString();
            _materialsIcon[i].sprite = _materialsData.GetMaterialsIcon(_craftSystem.ActiveDrawing.DrawingComponents[i].Material);
            //todo Fill  _availableCount from Craft System
        }

        if (_material.Length > _craftSystem.ActiveDrawing.DrawingComponents.Length)
        {
            for (; i < _material.Length; i++)
            {
                _material[i].text = "";
                _materialscount[i].text = "";
            }
        }
    }
}