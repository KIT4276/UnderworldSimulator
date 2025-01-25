using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PolygonCollider2D))]
public class PolygonSplitter : MonoBehaviour
{
    [SerializeField] private PolygonCollider2D _polygonCollider;
    //[SerializeField] private float cellSize = 0.5f;
    [SerializeField] private GameObject _squarePrefab;

    private PersistantStaticData _persistantStaticData;
    private IAssets _assets;

    public List<GridCell> PotentiallyOccupiedCells{ get; private set; }

    public void Initialize(IAssets assets, PersistantStaticData persistantStaticData)
    {
        PotentiallyOccupiedCells = new List<GridCell>();

        _assets = assets;
        _persistantStaticData = persistantStaticData;

        SplitPolygonIntoSquares();
    }

    private void SplitPolygonIntoSquares()
    {
        if (_polygonCollider == null || _assets == null)
        {
            Debug.LogError("PolygonCollider2D or _assets is not assigned.");
            return;
        }

        Bounds bounds = _polygonCollider.bounds;
        float startX = bounds.min.x;
        float startY = bounds.min.y;
        float endX = bounds.max.x;
        float endY = bounds.max.y;

        List<Vector2> polygonPoints = new List<Vector2>(_polygonCollider.points);
        for (float x = startX; x < endX; x += _persistantStaticData.CellSize)
        {
            for (float y = startY; y < endY; y += _persistantStaticData.CellSize)
            {
                Vector3 center = new Vector3(x + _persistantStaticData.CellSize / 2, y + _persistantStaticData.CellSize / 2, 0);
                if (IsPointInsidePolygon(center, polygonPoints))
                {
                    PotentiallyOccupiedCells.Add(new DecorsCell(center.x, center.y, true, _assets, this.gameObject));
                    //var cell = Instantiate(_squarePrefab, center, Quaternion.identity);
                    //cell.transform.parent = this.transform;
                    //cell.transform.position = center;
                }
            }
        }
    }

    private bool IsPointInsidePolygon(Vector2 point, List<Vector2> polygon)
    {
        int intersections = 0;
        for (int i = 0; i < polygon.Count; i++)
        {
            Vector2 v1 = polygon[i];
            Vector2 v2 = polygon[(i + 1) % polygon.Count];
            if ((v1.y > point.y) != (v2.y > point.y) &&
                point.x < (v2.x - v1.x) * (point.y - v1.y) / (v2.y - v1.y) + v1.x)
            {
                intersections++;
            }
        }
        return (intersections % 2) == 1;
    }
}
