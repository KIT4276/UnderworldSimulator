using UnityEngine;

public class GuestMenu : MonoBehaviour
{
    [SerializeField] private RoomRating _roomRating;

    public void Back()
    {
        _roomRating.gameObject.SetActive(true);
        this.gameObject.SetActive(false);
    }
}
