using System.Collections.Generic;
using UnityEngine;

public class DecorPolygonSplitter : BasePolygonSplitter
{
    public List<BaceCell> PotentiallyOccupiedCells { get => _сells; }

    protected override void Enumeration(float startX, float startY, float endX, float endY, List<Vector2> polygonPoints)
    {
        for (float x = startX; x < endX; x += _persistantStaticData.CellSize)
        {
            for (float y = startY; y < endY; y += _persistantStaticData.CellSize)
            {
                Vector3 center = new Vector3(x + _persistantStaticData.CellSize / 2, y + _persistantStaticData.CellSize / 2, 0);
                if (IsPointInsidePolygon(center, polygonPoints))
                {
                    AddCells(center);
                }
            }
        }

        _polygonCollider.enabled = false;
    }

    private void AddCells(Vector3 center)
    {
        _сells.Add(new DecorsCell(center.x, center.y, true, _assets, this.gameObject));
    }
}
