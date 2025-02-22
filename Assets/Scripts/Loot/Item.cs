using UnityEngine;

public class Item : BaseItem
{
    [SerializeField] private LootType _type;

    public LootType LootType{ get => _type;}
}
