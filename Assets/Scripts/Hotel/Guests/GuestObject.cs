using System;
using UnityEngine;
using Zenject;

public class GuestObject : MonoBehaviour
{
    private StateMachine _stateMachine;

    public Guest Guest {  get; private set; }



    public void Init(Guest guest, StateMachine stateMachine)
    {
        Guest = guest;
        _stateMachine = stateMachine;

        Guest.CheckIn += OnCheckIn;
        Guest.Evict += OnEvict;
        _stateMachine.ChangeStateAction += OnChangeState;
    }

    private void OnChangeState(IExitableState state)
    {
        Debug.Log(state);
        
        if(state is DecorationState)
        {
            gameObject.SetActive(false);
        }
       else
        {
            gameObject.SetActive(true);

        }
    }

    private void OnEvict()
    {
        transform.position = transform.parent.position;
    }

    private void OnCheckIn(Room room)
    {
        transform.position = room.GuestsPoint.position;
    }

    private void OnDestroy()
    {
        Guest.MakeUnavailable();
        //_stateMachine.ChangeStateAction -= OnChangeState;
    }
}
