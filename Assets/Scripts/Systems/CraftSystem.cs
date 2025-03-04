using System;
using Zenject;

public class CraftSystem
{
    public int Count { get; private set; }

    private Drawing _activeDrawing;
    private InventorySystem _inventorySystem;
    private DrawingData _drawingDatas;

    public DrawingData DrawingDatas { get => _drawingDatas; }
    public Drawing ActiveDrawing { get => _activeDrawing; }

    [Inject] private DecorFactory _decoratorFactory;

    public event Action ChangeCount;

    public CraftSystem(InventorySystem inventorySystem, DrawingData drawingDatas)
    {
        Count = 1;
        _inventorySystem = inventorySystem;
        _drawingDatas = drawingDatas;

        _activeDrawing = _drawingDatas.Drawings[0];
    }

    public void SelectDrawing(Drawing drawing)
    {
        _activeDrawing = drawing;
        Count = 1;
        ChangeCount?.Invoke();
    }

    public void OnChangeCount(int count)
    {
        Count += count;
        if (Count < 0)
            Count = 0;

        ChangeCount?.Invoke();
    }

    public void CreateDecor()
    {
       //todo check vfterials!
        
        for (int i = 0; i < Count; i++)
        {
            var decor = _decoratorFactory.SpawnDecor(_activeDrawing.Decor);

            _inventorySystem.TryReturnDecorToInventory(decor);
            //todo dectees materials in inventory!
        }
    }
}
