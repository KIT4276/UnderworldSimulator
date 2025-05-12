using System;
using UnityEngine;

public class GuestObject : MonoBehaviour
{
    [SerializeField] private GameObject[] _body;
    [SerializeField] private GameObject _footprints;
    [SerializeField] private NPCPeriodicAnimations _periodicAnimations;

    private StateMachine _stateMachine;

    public Guest Guest { get; private set; }

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
        if (state is DecorationState)
        {
            foreach(var obj in _body)
            {
                obj.SetActive(false);
            }
            _footprints.SetActive(true);

            //gameObject.SetActive(false);
        }
        else
        {
            _footprints.SetActive(false);
            foreach (var obj in _body)
            {
                obj.SetActive(true);
            }
                _periodicAnimations.StartPeriodicMove();
            // gameObject.SetActive(true);
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
    }
}
