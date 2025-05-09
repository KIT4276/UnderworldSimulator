using System;
using TMPro;
using UnityEngine;
using Zenject;

public class CraftMenu : MonoBehaviour
{
    [SerializeField] private CraftSlot[] _slots;
    [SerializeField] private GameObject _menu;
    [SerializeField] private MainDrawingSign _mainDrawingSign;
    [SerializeField] private TMP_Text _count;
    [SerializeField] private FadeInSign _notEnoughSign;
    [SerializeField] private CraftButton _craftButton;
    [SerializeField] private FadeInSign _catnCraftSign;

    [Inject] private CraftSystem _craftSystem;
    [Inject] private WorkbenchSystem _workbenchSystem;
    [Inject] private StateMachine _machine;
    [Inject] private ParameterData _parameterData;

    private void Start()
    {
        //_workbenchSystem.CraftButtonClick += OpenCraftMenu;//todo to state change
        _craftSystem.ChangeCount += UpdateCount;
        _machine.ChangeStateAction += StateChanged;
        _craftSystem.Crafted += ResetCraftMenu;
        _craftSystem.DrawingAdded += FillSlots;
        _craftSystem.NotEnoughMaterials += NotEnough;

        foreach (var slot in _slots)
        {
            slot.DrawingSelected += ToSelectDrawing;
        }

        //FillSlots();

        //StartFill(_craftSystem.ActiveDrawing);
        UpdateCount();
        CloseCraftMenu();
        _notEnoughSign.gameObject.SetActive(false);
    }

    private void NotEnough()
    {
        //Debug.Log("NotEnough");
        _notEnoughSign.gameObject.SetActive(true);
        _notEnoughSign.StartFadeIn();
    }

    private void FillSlots()
    {
        // Debug.Log(_craftSystem.AvailableDrawings.Count);
        if (_craftSystem.AvailableDrawings.Count > _slots.Length)
        {
            Debug.LogWarning("������ ������, ��� ��������!");
        }
        else
        {
            foreach (var slot in _slots)
            {
                slot.FillEmpty();
            }

            int i = 0;
            for (; i < _craftSystem.AvailableDrawings.Count; i++)
            {
                _slots[i].FillDrawingData(_craftSystem.AvailableDrawings[i], _parameterData);
            }

            //if (_craftSystem.AvailableDrawings.Count < _slots.Length)
            //{
            //    for (; i < _slots.Length; i++)
            //    {
            //        _slots[i].FillEmpty();
            //    }
            //}
        }
    }

    public void OnCreate()
    {
        if (_machine.ActiveState is PseudoCraftState)
        {
            _catnCraftSign.gameObject.SetActive(true);
            _catnCraftSign.StartFadeIn();

        }
        else if (_machine.ActiveState is CraftState)
        {
            _craftSystem.CreateDecor();
        }
        AudioReciever.Instance.PlayUIGeneralClick("OnCreate");
    }

    private void UpdateCount()
    {
        _count.text = _craftSystem.Count.ToString();
        //_notEnoughSign.gameObject.SetActive(false);
    }

    public void OpenCraftMenu()
    {
        _menu.SetActive(true);
        _craftSystem.AwakeMenu();
        ResetCraftMenu();
    }

    private void ResetCraftMenu()
    {
        UpdateCount();
        _mainDrawingSign.FillSign();
        FillSlots();
        //_notEnoughSign.gameObject.SetActive(false);
    }

    public void CloseCraftMenu()
    {
        _menu.SetActive(false);
    }

    public void OnChangeCount(int count)
    {
        _craftSystem.OnChangeCount(count);
        UpdateCount();
        AudioReciever.Instance.PlayUIGeneralClick("OnChangeCount");
    }

    private void StateChanged(IExitableState state)
    {
        if (state is CraftState || state is PseudoCraftState)
        {
            OpenCraftMenu();

            if (state is PseudoCraftState)
            {
                _craftButton.Deactivate();
            }
            else
            {
                _craftButton.Activate();
            }
        }
        else
        {
            CloseCraftMenu();

        }
    }

    private void ToSelectDrawing(Drawing drawing)
    {
        _craftSystem.SelectDrawing(drawing);
        _mainDrawingSign.FillSign();
    }

    private void OnDestroy()
    {
        _workbenchSystem.CraftButtonClick -= OpenCraftMenu;
        _craftSystem.ChangeCount -= UpdateCount;
        _machine.ChangeStateAction -= StateChanged;
        _craftSystem.Crafted -= ResetCraftMenu;
        _craftSystem.DrawingAdded -= FillSlots;
        _craftSystem.NotEnoughMaterials -= NotEnough;

        _craftSystem.OnDestroy();
    }
}
