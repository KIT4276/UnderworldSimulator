using System;
using UnityEngine;

public class Guest
{
    public GuestsType GuestsType { get; private set; }
    public string Name { get; private set; }
    public bool IsAvailable { get; private set; }
    public Sprite Icon { get; private set; }
    public string PrefabLink { get; private set; }
    public Room Room { get; private set; }

    public Guest(GuestStaticData guestData)
    {
        PrefabLink = guestData.PrefabLink();
        GuestsType = guestData.Type;
        Name = guestData.Name;
        Icon = guestData.Icon;

        MakeAvailable();  // for tests
    }

    public void MakeAvailable()
    {
        IsAvailable = true;
    }

    public void CheckInGuest(Room selectedRoom)
    {
        if (Room != null)
            EvictGuest();
        Room = selectedRoom;
        Debug.Log("CheckIn " + Name + " " + "to " + Room.Name);
        Room.CheckInTheRoom(this);
        //TODO effects
    }

    public void EvictGuest()
    {
        Debug.Log("VacateTheRoom " + Name + " " + "to " + Room.Name);
        Room.VacateTheRoom();
        Room = null;
        //TODO effects
    }
}