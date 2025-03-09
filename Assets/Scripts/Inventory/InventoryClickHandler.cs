using UnityEngine;
using Zenject;

public class InventoryClickHandler : MonoBehaviour
{
    [SerializeField] private InventorySlot _slot;

    [Inject] private readonly DecorationSystem _decorationSystem;
    [Inject] private StateMachine _stateMachine;

    public void OnButtonClick()
    {
        if (_slot.GetLastItems() is Decor)
        {
            if (_slot.IsOccupied && _stateMachine.ActiveState is DecorationState)
                _decorationSystem.SpawnDecorIfCan((Decor)_slot.TakeLastItem());
        }
        else if (_slot.GetLastItems() is Item)
        {
            Debug.Log($"{_slot.GetLastItems()} Тут происходят какие-то действия с лутом ");
        }
        else
        {
            Debug.Log($"{_slot.GetLastItems()} Ни лут, ни декор!");
        }
    }
}
