using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(ClickHandler))]
public class FloorMarker : MonoBehaviour
{
    [SerializeField] private int _id;
    [SerializeField] private string _name;
    [SerializeField] private Sprite _icon;
    [SerializeField] private PolygonCollider2D _collider;
    [SerializeField] private ClickHandler _clickHandler;
    [SerializeField] private Transform _guestsPoint;
    //[SerializeField] private ParameterData _parameterData;
    // [SerializeField] private SetOfRoomParameters _setOfParameters;

    private RoomsSystem _roomsSystem;

    public PolygonCollider2D Collider { get => _collider; }

    public Room Room { get; private set; }

    public int ID { get => _id; }

    [Inject]
    private void Construct(RoomsSystem roomsSystem)
    {
        _roomsSystem = roomsSystem;
    }

    public void Init()
    {
        Room = new(_name, /*_setOfParameters, */_clickHandler, _icon, ID, _roomsSystem, _guestsPoint);
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

    [SerializeField] private RoomParameter[] _parameters;
    [SerializeField] private ParameterData _parameterData;

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

    public float GetParamValueByType(RoomParameterType parameterType)
    {
        float result = 0;
        foreach (RoomParameter foundParam in _parameters)
        {
            if (foundParam.ParameterType == parameterType)
                return foundParam.Value;
        }
        return result;
    }
}

[Serializable]
public class RoomParameter
{
    //[SerializeField] private string _name;
    [SerializeField] private RoomParameterType _type;
    [SerializeField] private int _value;

    public RoomParameterType ParameterType { get => _type; }
    public int Value { get => _value; }

    //public string Name { get; private set; }

    //public Sprite Icon { get; private set; }

    //public ParameterData _parameterData;

    //[Inject]
    //private void Construct(ParameterData parameterData)
    //{
    //    //Debug.Log("PseudoConstruct");
    //    _parameterData = parameterData;
    //    Parameter param = parameterData.FindParamByType(ParameterType);

    //    Name = param.Name;
    //    Icon = param.Icon;

    //}

    public RoomParameter(RoomParameterType type, int value/*, ParameterData parameterData*/)
    {
        //Debug.Log("Construct");
        _type = type;
        _value = value;

        //_parameterData = parameterData;
        //Parameter param = parameterData.FindParamByType(ParameterType);

        //Name = param.Name;
        //Icon = param.Icon;

        //_name = RoomParameterNames.Names[_type];
    }

    public void IncreaseParametersValue(int value) =>
        _value += value;

    public void Clear() =>
        _value = 0;
}

public enum RoomParameterType
{
    Comfort,
    Leisure,
    Aesthetics,
}

//public static class RoomParameterNames
//{
//    public static Dictionary<RoomParameterType, string> Names = new()
//    {
//        { RoomParameterType.Leisure, "Досуг"},
//        { RoomParameterType.Aesthetics, "Эстетика"},
//        { RoomParameterType.Comfort, "Комфорт"}
//    };
//}
