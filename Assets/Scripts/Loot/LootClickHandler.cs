using UnityEngine;
using Zenject;

public class LootClickHandler : MonoBehaviour
{
    [SerializeField] private LootSlot _lootSlot;
   

    [Inject] private LootSystem _lootSystem;

    public void OntakeClick()
    {
        //_lootSystem.TakeLootToInventory(_lootSlot.TakeLastItem());

        //if (_lootSlot.Loots.Count <= 0)
        //{
        //   // Debug.Log(_lootSlot.Loots.Count);
        //    _lootSystem.AllIsTacen();
        //}
    }

    public void OnTakeAllClick()
    {
        while (_lootSlot.Loots.Count > 0)
        {
            _lootSystem.TakeLootToInventory(_lootSlot.TakeLastItem());
        }
        _lootSystem.AllIsTacen();
    }
}
