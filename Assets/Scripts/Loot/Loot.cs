using UnityEngine;

public class Loot  : MonoBehaviour
{
    [SerializeField] private LootSettings[] _lootSettings;

    public LootSettings[] LootSettings { get => _lootSettings; }
}
