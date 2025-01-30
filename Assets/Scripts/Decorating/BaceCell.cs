using UnityEngine;

public abstract class BaceCell
{
    public float CenterX { get; protected set; }
    public float CenterY { get; protected set; }
    public bool IsOccupied { get; protected set; }
    public GameObject QuadObject { get; protected set; }
    public SpriteRenderer SpriteRenderer { get; protected set; }

    protected Color _startColor;

    public void OccupyCell()
    {
        IsOccupied = true;
        SpriteRenderer.color = new Color( Color.red.r, Color.red.g, Color.red.b, _startColor.a);
    }

    public void EmptyCell()
    {
        IsOccupied = false;
        SpriteRenderer.color = _startColor;
    }
}
