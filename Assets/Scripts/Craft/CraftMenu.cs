using TMPro;
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
    [Inject] private StateMachine _machine;

    private void Start()
    {
        _workbenchSystem.CraftButtonClick += OpenCraftMenu;
        _craftSystem.ChangeCount += UpdateCount;
        _machine.ChangeStateAction += StateChanged;
        _craftSystem.Crafted += ResetCraftMenu;
        _craftSystem.DrawingAdded += FillSlots;

        foreach (var slot in _slots)
        {
            slot.DrawingSelected += ToSelectDrawing;
        }

        FillSlots();

        StartFill(_craftSystem.ActiveDrawing);
        UpdateCount();
        CloseCraftMenu();
    }

    private void FillSlots()
    {
        if (_craftSystem.AvailableDrawings.Count > _slots.Length)
        {
            Debug.LogWarning("Слотов меньше, чем чертежей!");
        }
        else
        {
            int i = 0;
            for (; i < _craftSystem.AvailableDrawings.Count; i++)
            {
                _slots[i].FillDrawingData(_craftSystem.AvailableDrawings[i]);
            }

            if (_craftSystem.AvailableDrawings.Count < _slots.Length)
            {
                for (; i < _slots.Length; i++)
                {
                    _slots[i].FillEmpty();
                }
            }
        }
    }

    public void OnCreate()
    {
        _craftSystem.CreateDecor();
    }

    private void UpdateCount()
    {
        _сount.text = _craftSystem.Count.ToString();
    }

    private void StartFill(Drawing drawing)
    {
        _mainDrawingSign.FillSign();
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
    }

    public void CloseCraftMenu()
    {
        _menu.SetActive(false);
    }

    public void OnChangeCount(int count)
    {
        _craftSystem.OnChangeCount(count);
        UpdateCount();
    }

    private void StateChanged(IExitableState state)
    {
        if (!(state is CraftState))
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
    }
}
