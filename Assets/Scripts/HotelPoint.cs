using UnityEngine;

public class HotelPoint : MonoBehaviour
{
    [SerializeField] private Collider2D _frame;

    public Collider2D Frame { get => _frame; }
}
