using UnityEngine;
using Zenject;

[RequireComponent(typeof(InventorySlot))]
public class DecorSpawner : MonoBehaviour
{
    [SerializeField] private InventorySlot _slot;
    [SerializeReference] private InventorySlotCounter _counter;

    [Inject] private readonly DecorationSystem _decorationSystem;

    public void SpawnDecor()
    {
        if (_slot.IsOccupied)
        {
            _decorationSystem.SpawnDecorIfCan(_slot.TakeLastDecor());
        }
    }
}
