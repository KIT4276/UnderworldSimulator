using System;
using UnityEngine;
using Zenject;

public class GuestMenu : MonoBehaviour
{
    [SerializeField] private RoomRating _roomRating;
    [SerializeField] private GuestCard[] _guestCard;

    [Inject] private GuestsSystem _guestsSystem;

    private void Start()
    {
        _guestsSystem.GuestsInstantiated += OnGuestsInstantiated;
    }

    public void Open()
    {
        FillCards();
    }

    private void OnGuestsInstantiated()
    {
        FillCards();
    }

    private void FillCards() 
    {

        int i = 0;
        for (; i < _guestsSystem.Guests.Count; i++)
        {
            if (_guestsSystem.Guests[i].IsOpen)
                _guestCard[i].FillCard(_guestsSystem.Guests[i]);
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
}
