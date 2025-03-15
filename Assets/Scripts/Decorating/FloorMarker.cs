using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ClickHandler))]
public class FloorMarker : MonoBehaviour
{
    [SerializeField] private string _name;
    [SerializeField] private PolygonCollider2D _collider;
    [SerializeField] private ClickHandler _clickHandler;
    [SerializeField] private SetOfRoomParameters _setOfParameters;

    public PolygonCollider2D Collider { get => _collider; }

    public Room Room { get; private set; }

    public void Init()
    {
        Room = new(_name, _setOfParameters, _clickHandler);
    }

    public void AddDecor(Decor decor)
    {
        Room.AddDecor(decor);
    }

    public void DeleteDecor(Decor decor)
    {
        Room.DeleteDecor(decor);
    }

}

[Serializable]
public class SetOfRoomParameters
{
    private static int _count = Enum.GetValues(typeof(RoomParameterType)).Length;

    [SerializeField] private RoomParameter[] _parameters;// = new RoomParameter[count];

    public RoomParameter[] Parameters { get => _parameters; }

    public SetOfRoomParameters()
    {
        _parameters = new RoomParameter[_count];

        for (int i = 0; i < _count; i++)
        {
            _parameters[i] = new RoomParameter((RoomParameterType)i, 0);
        }
    }

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

    public RoomParameter(RoomParameterType type, int value)
    {
        _type = type;
        _value = value;
    }

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
