using UnityEngine;

public class Floor : MonoBehaviour
{
    [SerializeField] private PolygonCollider2D _polygonCollider;
    public PolygonCollider2D Polygon {get => _polygonCollider; }
}
