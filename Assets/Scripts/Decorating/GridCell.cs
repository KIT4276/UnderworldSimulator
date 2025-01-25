using UnityEngine;

public class GridCell
{
    public float CenterX { get; protected set; }
    public float CenterY { get; protected set; }
    public bool IsOccupied { get; protected set; }
    public GameObject QuadObject { get; protected set; }
    public SpriteRenderer SpriteRenderer { get; protected set; }

    protected Color _startColor;


    public GridCell(float x, float y, bool isOccupied, IAssets assets)
    {
        this.CenterX = x;
        this.CenterY = y;
        this.IsOccupied = isOccupied;
        this.QuadObject = assets.Instantiate(AssetPath.CellPath);
        QuadObject.transform.position = new Vector3(x, y, 0);
        SpriteRenderer = QuadObject.GetComponent<SpriteRenderer>();

        _startColor = SpriteRenderer.color;
    }

    public void OccupyCell()
    {
        IsOccupied = true;
        SpriteRenderer.color = Color.red;
    }

    public void EmptyCell()
    {
        IsOccupied = false;
        SpriteRenderer.color = _startColor;
    }
}
