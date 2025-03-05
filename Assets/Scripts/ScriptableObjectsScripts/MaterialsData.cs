using System;
using UnityEngine;

[CreateAssetMenu(fileName = "MaterialsData", menuName = "ScriptableObjects/MaterialsData", order = 4)]
public class MaterialsData : ScriptableObject
{
    [SerializeField] private Sprite _defaultIcon;
    [Space]
    [SerializeField] private LootMaterial[] _materials;

    public Sprite GetMaterialsIcon(LootType material)
    {
        foreach (var mat in _materials)
        {
            if (mat.LootType == material)
                return mat.Icon;
        }
        return _defaultIcon;
    }

    public string GetMaterialsHint(LootType material)
    {
        foreach (var mat in _materials)
        {
            if (mat.LootType == material)
                return mat.Hint;
        }
        return "No Hints";
    }
}
