using UnityEngine;

public class FloorMarker : MonoBehaviour
{
    [SerializeField] private PolygonCollider2D _collider;

    public PolygonCollider2D Collider {  get => _collider;}
}
