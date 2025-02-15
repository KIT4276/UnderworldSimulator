using UnityEngine;
using Zenject;

public class DecorSpawner : MonoBehaviour
{
    [SerializeField] private InventorySlot _slot;

    [Inject] private readonly DecorationSystem _decorationSystem;

    public void SpawnDecor()
    {
        if (_slot.IsOccupied)
        {
            if (_slot.GetLastInventoryObject() is Decor)
                _decorationSystem.SpawnDecorIfCan((Decor)_slot.TakeLastDecor());
            else
            {
                Debug.Log($"{_slot.GetLastInventoryObject()} Это не декор, а, скорее всего, лут ");
            }
        }
    }
}
