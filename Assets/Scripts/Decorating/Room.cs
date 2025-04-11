using System;
using System.Collections.Generic;
using UnityEngine;

public class Room
{
    private ClickHandler _clickHandler;
    private readonly RoomsSystem _roomsSystem;

    public string Name { get; private set; }


    public Sprite Icon { get; private set; }
    public int ID { get; private set; }

    public List<Decor> InstalledDecor { get; private set; }
    public SetOfRoomParameters SetOfParameters { get; private set; }
    public Guest Guest { get; private set; }
    public Transform GuestsPoint { get; private set; }


    public event Action CheckIn;
    public event Action Evicted;

    public Room(string name, ClickHandler clickHandler, 
        Sprite icon, int id, RoomsSystem roomsSystem, Transform guestsPoint)
    {
        InstalledDecor = new();

        Name = name;
        GuestsPoint = guestsPoint;

        SetOfParameters = new();
        _clickHandler = clickHandler;
        UpdateParameters();
        _clickHandler.ClickAction += OnRoomSelected;
        Icon = icon;
        ID = id;
        _roomsSystem = roomsSystem;
    }

    public void CheckInTheRoom(Guest guest)
    {
        Guest = guest;
        _roomsSystem.OnRoomsParamsChanged(this);
        CheckIn?.Invoke();
    }

    public void VacateTheRoom()
    {
        Guest = null;

        _roomsSystem.OnRoomsParamsChanged(this);
        Evicted?.Invoke();
    }

    private void OnRoomSelected()
    {
        _roomsSystem.OnRoomSelected(this);
    }

    public void AddDecor(Decor decor)
    {
        InstalledDecor.Add(decor);
        UpdateParameters();

        OnChangeParameter();
    }

    public void DeleteDecor(Decor decor)
    {
        InstalledDecor.Remove(decor);
        UpdateParameters();

        OnChangeParameter();
    }

    private void UpdateParameters()
    {
        foreach (var param in SetOfParameters.Parameters)
        {
            param.Clear();
        }

        foreach (Decor decor in InstalledDecor)
        {
            foreach (var decorParam in decor.Parameters.Parameters)
            {
                SetOfParameters.IncreaseParameterByType(decorParam);
            }
        }
    }

    private void OnChangeParameter()
    {
        _roomsSystem.OnRoomsParamsChanged(this);
    }
}
