using System;
using UnityEngine;
using Zenject;

public class WallsSystem
{
    private GameObject[] _smallWalls;
    private GameObject[] _bigWalls;
    private GameObject[] _roof;

    private StateMachine _stateMachine;

    public HotelViewState CurrentlViewState { get; private set; }

    public event Action WallsStateChange;

    [Inject]
    private void Construct(StateMachine stateMachine)
    {
        _stateMachine = stateMachine;
        _stateMachine.ChangeStateAction += OnChangeState;
    }


    public void InitWithWalls(GameObject[] smallWalls, GameObject[] bigWalls, GameObject[] roof)
    {
        _smallWalls = new GameObject[smallWalls.Length];
        _bigWalls = new GameObject[bigWalls.Length];
        _roof = new GameObject[roof.Length];

        _smallWalls = smallWalls;
        _bigWalls = bigWalls;
        _roof = roof;

        //Debug.Log("InitWithWalls");
        SwitchToRoof();
    }

    private void OnChangeState(IExitableState state)
    {
        if (state is WorkbenchState && _stateMachine.PredioslyState is GameLoopState)
        {
            CurrentlViewState = HotelViewState.Floor;
            UpdateObjects();
            WallsStateChange?.Invoke();
        }
        if (state is GameLoopState && _stateMachine.PredioslyState is WorkbenchState)
        {
            CurrentlViewState = HotelViewState.Roof;
            UpdateObjects();
            WallsStateChange?.Invoke();
        }
    }

    public void SwitchUpViewState()
    {
        CurrentlViewState = (HotelViewState)(((int)CurrentlViewState + 1) % 3);

        UpdateObjects();
        WallsStateChange?.Invoke();
    }

    public void SwitchDownViewState()
    {
        CurrentlViewState = (HotelViewState)(((int)CurrentlViewState - 1 + 3) % 3);
        UpdateObjects();
        WallsStateChange?.Invoke();
    }

    private void UpdateObjects()
    {
        switch (CurrentlViewState)
        {
            case HotelViewState.Roof:
                SwitchToRoof();
                break;
            case HotelViewState.Walls:
                SwitchToBig();
                break;
            case HotelViewState.Floor:
                SwitchToSmall();
                break;
        }
    }

    private void SwitchToBig()
    {
        if (_smallWalls == null || _bigWalls == null || _roof == null)
        {
            //Debug.Log("WallsSystem Not Inited!");
            return;
        }

        SwitchGameObjects(null, _bigWalls, false);
    }

    private void SwitchToSmall()
    {
        if (_smallWalls == null || _bigWalls == null || _roof == null)
        {
            //Debug.Log("WallsSystem Not Inited!");
            return;
        }

        SwitchGameObjects(_bigWalls, _smallWalls, false);
    }

    private void SwitchToRoof()
    {
        if (_smallWalls == null || _bigWalls == null || _roof == null)
        {
            // Debug.Log("WallsSystem Not Inited!");
            return;
        }
        SwitchGameObjects(null, _bigWalls, true);
    }

    private void SwitchGameObjects(GameObject[] offWalls, GameObject[] onWalls, bool isRoof)
    {
        if (offWalls != null)
        {
            foreach (var wall in offWalls)
            {
                wall.SetActive(false);
            }
        }

        foreach (var wall in onWalls)
        {
            wall.SetActive(true);
        }

        if (isRoof)
        {
            foreach (var roof in _roof)
                roof.SetActive(true);
        }
        else
        {
            foreach (var roof in _roof)
                roof.SetActive(false);
        }
    }
}
