using UnityEngine;
using Zenject;

[RequireComponent(typeof(CraftLoot))]
public class StartMaterials : MonoBehaviour
{
    [SerializeField] private CraftLoot _craftLoot;
    [SerializeField] private InventorySystem _inventorySystem;

    private bool _isInit;
    private StateMachine _stateMachine;

    [Inject]
    private void Construct(StateMachine stateMachine)
    {
        _stateMachine = stateMachine;
        stateMachine.ChangeStateAction += OnChangeState;
    }

    private void OnChangeState(IExitableState state)
    {
        if(state is GameLoopState)
        {
            if (!_isInit)
            {
                foreach (var loot in _craftLoot.LootSettings)
                {
                    for (var i = 0; i < loot.Count;i++)
                    {
                        var item = loot.Loot;
                        item.Init(_inventorySystem.MaterialsData);
                        _inventorySystem.TryReturnLootToInventory(item);
                    }
                }

                _isInit = true;
            }
        }
    }

    private void OnDestroy()
    {
        _stateMachine.ChangeStateAction -= OnChangeState;
        _isInit = false;
    }
}
