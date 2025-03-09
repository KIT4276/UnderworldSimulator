using System;
using UnityEngine;

[Serializable]
public class DrawingComponent
{
    [SerializeField] private LootType _material;
    [SerializeField] private int _count;

    public LootType Material {  get => _material;} 
    public int Count {  get => _count; }
}
