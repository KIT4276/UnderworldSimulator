using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

public class LootClickHandler : MonoBehaviour
{
    [SerializeField] private LootSlot _lootSlot;
   

    [Inject] private LootSystem _lootSystem;

    public void OntakeClick()
    {
        _lootSystem.TakeLootToInventory(_lootSlot.TakeLastItem());
    }

    public void OnTakeAllClick()
    {
        while (_lootSlot.Loots.Count > 0)
        {
            _lootSystem.TakeLootToInventory(_lootSlot.TakeLastItem());
        }
        _lootSystem.OffInteractiveObject();
        _lootSystem.CloseMenu();
    }
}
