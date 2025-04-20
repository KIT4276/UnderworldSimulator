using UnityEngine;

public class HotelUnTransparency : MonoBehaviour
{
    [SerializeField] private HotelTransparency _hotelTransparency;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<HeroMove>(out var hero))
        {
            _hotelTransparency.UnTransparent();
        }
    }
}
