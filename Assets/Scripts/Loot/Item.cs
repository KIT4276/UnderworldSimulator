using System;
using UnityEngine;

[Serializable]
public class Item : IBaseItem
{
    [SerializeField] protected LootType _type;

    [SerializeField, HideInInspector]  private MaterialsData _materialsData;

    public LootType LootType { get => _type; }

    protected int _id;

    public void Init(MaterialsData materialsData)
    {
        _materialsData = materialsData;
    }

    public string GetHint() => _materialsData.GetMaterialsHint(_type);

    public Sprite GetIcon() => _materialsData.GetMaterialsIcon(_type);
}
