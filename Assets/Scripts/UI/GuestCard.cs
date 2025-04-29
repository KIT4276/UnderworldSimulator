using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class GuestCard : MonoBehaviour
{
    [SerializeField] private Image _icon;
    [Space]
    [SerializeField] private RoomMenu _roomMenu;
    [Space]
    [SerializeField] private Sprite _lockSprite;
    [SerializeField] private Sprite _evictSprite;

    [Inject] private RoomsSystem _roomsSystem;

    private Guest _guest;

    public void FillCard(Guest guest)
    {
       // Debug.Log(guest.Name);
        _guest = guest;
        _icon.sprite = guest.Icon;
    }

    public void CheckInGuest()
    {
        _roomsSystem.TryToCheckInGuest(_guest);

        //_roomMenu.BackToRooms();
    }

    public void EvictGuest()
    {
        _guest.EvictGuest();
        _guest = null;
    }

    public void FillCardUnAvailable()
    {
        Debug.Log("FillCardUnAvailable");
        _icon.sprite = _lockSprite;

    }

    public void FillCardEvict()
    {
        Debug.Log("FillCardEvict");
        _icon.sprite = _evictSprite;
    }
}
