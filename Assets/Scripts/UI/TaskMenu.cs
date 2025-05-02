using System;
using UnityEngine;
using Zenject;

public class TaskMenu : MonoBehaviour
{
    [SerializeField] private ParameterSlot[] _parametersSlots;

    private TasksHandler _tasksHandler;
    private bool _isInit;
    private StateMachine _stateMachine;
    private RoomsSystem _roomsSystem;

    [Inject]
    private void Construct(TasksHandler tasksHandler, StateMachine stateMachine, RoomsSystem roomsSystem)
    {
        _tasksHandler = tasksHandler;
        _stateMachine = stateMachine;
        _roomsSystem = roomsSystem;
    }

    private void Start()
    {
        _stateMachine.ChangeStateAction += OnChangeState;
        _tasksHandler.AvailableUpdate += OnAvailableUpdate;
        _roomsSystem.RoomSelected += OnRoomChanged;
        _roomsSystem.RoomsParamsChanged += OnRoomChanged;

        foreach (var task in _tasksHandler.All)
        {
            task.BecameAvailable += OnBecameAvailable;
        }
    }

    private void Awake()
    {
        Fill();
    }

    private void OnRoomChanged(Room room)
    {
        Fill();
    }


    private void OnBecameAvailable(BaseHandledReward reward)
    {
        Fill();
    }

    private void OnAvailableUpdate()
    {
        Fill();
    }

    private void OnChangeState(IExitableState state)
    {
        if (!_isInit && state is GameLoopState)
        {
            Fill();
            _isInit = true;
        }
    }

    private void Fill()
    {
        foreach (var slot in _parametersSlots)
        {
            slot.FillEmpty();
        }

        int i = 0;

        foreach (var reward in _tasksHandler.AvailableList)
        {
            if (reward is Task task)
            {
                if (_roomsSystem.SelectedRoom.Guest != null &&
                task.GuestsType == _roomsSystem.SelectedRoom.Guest.Type)
                {
                    _parametersSlots[i].FillSlot(task);
                    i++;
                }
            }
        }
    }

    private void OnDestroy()
    {
        _stateMachine.ChangeStateAction -= OnChangeState;
        _tasksHandler.AvailableUpdate -= OnAvailableUpdate;
        _roomsSystem.RoomSelected -= OnRoomChanged;
        _tasksHandler.OnDestroy();
    }
}
