using UnityEngine;
using Zenject;

[RequireComponent(typeof(InventorySlot))]
public class DecorSpawner : MonoBehaviour
{
    [SerializeField] private InventorySlot _slot;

    [Inject] private DecorationSystem _decorationSystem;

    public void SpawnDecor()
    {
        if (_slot.IsOccupied)
            _decorationSystem.SpawnDecorIfCan(_slot.CurrentDecor); //тут прикрутить счётчик, а не на кнопку!
    }
}
