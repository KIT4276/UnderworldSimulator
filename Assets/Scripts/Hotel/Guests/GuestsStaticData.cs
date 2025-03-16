using System;
using UnityEngine;

[CreateAssetMenu(fileName = "GuestsData", menuName = "ScriptableObjects/GuestsData", order = 5)]
public class GuestsStaticData : ScriptableObject
{
    [SerializeField] private GuestStaticData[] _guests;

    public GuestStaticData[] Guests { get => _guests; }
}

[Serializable]
public class GuestStaticData
{
    [SerializeField] private GuestsType _type;
    [SerializeField] private string _name;
    [SerializeField] private Sprite _icon;

    public GuestsType Type { get => _type; }
    public Sprite Icon { get => _icon; }
    public string Name { get => _name; }

    public string PrefabLink()
    {
        switch (_type)
        {
            case GuestsType.Wolf:
                return AssetPath.WolfPath;
            case GuestsType.Buffalo:
                return AssetPath.BuffaloPath;
            case GuestsType.Monkey:
                return AssetPath.MonkeyPath;
            case GuestsType.Hare:
                return AssetPath.HarePath;
            case GuestsType.Bear:
                return AssetPath.BearPath;
            default:
                return null;
        }
    }
}
