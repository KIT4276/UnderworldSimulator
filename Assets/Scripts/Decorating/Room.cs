using System;
using System.Collections.Generic;

public class Room
{
    private ClickHandler _clickHandler;
    private SetOfRoomParameters _startSetOfParameters;

    public List<Decor> InstalledDecor { get; private set; }
    public string Name { get; private set; }
    public SetOfRoomParameters SetOfParameters { get; private set; }

    public event Action<Room> ChangeParameter;

    public Room(string name, SetOfRoomParameters setOfParameters, ClickHandler clickHandler)
    {
        InstalledDecor = new();

        Name = name;
        _startSetOfParameters = setOfParameters;
        SetOfParameters = new();
        _clickHandler = clickHandler;
        UpdateParameters();
        _clickHandler.ClickAction += ShowParameters;
    }

    public void AddDecor(Decor decor)
    {
        InstalledDecor.Add(decor);
        UpdateParameters();

        ShowParameters();
    }

    public void DeleteDecor(Decor decor)
    {
        InstalledDecor.Remove(decor);
        UpdateParameters();

        ShowParameters();
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

    private void ShowParameters()
    {
        ChangeParameter?.Invoke(this);
    }
}
