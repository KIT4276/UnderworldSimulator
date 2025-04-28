using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerProgress
{
    public State PlayerState;
    public WorldData WorldData;
    public Stats PlayerStats;
    public List<DecorData> DecorsData;
    public List<Guest> Guests;
    public List<Item> InventoryItems;
    public List<Decor> InventoryDecors;

    public PlayerProgress(string initialLevel, GuestsStaticData guestsStaticData)
    {
        WorldData = new WorldData(initialLevel);
        PlayerState = new State();
        PlayerStats = new Stats();
        DecorsData = new List<DecorData>();//here do the same as below
        Guests = NewGuests(guestsStaticData);
        InventoryItems = new();
        InventoryDecors = new();
    }

    private List<Guest> NewGuests(GuestsStaticData guestsStaticData)
    {
        List<Guest> guests = new();

        foreach(var guest in guestsStaticData.Guests)
        {
            //Guest guest = new(data);
            guests.Add(guest);
        }
        return guests;
    }
}
