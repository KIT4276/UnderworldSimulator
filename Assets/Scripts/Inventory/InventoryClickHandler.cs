using UnityEngine;
using Zenject;

public class InventoryClickHandler : MonoBehaviour
{
    [SerializeField] private InventorySlot _slot;

    [Inject] private readonly DecorationSystem _decorationSystem;

    public void OnButtonClick()
    {
        if (_slot.GetLastItems() is Decor)
        {
            if (_slot.IsOccupied)
                _decorationSystem.SpawnDecorIfCan((Decor)_slot.TakeLastItem());
        }
        else if (_slot.GetLastItems() is Loot)
        {
            Debug.Log($"{_slot.GetLastItems()} Тут происходят какие-то действия с лутом ");
        }
        else
        {
            Debug.Log($"{_slot.GetLastItems()} Ни лут, ни декор!");
        }
    }
}
