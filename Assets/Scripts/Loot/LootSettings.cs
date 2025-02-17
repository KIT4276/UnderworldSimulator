using System;
using UnityEngine;

[Serializable]
public class LootSettings /*: MonoBehaviour*/
{
    [SerializeField] private Item _loot;
    [SerializeField] private int _count;


    public Item Loot { get => _loot; }
    public int Count { get => _count; }
    

    //[SerializeField] private Loot _loot;
    //[SerializeField] private int _count;
    //[Inject] private LootSystem _lootSystem;

    //private bool _isActive;

    //private void Start()
    //{
    //    _lootSystem.OpenMenuAction += FillSlots;
    //}

    //public void SetIsActive(bool isActive)
    //{
    //    _isActive = isActive;
    //}

    //private void FillSlots()
    //{
    //   // Debug.Log("FillSlots");
    //    _lootSystem.FillSlot(_loot, _count, this.gameObject);
    //}

    //private void OnDestroy()
    //{
    //    _lootSystem.OpenMenuAction -= FillSlots;
    //}
}
