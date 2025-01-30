using UnityEngine;

public class GridCell : BaceCell
{
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

   
}
