using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class ParameterSlot : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private TMP_Text _paramName;
    [SerializeField] private TMP_Text _paramValue;
    [Space]
    [SerializeField] private Image _bar;

    private GuestsSystem _guestsSystem;
    //private ProgressSystem _progressSystem;
    private TasksHandler _tasksHandler;
    private RoomsSystem _roomsSystem;
    private Task _task;

    [Inject]
    private void Construct(GuestsSystem guestsSystem, /*ProgressSystem progressSystem,*/ TasksHandler tasksHandler,
        RoomsSystem roomsSystem)
    {
        _guestsSystem = guestsSystem;
        //_progressSystem = progressSystem;
        _tasksHandler = tasksHandler;
        _roomsSystem = roomsSystem;

        tasksHandler.UpdateTask += OnUpdate;
        guestsSystem.GuestsChanged += UpdateSlot;
        roomsSystem.RoomsParamsChanged += OnRoomsParamsChanged;
        roomsSystem.RoomSelected += OnRoomsParamsChanged;
    }

    private void OnRoomsParamsChanged(Room room)
    {
        UpdateSlot();
    }

    private void OnUpdate(Task task, Room room)
    {
        UpdateSlot();
    }

    private void UpdateTask(Task task, Room room)
    {

        if (task != _task || room.Guest == null) return;

        if (task is ParameterTask)
        {
            float currentValue = 0;
            foreach (var param in room.SetOfParameters.Parameters)
            {
                if (param.ParameterType == ((ParameterTask)task).ParameterType)
                {
                    currentValue = param.Value;
                }
            }
            var v = currentValue / ((ParameterTask)task).Value;
            _bar.fillAmount = v;
        }
        else if (task is SpecificTask)
        {

            //TODO
            if (task.GuestsType == room.Guest.Type)
            {
                foreach (var decor in room.InstalledDecor)
                {
                    if (((SpecificTask)task).DecorType != decor.DecorType)
                    {
                        _bar.fillAmount = 0;
                        //Debug.Log("UnDone");
                    }
                }

                foreach (var decor in room.InstalledDecor)
                {
                    if (((SpecificTask)task).DecorType == decor.DecorType)
                    {
                        _bar.fillAmount = 1;
                        //Debug.Log("Done");
                    }
                }
            }
        }
    }

    public void FillSlot(Task task)
    {
        _task = task;
        // Debug.Log("0");
        _image.gameObject.SetActive(true);
        _image.sprite = _guestsSystem.FindGuestByType(task.GuestsType).Icon;

        if (task is ParameterTask)
        {
            //Debug.Log("1");
            _paramName.text = RoomParameterNames.Names[((ParameterTask)task).ParameterType];
            _paramValue.text = ((ParameterTask)task).Value.ToString();
        }
        else
        {
            //Debug.Log("2");
            _paramName.text = string.Empty;
            _paramValue.text = ((SpecificTask)task).DecorType.ToString();
            //TODO
        }

        if (!CheckIfGuestHasRoom(task))
        {
            _bar.fillAmount = 0;
        }

        UpdateSlot();
    }

    public void UpdateSlot()
    {
        foreach (Room room in _roomsSystem.Rooms)
        {
            if (_task != null && room.Guest != null && room.Guest != null)
            {
                if (room.Guest.Type == _task.GuestsType)
                {
                    UpdateTask(_task, room);
                }
            }
        }
    }

    private bool CheckIfGuestHasRoom(Task task)
    {
        bool checkedIn = false;

        foreach (var room in _roomsSystem.Rooms)
        {
            if (room.Guest != null && task.GuestsType == room.Guest.Type)
            {
                checkedIn = true;
            }
        }
        return checkedIn;
    }

    public void FillEmpty()
    {
        //Debug.Log("FillEmpty");
        _task = null;
        _image.gameObject.SetActive(false);

        _paramName.text = string.Empty;
        _paramValue.text = string.Empty;
        _bar.fillAmount = 0;
    }

    private void OnDestroy()
    {
        _tasksHandler.UpdateTask -= OnUpdate;
        _guestsSystem.GuestsChanged -= UpdateSlot;
        _roomsSystem.RoomsParamsChanged -= OnRoomsParamsChanged;
        _roomsSystem.RoomSelected -= OnRoomsParamsChanged;
    }
}