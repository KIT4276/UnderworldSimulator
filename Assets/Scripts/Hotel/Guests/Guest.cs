using System;
using UnityEngine;

public class Guest : BaseHandledReward
{
    public GuestsType GuestsType { get; private set; }
    public bool IsAvailable { get; private set; }
    public Sprite Icon { get; private set; }
    public string PrefabLink { get; private set; }
    public Room Room { get; private set; }

    public override string Name { get => _name; }

    private string _name;

    public Guest(GuestStaticData guestData)
    {
        PrefabLink = guestData.PrefabLink();
        GuestsType = guestData.Type;
        _name = guestData.Name;
        Icon = guestData.Icon;

        MakeAvailable();  // for tests
    }

    //public void MakeAvailable()
    //{
    //    IsAvailable = true;
    //}

    public void CheckInGuest(Room selectedRoom)
    {
        if (Room != null)
            EvictGuest();
        Room = selectedRoom;
        Debug.Log("CheckIn " + _name + " " + "to " + Room.Name);
        Room.CheckInTheRoom(this);
        //TODO effects
    }

    public void EvictGuest()
    {
        Debug.Log("VacateTheRoom " + _name + " " + "to " + Room.Name);
        Room.VacateTheRoom();
        Room = null;
        //TODO effects
    }
}