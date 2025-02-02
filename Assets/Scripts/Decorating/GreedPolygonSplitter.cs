using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class GreedPolygonSplitter : BasePolygonSplitter
{
    private DecorationState _decorationState;

    public List<BaceCell> Cells { get => _cells; }

    [Inject] 
    public void Construct(DecorationState decorationState)
    {
        _decorationState = decorationState;
    }

    protected override void Enumeration(float startX, float startY, float endX, float endY, List<Vector2> polygonPoints)
    {
        for (float x = startX; x < endX; x += _persistantStaticData.CellSize)
        {
            for (float y = startY; y < endY; y += _persistantStaticData.CellSize)
            {
                Vector2 center = new Vector2(x + _persistantStaticData.CellSize / 2, y + _persistantStaticData.CellSize / 2);
                if (IsPointInsidePolygon(center, polygonPoints))
                {
                    AddCells(center);
                }
            }
        }
    }


    private void AddCells(Vector3 center)
    {
        _cells.Add(new GridCell(center.x, center.y, false, _assets, _decorationState));
    }
}
