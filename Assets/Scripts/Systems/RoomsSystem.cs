using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomsSystem
{
    private readonly SpaceDeterminantor _spaceDeterminantor;
    private readonly ICoroutineRunner _coroutineRunner;
    private CameraMove _camera;

    public List<Room> Rooms { get; private set; }
    public Room SelectedRoom { get; private set; }
    public Dictionary<Room, FloorMarker> FloorMarkers { get; private set; }

    public event Action<Room> RoomsParamsChanged;
    public event Action<Room> RoomSelected;
    public event Action Inited;

    public RoomsSystem(SpaceDeterminantor spaceDeterminantor, ICoroutineRunner coroutineRunner)
    {
        _spaceDeterminantor = spaceDeterminantor;
        _coroutineRunner = coroutineRunner;
        Rooms = new();
        FloorMarkers = new();

        _spaceDeterminantor.Find += OnFindFloor;
    }

    private void OnFindFloor()
    {
        Rooms.Clear();
        FloorMarkers.Clear();

        foreach (var floor in _spaceDeterminantor.FloorMarkers)
        {
            Rooms.Add(floor.Room);
            FloorMarkers.Add(floor.Room, floor);


        }

        OnRoomSelected(_spaceDeterminantor.FloorMarkers[0].Room);
        Inited?.Invoke();
    }

    public void OnRoomSelected(Room room)
    {
        SelectedRoom = room;
        RoomSelected?.Invoke(SelectedRoom); 
    }

    public void OnRoomsParamsChanged(Room room)
    {
        RoomsParamsChanged?.Invoke(room);
    }

    public void TryToCheckInGuest(Guest guest)
    {
        EvictGuest();
        guest.CheckInGuest(SelectedRoom);
    }

    public void EvictGuest()
    {
        if (SelectedRoom.Guest != null)
        {
            SelectedRoom.Guest.EvictGuest();
        }

    }

    private IEnumerator FindCamera()
    {
        if (_camera == null)
        {
            if (!(Camera.main.TryGetComponent<CameraMove>(out _camera)))
            {
                _coroutineRunner.StartCoroutine(FindCamera());
            }
        }
        else
        {
            yield return null;
        }
    }
}
