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
   // [SerializeField] private Sprite _icon;
    [SerializeField] private Sprite _iconForRoomMenu;
    [Space]
    [SerializeField] private Sprite _decorParamIcon;
    [SerializeField] private Sprite _roomMenuIcons;
    [SerializeField] private Sprite _iconForTasks;
    [SerializeField] private Sprite _iconForCraft;
    [SerializeField] private Sprite _iconForDrawings;

    public string Name { get => _name; } 
    public RoomParameterType ParameterType { get => _parameterType; } 
   // public Sprite Icon { get => _icon; } 
    public Sprite IconForRoomMenu { get => _iconForRoomMenu; } 


    public Sprite DecorParamIcon { get => _decorParamIcon; } 
    public Sprite RoomMenuIcons { get => _roomMenuIcons; }
    public Sprite IconForTasks { get => _iconForTasks; } 
    public Sprite IconForCraft { get => _iconForCraft; } 
    public Sprite IconForDrawings { get => _iconForDrawings; } 
}
