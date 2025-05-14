using UnityEngine;
using Zenject;

public class InventoryClickHandler : MonoBehaviour
{
    [SerializeField] private InventorySlot _slot;

    [Inject] private DecorationSystem _decorationSystem;
    [Inject] private StateMachine _stateMachine;
    [Inject]  private DecorHolder _decorHolder;

    public void Construct(StateMachine stateMachine, DecorationSystem decorationSystem, DecorHolder decorHolder)
    {
        _decorationSystem = decorationSystem;
        _stateMachine = stateMachine;
        _decorHolder = decorHolder;
    }

    public void OnButtonClick()
    {
        if (_slot.GetLastItems() is Decor)
        {
            //Debug.Log(_slot);
            //Debug.Log(_slot.IsOccupied);
            //Debug.Log(_stateMachine);
            //Debug.Log(_stateMachine.ActiveState);

            if (_slot == null || _stateMachine == null || _decorationSystem == null|| _decorHolder == null || _decorHolder.ActiveDecor != null) return;

            if (_slot.IsOccupied && _stateMachine.ActiveState is DecorationState)
                _decorationSystem.SpawnDecorIfCan((Decor)_slot.TakeLastItem());
        }
        else if (_slot.GetLastItems() is Item)
        {
            //Debug.Log($"{_slot.GetLastItems()} ��� ���������� �����-�� �������� � ����� " );
            Debug.Log(_slot.Items.Count);
        }
        else
        {
            Debug.Log($"{_slot.GetLastItems()} �� ���, �� �����!");
        }

        AudioReciever.Instance.PlayUIFurnitureClick();
    }
}
