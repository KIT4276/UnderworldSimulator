using System.Collections.Generic;
using UnityEngine;

public class GreedPolygonSplitter : BasePolygonSplitter
{
    public List<BaceCell> Cells { get => _сells; }

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
        _сells.Add(new GridCell(center.x, center.y, false, _assets));
    }
}
