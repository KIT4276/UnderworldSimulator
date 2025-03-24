using System;
using UnityEngine;
using Zenject;

public class GuestMenu : MonoBehaviour
{
    [SerializeField] private RoomRating _roomRating;
    [SerializeField] private GuestCard[] _guestCard;

    [Inject] private GuestsSystem _guestsSystem;
    [Inject] private RoomsSystem _roomsSystem;

    private void Start()
    {
        _guestsSystem.GuestsChanged += OnGuestsChanged;
    }

    public void Open()
    {
        FillCards();
    }

    private void OnGuestsChanged()
    {
        FillCards();
    }

    private void FillCards() 
    {
        int i = 0;
        for (; i < _guestsSystem.Guests.Count; i++)
        {
            if (_guestsSystem.Guests[i].IsAvailable)
            {
                _guestCard[i].FillCard(_guestsSystem.Guests[i]);
               // _guestCard[i].PushCheckIn += OnPushedCheckIn;
            }
            else
                _guestCard[i].FillCardEmpty();
        }

        if (_guestCard.Length > _guestsSystem.Guests.Count)
        {
            for (; i < _guestCard.Length; i++)
            {
                _guestCard[i].FillCardEmpty();
            }
        }
    }

    //private void OnPushedCheckIn(Guest guest)
    //{
    //    guest.CheckInGuest(_roomsSystem.SelectedRoom);
    //}

    public void Back()
    {
        _roomRating.gameObject.SetActive(true);
        this.gameObject.SetActive(false);
    }
}
