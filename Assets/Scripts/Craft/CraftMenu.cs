using TMPro;
using UnityEngine;
using Zenject;

public class CraftMenu : MonoBehaviour
{
    [SerializeField] private CraftSlot[] _slots;
    [SerializeField] private TMP_Text _count;
    [SerializeField] private GameObject _menu;

    [Inject] private CraftSystem _craftSystem;
    [Inject] private WorkbenchSystem _workbenchSystem;

    private void Start()
    {
        _workbenchSystem.CraftButtonClick += OpenCraftMenu;
        foreach (var slot in _slots)
        {
            slot.DrawingSelected += ToSelectDrawing;
        }

        CloseCraftMenu();
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
        _count.text = _craftSystem.Count.ToString();
    }

    private void ToSelectDrawing(Drawing drawing)
    {
        _craftSystem.SelectDrawing(drawing);
    }
}
