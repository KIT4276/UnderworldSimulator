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
        UpdateParameters();
        ShowParameters();
        Debug.Log(InstalledDecor.Count);
    }

    public void DeleteDecor(Decor decor)
    {
        InstalledDecor.Remove(decor);
        UpdateParameters();
        ShowParameters();
    }

    private void UpdateParameters()
    {
        foreach(var param in _setOfParameters.Parameters)
        {
            param.Clear();
        }

        foreach (Decor decor in InstalledDecor)
        {
            foreach(var decorParam in decor.Parameters.Parameters)
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

[Serializable]
public class SetOfRoomParameters
{
    [SerializeField] private RoomParameter[] _parameters;

    public RoomParameter[] Parameters { get => _parameters; }


    public void IncreaseParameterByType(RoomParameter param)
    {
        foreach (RoomParameter foundParam in _parameters)
        {
            if (foundParam.ParameterType == param.ParameterType)
            {
                foundParam.IncreaseParametersValue(param.Value);
            }
        }
    }
}

[Serializable]
public class RoomParameter
{
    [SerializeField] private RoomParameterType _type;
    [SerializeField] private int _value;

    public RoomParameterType ParameterType { get => _type; }
    public int Value { get => _value; }

    public void IncreaseParametersValue(int value) => 
        _value += value;

    public void Clear() => 
        _value = 0;
}

public enum RoomParameterType
{
    Leisure,
    Aesthetics,
    Comfort,
}

public static class RoomParameterNames
{
    public static Dictionary<RoomParameterType, string> Names = new()
    {
        { RoomParameterType.Leisure, "Досуг"},
        { RoomParameterType.Aesthetics, "Эстетика"},
        { RoomParameterType.Comfort, "Комфорт"}
    };
}
