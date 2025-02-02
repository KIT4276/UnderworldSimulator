using UnityEngine;

public class GridCell : BaceCell
{
    private SpriteRenderer _quadView;

    public GridCell(float x, float y, bool isOccupied, IAssets assets, DecorationState decorationState)
    {
        this.CenterX = x;
        this.CenterY = y;
        this.IsOccupied = isOccupied;
        this.QuadObject = assets.Instantiate(AssetPath.CellPath);
        QuadObject.transform.position = new Vector3(x, y, 0);
        SpriteRenderer = QuadObject.GetComponent<SpriteRenderer>();
        _quadView = QuadObject.GetComponent<SpriteRenderer>();
        _startColor = SpriteRenderer.color;

        decorationState.DecorationStateEnter += EnterDecorationState;
        decorationState.DecorationStateExit += ExitDecorationState;

        ExitDecorationState();
    }

    private void ExitDecorationState()
    {
        _quadView.enabled = false;
    }

    private void EnterDecorationState()
    {
        _quadView.enabled = true;
    }
}
