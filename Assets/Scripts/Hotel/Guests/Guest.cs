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
    }
}