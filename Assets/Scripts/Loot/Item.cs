using UnityEngine;

public class Item : BaseItem
{
    [SerializeField] protected LootType _type;

    public LootType LootType{ get => _type;}
}
