using System;
using UnityEngine;

[Serializable]
public class LootMaterial
{
    [SerializeField] private LootType _material;
    [SerializeField] private Sprite _icon;
    [SerializeField] private string _hints;

    public LootType LootType { get => _material; }
    public Sprite Icon { get => _icon; }
    public string Hint { get => _hints; }   

}

