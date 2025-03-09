using UnityEngine;

public class QuestsLoot : MonoBehaviour, Loot
{
    [SerializeField] private QuestsLootSettings[] _questsLootSettings;
    
    public LootSettings[] LootSettings => _questsLootSettings;
}
