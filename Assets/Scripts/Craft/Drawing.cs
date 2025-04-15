using System;
using UnityEngine;

[Serializable]
public class Drawing : BaseHandledReward
{
    [SerializeField] protected string _name;
    [SerializeField] protected string _description;
    [SerializeField] protected Sprite _draft;
    [SerializeField] private Decor _decor;
    [SerializeField] private DrawingComponent[] _drawingComponents;

    public DrawingComponent[] DrawingComponents { get => _drawingComponents; }
    public Sprite Icon { get => _decor.GetIcon(); }
    public Decor Decor { get => _decor; }

    public override string Name { get => _name; }
    public string Description { get => _description; }
    public Sprite Draft { get => _draft; }

}
