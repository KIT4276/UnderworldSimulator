using System;
using Unity.VisualScripting;
using UnityEngine;
using Zenject;

[Serializable]
public class Guest : BaseHandledReward
{
    [SerializeField] private GuestsType _type;
    [SerializeField] private string _name;
    [SerializeField] private Sprite _icon;
    [SerializeField] private Sprite _milestonesIcon;
    [SerializeField] private Sprite _roomsChooseIcon;

    public GuestsType Type { get => _type; }
    public Sprite Icon { get => _icon; }
    public Sprite MilestonesIcon { get => _milestonesIcon; }
    public Sprite RoomsChooseIcon { get => _roomsChooseIcon; }
    public override string Name { get => _name; }
    public Room Room { get; private set; }

    public event Action<Room> CheckIn;
    public event Action Evict;

    private GuestsStaticData _guestsStaticData;

   

    public string PrefabLink()// remove, make it a simple string
    {
        switch (_type)
        {
            case GuestsType.Wolf:
                return AssetPath.WolfPath;
            case GuestsType.Bull:
                return AssetPath.BullPath;
            case GuestsType.Monkey:
                return AssetPath.MonkeyPath;
            case GuestsType.Rabbit:
                return AssetPath.RabbitPath;
            case GuestsType.Bear:
                return AssetPath.BearPath;
            default:
                return null;
        }
    }

    public void CheckInGuest(Room selectedRoom)
    {
        if (Room != null)
            EvictGuest();
        Room = selectedRoom;
        //Debug.Log("CheckIn " + _name + " " + "to " + Room.Name);
        Room.CheckInTheRoom(this);
        //TODO effects

        CheckIn?.Invoke(Room);
    }

    public void EvictGuest()
    {
        // Debug.Log("VacateTheRoom " + _name + " " + "to " + Room.Name);
        Room.VacateTheRoom();
        Room = null;
        //TODO effects
        Evict?.Invoke();
    }
}