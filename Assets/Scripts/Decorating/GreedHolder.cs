using System.Collections.Generic;

public class GreedHolder
{
    private readonly GreedPolygonSplitter _floor;
    public List<GridCell> Grid { get => _floor.Cells;  }

    //private PersistantStaticData _persistantStaticData;
    //private IAssets _assets;

    public GreedHolder(GreedPolygonSplitter floor, IAssets assets, PersistantStaticData persistantStaticData)
    {
        //Grid = new List<GridCell>();
        //_assets = assets;
        _floor = floor;
        // _persistantStaticData = persistantStaticData;

        _floor.Initialize(assets, persistantStaticData);
        //SplitPolygonIntoSquares(floor);
    }

    //private void SplitPolygonIntoSquares(Floor floor)
    //{
    //    if (floor.Polygon == null || _assets == null)
    //    {
    //        Debug.Log("PolygonCollider2D or assets is not assigned.");
    //        return;
    //    }

    //    Bounds bounds = floor.Polygon.bounds;
    //    float startX = bounds.min.x;
    //    float startY = bounds.min.y;
    //    float endX = bounds.max.x;
    //    float endY = bounds.max.y;

    //    List<Vector2> polygonPoints = new List<Vector2>(floor.Polygon.points);
    //    for (float x = startX; x < endX; x += _persistantStaticData.CellSize)
    //    {
    //        for (float y = startY; y < endY; y += _persistantStaticData.CellSize)
    //        {
    //            Vector2 center = new Vector2(x + _persistantStaticData.CellSize / 2, y + _persistantStaticData.CellSize / 2);
    //            if (IsPointInsidePolygon(center, polygonPoints))
    //            {
    //                Grid.Add(new GridCell(center.x, center.y, false, _assets));
    //            }
    //        }
    //    }
    //}

    //private bool IsPointInsidePolygon(Vector2 point, List<Vector2> polygon)
    //{
    //    int intersections = 0;
    //    int count = polygon.Count;

    //    for (int i = 0; i < count; i++)
    //    {
    //        Vector2 v1 = _floor.transform.TransformPoint(polygon[i]);
    //        Vector2 v2 = _floor.transform.TransformPoint(polygon[(i + 1) % count]);

    //        if ((v1.y > point.y) != (v2.y > point.y) &&
    //            point.x < (v2.x - v1.x) * (point.y - v1.y) / (v2.y - v1.y) + v1.x)
    //        {
    //            intersections++;
    //        }
    //    }
    //    return (intersections % 2) == 1;
    //}
}
