using System;
using UnityEngine;
using Zenject;

public class GuestMenu : MonoBehaviour
{
    [SerializeField] private RoomMenu _roomRating;
    [SerializeField] private GuestCard[] _guestCard;

    [Inject] private GuestsSystem _guestsSystem;
    [Inject] private StateMachine _stateMachine;

    private void Start()
    {
        _guestsSystem.GuestsChanged += OnGuestsChanged;
        _stateMachine.ChangeStateAction += OnChangeState;
    }

    private void OnChangeState(IExitableState state)
    {
        if (state is GameLoopState)
        {
            this.gameObject.SetActive(false);
        }
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

    public void Back()
    {
        _roomRating.gameObject.SetActive(true);
        this.gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        _guestsSystem.GuestsChanged -= OnGuestsChanged;
        _stateMachine.ChangeStateAction -= OnChangeState;
    }
}
