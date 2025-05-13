using System;
using System.Linq;
using UnityEngine;
using Zenject;

public class GuestMenu : MonoBehaviour
{
    [SerializeField] private RoomMenu _roomMenu;
    [SerializeField] private GameObject _allCards;
    [SerializeField] private GuestCard[] _guestCard;
    [SerializeField] private GameObject _notifier;

    [Inject] private GuestsSystem _guestsSystem;
    [Inject] private RoomsSystem _roomsSystem;
    [Inject] private GuestsHandler _guestsHandler;


    private void Start()
    {
        foreach (var guest in _guestsHandler.All)
        {
            ((Guest)guest).CheckIn += OnCheckIn;
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
        UpdateNotifier();
    }

    private void OnCheckIn(Room room)
    {
        FillCards();
        UpdateNotifier();
    }

    public void Open()
    {
        if (_allCards.activeInHierarchy)
        {
            Back();
        }
        else
        {

            _allCards.SetActive(true);
            FillCards();
        }
        UpdateNotifier();
    }

    private void OnGuestsChanged()
    {
        UpdateNotifier();
        FillCards();
    }

    private void UpdateNotifier()
    {
        if (_roomsSystem.SelectedRoom != null)
        {
            if (_roomsSystem.SelectedRoom.Guest == null && IsExistsUnCheckedInGuest())
            {
                _notifier.SetActive(true);

            }
            else
            {
                _notifier.SetActive(false);
            }
        }
    }

    private bool IsExistsUnCheckedInGuest()
    {
        bool isExist = false;
        if (_guestsHandler.AvailableList != null && _guestsHandler.AvailableList.Count > 0)
        {
            foreach (var reward in _guestsHandler.AvailableList)
            {
                if (reward is Guest guest && guest.Room == null)
                {
                    isExist = true;
                    break;
                }
            }
        }
        return isExist;
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
