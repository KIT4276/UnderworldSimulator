using System;
using System.Collections.Generic;
using UnityEngine;

public class Room
{
    private ClickHandler _clickHandler;
    private SetOfRoomParameters _startSetOfParameters;

    public List<Decor> InstalledDecor { get; private set; }
    public string Name { get; private set; }
    public SetOfRoomParameters SetOfParameters { get; private set; }
    public Guest Guest { get; private set; }
    public Sprite Icon { get; private set; }
    public int ID { get; private set; }

    public event Action<Room> ChangeParameter;
    public event Action<Room> RoomSelected;
    public event Action CheckIn;

    public Room(string name, SetOfRoomParameters setOfParameters, ClickHandler clickHandler, Sprite icon, int id)
    {
        InstalledDecor = new();

        Name = name;
        _startSetOfParameters = setOfParameters;
        SetOfParameters = new();
        _clickHandler = clickHandler;
        UpdateParameters();
        _clickHandler.ClickAction += OnRoomSelected;
        Icon = icon;
        ID = id;
        //ShowParameters;
    }

    public void CheckInTheRoom(Guest guest)
    {
        Guest = guest;
        //Debug.Log(Name + " is CheckedIn");
        ChangeParameter?.Invoke(this);
        CheckIn?.Invoke();
    }

    public void VacateTheRoom()
    {
        Guest = null;
        // Debug.Log(Name + " is Vacate");
        ChangeParameter?.Invoke(this);
    }

    private void OnRoomSelected()
    {
        RoomSelected?.Invoke(this);
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

        foreach (var param in _startSetOfParameters.Parameters)
        {
            SetOfParameters.IncreaseParameterByType(param);
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
        ChangeParameter?.Invoke(this);
    }
}
