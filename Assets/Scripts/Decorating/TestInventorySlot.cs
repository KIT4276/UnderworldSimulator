using UnityEngine;

[RequireComponent(typeof(InventorySlot))]
public class TestInventorySlot : MonoBehaviour
{
    [SerializeField] private InventorySlot _slot;
    [SerializeField] private InventorySlotCounter _slotCounter;
    [SerializeField] private int _count;
    [SerializeField] private Decor _decorPrefab;

    private bool _isInited;


    private void Awake()
    {
        _slot.OnInitialized += InitSlot;
    }

    private void InitSlot()
    {
        if (_isInited) return;
        for (int i = 0; i < _count; i++)
        {
            _slot.SetDecor(_decorPrefab);
            _isInited = true;
        }
    }

    private void OnDisable()
    {
        _slot.OnInitialized -= InitSlot;
    }
}
