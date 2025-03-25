using System;
using UnityEngine;

[Serializable]
public class Drawing : BaseHandledReward
{
    [SerializeField] private Decor _decor;
    [SerializeField] private DrawingComponent[] _drawingComponents;

    public DrawingComponent[] DrawingComponents { get => _drawingComponents; }
    public Sprite Icon { get => _decor.GetIcon(); }
    public Decor Decor { get => _decor; }
}
