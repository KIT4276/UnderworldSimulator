using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using Zenject;

public class CraftMenu : MonoBehaviour
{
    [SerializeField] private CraftSlot[] _slots;
    [SerializeField] private GameObject _menu;
    [SerializeField] private MainDrawingSign _mainDrawingSign;
    [SerializeField] private TMP_Text _сount;

    [Inject] private CraftSystem _craftSystem;
    [Inject] private WorkbenchSystem _workbenchSystem;

    private void Start()
    {
        _workbenchSystem.CraftButtonClick += OpenCraftMenu;
        foreach (var slot in _slots)
        {
            slot.DrawingSelected += ToSelectDrawing;
        }

        if (_craftSystem.DrawingDatas.Drawings.Length > _slots.Length)
        {
            Debug.LogWarning("Слотов меньше, чем рецептов!");
        }
        else
        {
            
            int i = 0;
            for (; i < _craftSystem.DrawingDatas.Drawings.Length; i++)
            {
                _slots[i].FillDrawingData(_craftSystem.DrawingDatas.Drawings[i]);
            }

            if(_craftSystem.DrawingDatas.Drawings.Length < _slots.Length)
            {
                for(;i < _slots.Length; i ++)
                {
                    _slots[i].FillEmpty();
                }
            }
        }


        //CloseCraftMenu();
        _craftSystem.Created += StartFill;
    }

    private void StartFill(Drawing drawing)
    {
        _mainDrawingSign.FillSign(drawing);

        Debug.Log("start fill");// сюда не заходит!
    }

    public void OnCreate()
    {
        _craftSystem.Create();
    }

    public void OpenCraftMenu()
    {
        _menu.SetActive(true);
    }

    public void CloseCraftMenu()
    {
        _menu.SetActive(false);
    }

    public void OnChangeCount(int count)
    {

        _craftSystem.ChangeCount(count);
        _сount.text = _craftSystem.Count.ToString();
    }

    private void ToSelectDrawing(Drawing drawing)
    {
        _craftSystem.SelectDrawing(drawing);

        _mainDrawingSign.FillSign(drawing);
    }
}
