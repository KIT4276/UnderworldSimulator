using UnityEngine;
using Zenject;


public class LootSettings : MonoBehaviour
{
    [SerializeField] private Loot _loot;
    [SerializeField] private int _count;
    [Inject] private LootSystem _lootSystem;

    private void Start()
    {
        _lootSystem.OpenMenuAction += FillSlots;
    }

    private void FillSlots()
    {
        _lootSystem.FillSlot(_loot, _count, this.gameObject);
    }

    private void OnDestroy()
    {
        _lootSystem.OpenMenuAction -= FillSlots;
    }
}
