using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PolygonCollider2D))]
public abstract class BasePolygonSplitter : MonoBehaviour
{
    [SerializeField] protected PolygonCollider2D _polygonCollider;

    protected PersistantStaticData _persistantStaticData;
    protected IAssets _assets;

    protected List<GridCell> _ñells;

    public void Initialize(IAssets assets, PersistantStaticData persistantStaticData)
    {
        _ñells = new List<GridCell>();

        _assets = assets;
        _persistantStaticData = persistantStaticData;

        SplitPolygonIntoSquares();
    }

    protected void SplitPolygonIntoSquares()
    {
        if (_polygonCollider == null || _assets == null)
        {
            Debug.LogError("PolygonCollider2D or Assets is not assigned.");
            return;
        }

        Bounds bounds = _polygonCollider.bounds;
        float startX = bounds.min.x;
        float startY = bounds.min.y;
        float endX = bounds.max.x;
        float endY = bounds.max.y;

        List<Vector2> polygonPoints = new List<Vector2>(_polygonCollider.points);

        Enumeration(startX, startY, endX, endY, polygonPoints);
    }

    protected abstract void Enumeration(float startX, float startY, float endX, float endY, List<Vector2> polygonPoints);

    protected abstract void AddCells(Vector3 center);

    protected bool IsPointInsidePolygon(Vector2 point, List<Vector2> polygon)
    {
        int intersections = 0;
        int count = polygon.Count;

        for (int i = 0; i < count; i++)
        {
            Vector2 v1 = transform.TransformPoint(polygon[i]);
            Vector2 v2 = transform.TransformPoint(polygon[(i + 1) % count]);

            if ((v1.y > point.y) != (v2.y > point.y) &&
                point.x < (v2.x - v1.x) * (point.y - v1.y) / (v2.y - v1.y) + v1.x)
            {
                intersections++;
            }
        }
        return (intersections % 2) == 1;
    }
}
