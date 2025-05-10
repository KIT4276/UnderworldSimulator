using System;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class GuestCard : MonoBehaviour
{
    [SerializeField] private Image _icon;
    [Space]
    [SerializeField] private RoomMenu _roomMenu;
    [SerializeField] private GuestMenu _guestMenu;
    [Space]
    [SerializeField] private Sprite _lockSprite;
    [SerializeField] private Sprite _evictSprite;

    [Inject] private RoomsSystem _roomsSystem;

    private Guest _guest;

    private bool _isEvict;

    public void FillCard(Guest guest)
    {
        _guest = guest;
        _icon.sprite = guest.CheckInIconSmall;
        var color = _icon.color;
        color.a = 1;
        _isEvict = false;
    }

    public void CheckInGuest()
    {
        if (_guest != null)
        {
            _roomsSystem.TryToCheckInGuest(_guest);
        }
        else if (_isEvict)
        {
            _guest = null;
            _roomsSystem.EvictGuest();
        }
        else { return; }

        AudioReciever.Instance.PlayUIRoomClick();
        _guestMenu.Back();
    }

    public void FillCardUnAvailable()
    {
        _icon.sprite = _lockSprite;
        var color = _icon.color;
        color.a = 1;
        _isEvict = false;

    }

    public void FillCardEvict()
    {
        _icon.sprite = _evictSprite;
        var color = _icon.color;
        color.a = 1;
        _isEvict = true;
    }
}
