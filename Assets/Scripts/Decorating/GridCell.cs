using UnityEngine;

public class GridCell
{
    public float CenterX { get; private set; }
    public float CenterY { get; private set; }
    public bool IsOccupied { get; private set; }
    public GameObject QuadObject { get; private set; }
    public SpriteRenderer SpriteRenderer { get; private set; }


    public GridCell(float x, float y, bool isOccupied, IAssets assets)
    {
        this.CenterX = x;
        this.CenterY = y;
        this.IsOccupied = isOccupied;
        this.QuadObject = assets.Instantiate(AssetPath.CellPath);
        QuadObject.transform.position = new Vector3(x, y, 0);
        SpriteRenderer = QuadObject.GetComponent<SpriteRenderer>();
    }

    public void SetIsOccupied(bool isOccupied)
        => IsOccupied = isOccupied;
}
