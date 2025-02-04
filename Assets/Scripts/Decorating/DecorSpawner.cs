using UnityEngine;
using Zenject;

[RequireComponent(typeof(InventorySlot))]
public class DecorSpawner : MonoBehaviour
{
    [SerializeField] private InventorySlot _slot;

    [Inject] private DecorationSystem _decorationSystem;

    public void SpawnDecor()
    {
       // Debug.Log(_slot.CurrentDecor);
        
        if (_slot.IsOccupied)
            _decorationSystem.InstantiateDecor(_slot.CurrentDecor);
    }
}
