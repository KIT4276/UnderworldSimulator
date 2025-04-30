using System;
using System.Linq;
using UnityEngine;
using Zenject;

public class GuestMenu : MonoBehaviour
{
    [SerializeField] private RoomMenu _roomMenu;
    [SerializeField] private GameObject _allCards;
    [SerializeField] private GuestCard[] _guestCard;

    [Inject] private GuestsSystem _guestsSystem;
    [Inject] private RoomsSystem _roomsSystem;
    [Inject] private GuestsHandler _guestsHandler;

    private void Start()
    {
        foreach(var guest in _guestsHandler.All)
        {
            ((Guest) guest).CheckIn += OnCheckIn;
            ((Guest)guest).Evict += OnGuestsChanged;
        }
        _guestsHandler.AvailableUpdate += OnGuestsChanged;
        _guestsSystem.GuestsChanged += OnGuestsChanged;
        _roomsSystem.RoomSelected += OnRoomSelected;
        _allCards.SetActive(false);
    }

    private void OnRoomSelected(Room room)
    {
        FillCards();
    }

    private void OnCheckIn(Room room)
    {
        FillCards();
    }

    public void Open()
    {
        _allCards.SetActive(true);
        FillCards();
    }

    private void OnGuestsChanged()
    {
        FillCards();
    }

    private void FillCards()
    {
        var guests = _guestsSystem.Guests;
        var selectedRoom = _roomsSystem.SelectedRoom;

        var filteredGuests = guests
            .Where(guest => guest.Room != selectedRoom)
            .ToList();

        var sortedGuests = filteredGuests
            .OrderByDescending(guest => _guestsHandler.AvailableList.Contains(guest)) // сначала доступные
            .ToList();

        int guestCount = sortedGuests.Count;
        int cardCount = _guestCard.Length;

        bool showEvictCard = guestCount < cardCount;

        int i = 0;

        for (; i < Math.Min(guestCount, cardCount); i++)
        {
            var guest = sortedGuests[i];

            if (_guestsHandler.AvailableList.Contains(guest))
                _guestCard[i].FillCard(guest);
            else
                _guestCard[i].FillCardUnAvailable();
        }

        if (showEvictCard && i < cardCount)
        {
            _guestCard[i].FillCardEvict();
            i++;
        }

        for (; i < cardCount; i++)
        {
            _guestCard[i].FillCardEvict();
        }
    }

    public void Back()
    {
        _allCards.SetActive(false);
    }

    private void OnDestroy()
    {
        foreach (var guest in _guestsHandler.All)
        {
            ((Guest)guest).CheckIn -= OnCheckIn;
            ((Guest)guest).Evict -= OnGuestsChanged;
        }

        _guestsHandler.AvailableUpdate -= OnGuestsChanged;
        _guestsSystem.GuestsChanged -= OnGuestsChanged;
        _roomsSystem.RoomSelected -= OnRoomSelected;
    }
}
