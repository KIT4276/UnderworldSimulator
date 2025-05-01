using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class ParameterSlot : MonoBehaviour
{
   // [SerializeField] private SpriteRenderer _taskSlot;
    [SerializeField] private Image _icon;
    [SerializeField] private GameObject _star;
    [SerializeField] private GameObject _progressBar;
    [SerializeField] private Image _bar;
    [Space]
    [SerializeField] private TMP_Text _name;
    [SerializeField] private TMP_Text _description;
    [SerializeField] private TMP_Text _score;
    [SerializeField] private TMP_Text _reward;

    private GuestsSystem _guestsSystem;
    private TasksHandler _tasksHandler;
    private RoomsSystem _roomsSystem;
   // private ParameterData _parameterData;
    private DrawingData _drawingData;
    private Task _task;

    [Inject]
    private void Construct(GuestsSystem guestsSystem, TasksHandler tasksHandler,
        RoomsSystem roomsSystem,/* ParameterData parameterData,*/ DrawingData drawingData)
    {
        _guestsSystem = guestsSystem;
        _tasksHandler = tasksHandler;
        _roomsSystem = roomsSystem;
        //_parameterData = parameterData;
        _drawingData = drawingData;

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

        if (task is ParameterTask parameterTask)
        {
            float currentValue = 0;
            foreach (var param in room.SetOfParameters.Parameters)
            {
                if (param.ParameterType == parameterTask.ParameterType)
                {
                    currentValue = param.Value;
                }
            }
            _score.text = currentValue + "/" + parameterTask.Value;
            var v = currentValue / parameterTask.Value;
            _bar.fillAmount = v;
        }
        else if (task is SpecificTask specificTask)
        {

            //TODO
            if (task.GuestsType == room.Guest.Type)
            {
                foreach (var decor in room.InstalledDecor)
                {
                    if (specificTask.DecorType != decor.DecorType)
                    {
                        _bar.fillAmount = 0;
                        //Debug.Log("UnDone");
                    }
                }

                foreach (var decor in room.InstalledDecor)
                {
                    if (specificTask.DecorType == decor.DecorType)
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
        _progressBar.SetActive(true);
        _bar.gameObject.SetActive(true);
        _star.gameObject.SetActive(true);
        _icon.gameObject.SetActive(true);


        if (task is ParameterTask parameterTask)
        {
            _icon.sprite = parameterTask.Parameter.IconForTasks;
            _name.text = parameterTask.Name;
            _description.text = parameterTask.Description;
            _reward.text = "+" + parameterTask.XP;
            //Debug.Log("1");
        }
        else if (task is SpecificTask specificTask)
        {
            foreach(var dr in _drawingData.Drawings)
            {
                if (dr.Decor.DecorType == specificTask.DecorType)
                {
                    _icon.sprite = dr.Icon;
                    break;
                }
            }
            
            _name.text = specificTask.Name;
            _description.text = specificTask.Description;
        }
        //else
        //{
        //    //Debug.Log("2");
        //    _paramName.text = string.Empty;
        //    _paramValue.text = ((SpecificTask)task).DecorType.ToString();
        //    //TODO
        //}

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

        _progressBar.SetActive(false);
        _bar.gameObject.SetActive(false);
        _star.gameObject.SetActive(false);
        _icon.gameObject.SetActive(false);

        _name.text = string.Empty;
        _description.text = string.Empty;

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