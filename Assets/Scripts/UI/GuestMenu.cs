using System;
using System.Linq;
using UnityEngine;
using Zenject;

public class GuestMenu : MonoBehaviour
{
    [SerializeField] private RoomMenu _roomMenu;
    [SerializeField] private GuestCard[] _guestCard;

    [Inject] private GuestsSystem _guestsSystem;
    [Inject] private StateMachine _stateMachine;
    [Inject] private RoomsSystem _roomsSystem;
    [Inject] private GuestsHandler _guestsHandler;

    private void Start()
    {

        _guestsSystem.GuestsChanged += OnGuestsChanged;
        _stateMachine.ChangeStateAction += OnChangeState;
    }

    private void OnChangeState(IExitableState state)
    {
        //if (state is GameLoopState)
        //{
        //    this.gameObject.SetActive(false);
        //}
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
        Debug.Log(_guestsSystem.Guests.Count);
        Debug.Log(_guestsHandler.AvailableList.Count);

        var guests = _guestsSystem.Guests;
        var selectedRoom = _roomsSystem.SelectedRoom;

        // ”бираем гостей, которые уже наход€тс€ в выбранной комнате
        var filteredGuests = guests
            .Where(guest => guest.Room != selectedRoom)
            .ToList();

        // ‘ормируем список по доступности
        var sortedGuests = filteredGuests
            .OrderByDescending(guest => _guestsHandler.AvailableList.Contains(guest)) // сначала доступные
            .ToList();

        // ќпредел€ем количество отображаемых гостей
        int guestCount = sortedGuests.Count;
        int cardCount = _guestCard.Length;

        // ≈сли гостей меньше, чем карточек Ч последн€€ пуста€ карточка
        bool showEvictCard = guestCount < cardCount;

        int i = 0;

        // «аполн€ем карточки гост€ми
        for (; i < Math.Min(guestCount, cardCount); i++)
        {
            var guest = sortedGuests[i];

            if (_guestsHandler.AvailableList.Contains(guest))
                _guestCard[i].FillCard(guest);
            else
                _guestCard[i].FillCardUnAvailable();
        }

        // ≈сли надо отрисовать пустую карточку Ч делаем это
        if (showEvictCard && i < cardCount)
        {
            _guestCard[i].FillCardEvict();
            i++;
        }

        // ќставшиес€ карточки (если есть) очищаем
        for (; i < cardCount; i++)
        {
            _guestCard[i].FillCardEvict();
        }

        //var guests = _guestsSystem.Guests;
        //var selectedRoom = _roomsSystem.SelectedRoom;

        //// ”бираем гостей, которые уже наход€тс€ в выбранной комнате
        //var filteredGuests = guests
        //    .Where(guest => guest.Room != selectedRoom)
        //    .ToList();

        //// √ости, доступные по наличию в AvailableList
        //var availableGuests = filteredGuests
        //    .Where(guest => _guestsHandler.AvailableList.Contains(guest))
        //    .ToList();

        //// ќстальные считаютс€ недоступными
        //var unavailableGuests = filteredGuests
        //    .Where(guest => !_guestsHandler.AvailableList.Contains(guest))
        //    .ToList();

        //// —начала доступные, затем недоступные
        //var sortedGuests = availableGuests.Concat(unavailableGuests).ToList();

        //// «аполнение карточек гостей
        //for (int i = 0; i < _guestCard.Length; i++)
        //{
        //    if (i < sortedGuests.Count)
        //    {
        //        _guestCard[i].FillCard(sortedGuests[i]);
        //    }
        //    else
        //    {
        //        _guestCard[i].FillCardEvict();
        //    }
        //}

        //var guests = _guestsSystem.Guests;
        //var selectedRoom = _roomsSystem.SelectedRoom;

        //var filteredGuests = guests
        //    .Where(guest => guest.Room != selectedRoom)
        //    .ToList();

        //var availableGuests = filteredGuests
        //    .Where(guest => guest.IsAvailable)
        //    .ToList();

        //var unavailableGuests = filteredGuests
        //    .Where(guest => !guest.IsAvailable)
        //    .ToList();

        //var sortedGuests = availableGuests.Concat(unavailableGuests).ToList();

        //for (int i = 0; i < _guestCard.Length; i++)
        //{
        //    if (i < sortedGuests.Count)
        //    {
        //        _guestCard[i].FillCard(sortedGuests[i]);
        //    }
        //    else
        //    {
        //        _guestCard[i].FillCardEvict();
        //    }
        //}
    }

    public void Back()
    {
       // _roomMenu.gameObject.SetActive(true);
        //this.gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        _guestsSystem.GuestsChanged -= OnGuestsChanged;
        _stateMachine.ChangeStateAction -= OnChangeState;
    }
}
