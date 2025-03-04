using System;
using UnityEngine;

[Serializable]
public class LootMaterial
{
    [SerializeField] private LootType _material;
    [SerializeField] private Sprite _icon;

    public LootType LootType { get => _material; }
    public Sprite Icon { get => _icon; }
}

