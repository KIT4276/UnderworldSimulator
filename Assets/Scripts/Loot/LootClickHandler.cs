using UnityEngine;
using Zenject;

public class LootClickHandler : MonoBehaviour
{
    [SerializeField] private LootSlot _lootSlot;
    
    [Inject] private LootSystem _lootSystem;

    public void OntakeClick()
    {
        Debug.Log("OntakeClick");
        _lootSystem.TakeLootToInventory(_lootSlot.TakeLastItem());
    }

    public void OnTakeAllClick()
    {
        Debug.Log("OnTakeAllClick");
        foreach (var loot in _lootSlot.Loots)
        {
            _lootSystem.TakeLootToInventory(_lootSlot.TakeLastItem());
        }
    }
}
