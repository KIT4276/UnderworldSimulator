using System;
using UnityEngine;

[Serializable]
public class Drawing
{
    [SerializeField] private Decor _decor;
    [SerializeField] private string _name;
    [SerializeField] private DrawingComponent[] _drawingComponents;
   // [SerializeField] private Sprite _icon;

    public DrawingComponent[] DrawingComponents { get => _drawingComponents; }
    public string Name { get => _name; }
    public Sprite Icon { get => _decor.GetIcon(); }
    public Decor Decor { get => _decor; }
}
