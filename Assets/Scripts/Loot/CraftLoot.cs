using UnityEngine;

public class CraftLoot : MonoBehaviour, Loot
{
    [SerializeField] private CraftLootSettings[] _craftLootSettings;

    public LootSettings[] LootSettings => _craftLootSettings;

    private void Start()
    {
        foreach (var settings in _craftLootSettings)
        {
            settings.Init();
        }
    }

    public void TakeItem(LootType type, int count)
    {
        foreach (var craftLoot in _craftLootSettings)
        {
            if (craftLoot.Loot.LootType == type && craftLoot.CurrentCount >= count)
            {
                craftLoot.DecreaseCount(count);
            }
        }
    }

    public void Restart()
    {
        foreach (var craftLoot in _craftLootSettings)
        {
            craftLoot.RestartCount();
        }
    }
}
