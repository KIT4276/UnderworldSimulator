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
            floor.Room.ChangeParameter += OnRoomsParamsChanged;
            floor.Room.RoomSelected += OnRoomsParamsChanged;

            Rooms.Add(floor.Room);
            FloorMarkers.Add(floor.Room, floor);
        }
        OnRoomSelected(_spaceDeterminantor.FloorMarkers[0].Room);
    }

    private void OnRoomSelected(Room room)
    {
        SelectedRoom = room;
        RoomSelected?.Invoke(SelectedRoom);
    }

    private void OnRoomsParamsChanged(Room room)
    {
        RoomsParamsChanged?.Invoke(room);
    }

    public void SwitchUpRoom()
    {
        int i = Rooms.IndexOf(SelectedRoom);
        i++;
        if (i >= Rooms.Count)
        {
            i = 0;
        }

        OnRoomSelected(Rooms[i]);
        var positionOfFloor = FloorMarkers[SelectedRoom].transform.position;

        if (_camera == null)
        {
            _coroutineRunner.StartCoroutine(FindCamera());
        }
        _camera.MoveTo(positionOfFloor.x, positionOfFloor.y);
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
