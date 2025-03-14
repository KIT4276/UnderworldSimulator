using System;
using System.Collections.Generic;
using UnityEngine;

public class FloorMarker : MonoBehaviour
{
    [SerializeField] private string _name;
    [SerializeField] private PolygonCollider2D _collider;
    [SerializeField] private ClickHandler _clickHandler;
    [SerializeField] private SetOfRoomParameters _setOfParameters;

    public PolygonCollider2D Collider { get => _collider; }
    public List<Decor> InstalledDecor { get; private set; }
    public string Name { get => _name; }
    public SetOfRoomParameters SetOfParameters { get => _setOfParameters; }

    public event Action<FloorMarker> ChangeParameter;

    private void Start()
    {
        InstalledDecor = new();
        _clickHandler.ClickAction += ShowParameters;
    }

    public void AddDecor(Decor decor)
    {

        InstalledDecor.Add(decor);
        _setOfParameters.IncreaseParameters(decor.Parameters);
        ShowParameters();
    }

    public void DeleteDecor(Decor decor)
    {
        InstalledDecor.Remove(decor);
        _setOfParameters.DecreaseParameters(decor.Parameters);
        ShowParameters();
    }

    private void ShowParameters()
    {
        ChangeParameter?.Invoke(this);
    }
}

[Serializable]
public class SetOfRoomParameters
{
    [SerializeField] private RoomParameter[] _parameters;

    public RoomParameter[] Parameters { get => _parameters; }

    public void IncreaseParameters(SetOfRoomParameters set)
    {
        for (int i = 0; i < set.Parameters.Length; i++)
        {
            if (set.Parameters[i].ParameterType == _parameters[i].ParameterType)
            {
                _parameters[i].IncreaseParametersValue(set.Parameters[i].Value);
                break;
            }
        }
    }

    public void DecreaseParameters(SetOfRoomParameters set)
    {
        for (int i = 0; i < set.Parameters.Length; i++)
        {
            if (set.Parameters[i].ParameterType == _parameters[i].ParameterType)
            {
                _parameters[i].DecreaseParametersValue(set.Parameters[i].Value);
                break;
            }
        }
    }
}

[Serializable]
public class RoomParameter
{
    [SerializeField] private string _name;
    [SerializeField] private RoomParameterType _type;
    [SerializeField] private int _value;

    public string Name { get => _name; }
    public RoomParameterType ParameterType { get => _type; }
    public int Value { get => _value; }

    public void IncreaseParametersValue(int value)
    {
        _value += value;
    }

    public void DecreaseParametersValue(int value)
    {
        _value -= value;
    }
}

public enum RoomParameterType
{
    Leisure,
    Aesthetics,
    Comfort,
}
