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
    private ProgressSystem _progressSystem;
    private TasksHandler _tasksHandler;
    private object _task;

    [Inject]
    private void Construct(GuestsSystem guestsSystem, ProgressSystem progressSystem, TasksHandler tasksHandler, RoomsSystem roomsSystem)
    {
        _guestsSystem = guestsSystem;
        _progressSystem = progressSystem;
        _tasksHandler = tasksHandler;

        tasksHandler.UpdateTask += OnTaskUpdate;
    }

    private void OnTaskUpdate(Task task, Room room)
    {
       if(task != _task) return;
        
        float currentValue = 0;
        foreach(var param in room.SetOfParameters.Parameters)
        {
            if(param.ParameterType == task.ParameterType)
            {
                currentValue = param.Value;
            }
        }
         var v = currentValue / task.Value;
        _bar.fillAmount = v;
    }

    public void FillEmpty()
    {
        _task = null;


        Debug.Log("FillEmpty");
        _image.gameObject.SetActive(false);

        _paramName.text = string.Empty;
        _paramValue.text = string.Empty;
    }

    public void FillSlot(Task task)
    {
        _task = task;


        Debug.Log("FillSlot");
        _image.gameObject.SetActive(true);
        _image.sprite = _guestsSystem.FindGuestByType(task.GuestsType).Icon; 

        _paramName.text = RoomParameterNames.Names[task.ParameterType];
        _paramValue.text = task.Value.ToString();

       
    }

    
}