using System;
using UnityEngine;

[CreateAssetMenu(fileName = "ParameterData", menuName = "ScriptableObjects/ParameterData", order = 7)]

public class ParameterData : ScriptableObject
{
    [SerializeField] private Parameter[] _parameters;

    public Parameter[] Parameters { get { return _parameters; } }

    public Parameter FindParamByType(RoomParameterType parameterType)
    {
        Parameter param = _parameters[0];

        foreach (var parameter in _parameters)
        {
            if(parameter.ParameterType == parameterType)
                param =  parameter;
        }

        return param;
    }
}

[Serializable]
public class Parameter
{
    [SerializeField] private string _name;
    [SerializeField] private RoomParameterType _parameterType;
    [SerializeField] private Sprite _icon;

    public string Name { get { return _name; } }
    public RoomParameterType ParameterType { get { return _parameterType; } }
    public Sprite Icon { get { return _icon; } }
}
