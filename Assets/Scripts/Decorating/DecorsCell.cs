using UnityEngine;

public class DecorsCell : GridCell
{
    public DecorsCell(float x, float y, bool isOccupied, IAssets assets, GameObject polygonSplitterObject) : base(x, y, isOccupied, assets)
    {
        Debug.Log("DecorsCell");
        this.CenterX = x;
        this.CenterY = y;
        this.IsOccupied = isOccupied;
        this.QuadObject = assets.Instantiate(AssetPath.CellPath);
        QuadObject.transform.parent = polygonSplitterObject.transform;
        QuadObject.transform.position = new Vector3(x, y, 0);
        QuadObject.name = "DecorsCell";
        SpriteRenderer = QuadObject.GetComponent<SpriteRenderer>();

        OccupyCell();
    }
}
