using System.Collections.Generic;
using UnityEngine;
using Zenject;



public class GuestsSpawner
{
    public void SpawnGuests(List<Guest> gests, IAssets assets, StateMachine stateMachine)
    {
        GuestsPoint[] points = GameObject.FindObjectsByType<GuestsPoint>(FindObjectsSortMode.None);

        foreach (var point in points)
        {
            foreach (var guest in gests)
            {
                if (guest.Type == point.Type)
                {
                    GameObject guestGameObject = assets.Instantiate(guest.PrefabLink(), point.transform.position);
                    guestGameObject.transform.parent = point.transform;
                    guestGameObject.GetComponent<GuestObject>().Init(guest, stateMachine);
                }
            }
        }
    }
}
