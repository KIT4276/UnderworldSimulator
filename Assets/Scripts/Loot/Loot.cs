using UnityEngine;

public class Loot : BaseItem
{
    [SerializeField] private LootType _type;
    
    public LootType LootType{ get => _type;}
}

public enum LootType
{
    Wood,

    Copper,
    Iron,
    Bronze,
    Aluminum,

    BlackGranite,
    RedGranite,
    Marble,

    Percale,
    Cobweb,

    Glass,

    PineResin,

    GlowingFlowersOrAnimals,

    Bones,
    Teeth,
}
