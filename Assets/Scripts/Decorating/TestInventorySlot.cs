using UnityEngine;

[RequireComponent(typeof(InventorySlot))]
public class TestInventorySlot : MonoBehaviour
{
    [SerializeField] private InventorySlot _slot;
    [SerializeField] private InventorySlotCounter _slotCounter;
    [SerializeField] private int _count;
    [SerializeField] private Decor _decorPrefab;


    private void Awake()
    {
        _slot.OnInitialized += InitSlot;
    }

    private void InitSlot()
    {
        _slot.SetDecor(_decorPrefab);
        _slotCounter.AddCount(_count);
    }

    private void OnDisable()
    {
        _slot.OnInitialized -= InitSlot;
    }
}
