using UnityEngine;

public class CraftLoot : MonoBehaviour, Loot
{
    [SerializeField] private CraftLootSettings[] _craftLootSettings;
    
    public LootSettings[] LootSettings => _craftLootSettings;
}
