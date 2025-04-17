using System;
using UnityEngine;

[Serializable]
public class Item : BaseItem
{
    [SerializeField] protected LootType _type;

    private MaterialsData _materialsData;

    public LootType LootType { get => _type; }


    public void Init(MaterialsData materialsData)
    {
        _materialsData = materialsData;
    }

    public string GetHint() => _materialsData.GetMaterialsHint(_type);

    public Sprite GetIcon() => _materialsData.GetMaterialsIcon(_type);

}
