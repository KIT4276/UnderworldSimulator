using System;
using UnityEngine;
using Zenject;

[Serializable]
public class Item : BaseItem
{
    [SerializeField] protected LootType _type;

    private MaterialsData _materialsData;

    public LootType LootType { get => _type; }

    //[Inject]
    //private void Construct(MaterialsData materialsData)
    //{
    //    Debug.Log(_materialsData);
    //    _materialsData = materialsData;
    //}

    public void Init(MaterialsData materialsData)
    {
        _materialsData = materialsData;
    }

    public string GetHint() => _materialsData.GetMaterialsHint(_type);

    public Sprite GetIcon() => _materialsData.GetMaterialsIcon(_type);

}
