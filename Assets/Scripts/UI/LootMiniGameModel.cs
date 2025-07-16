using System;
using System.Linq;
using UnityEngine;

public class LootMiniGameModel
{
    private RectTransform _carriage;
    private RectTransform _movementArea;
    private RectTransform _targetArea;
    private float _speed;

    private float direction = 1f;
    private float leftBound;
    private float rightBound;
    private MiniGameStage[] _miniGameStages;
    private Vector2 _startPosition;

    public bool IsInsideTarget { get; private set; }

    public void StartMiniGame(RectTransform carriage, RectTransform area, RectTransform targetArea, float speed, LootMiniGameUI lootMiniGameUI, MiniGameStage[] miniGameStages)
    {
        IsInsideTarget = false;

        _carriage = carriage;
        _movementArea = area;
        _targetArea = targetArea;
        _speed = speed;
        _miniGameStages = miniGameStages;
        _startPosition = carriage.transform.position;

        lootMiniGameUI.Started += StartStage;
        lootMiniGameUI.Stoped += StopStartStage;

        CalculateBounds();
    }

    private void StartStage()
    {
        _carriage.transform.position = _startPosition;

        if (_miniGameStages == null || _miniGameStages.Length == 0)
            return;

        bool hasActiveStage = _miniGameStages.Any(s => s != null && s.CurrentState == StageState.Active);
        if (hasActiveStage) return;

        MiniGameStage firstPassive = _miniGameStages.FirstOrDefault(
            s => s != null && s.CurrentState == StageState.Passive);

        if (firstPassive != null)
        {
            firstPassive.SetStageState(StageState.Active);
        }
        else
        {
            //TODO
            Debug.Log("Все стадии завершены");
        }
    }

    private void StopStartStage()
    {
        if (_miniGameStages == null || _miniGameStages.Length == 0)
            return;

        int activeIndex = Array.FindIndex(_miniGameStages,
            s => s != null && s.CurrentState == StageState.Active);

        if (IsInsideTarget)
        {
            _miniGameStages[activeIndex].SetStageState(StageState.Passed);
        }
        else
        {
            _miniGameStages[activeIndex].SetStageState(StageState.Failed);
        }

    }

    private void CalculateBounds()
    {
        Vector3[] areaCorners = new Vector3[4];
        _movementArea.GetWorldCorners(areaCorners);

        leftBound = areaCorners[0].x;
        rightBound = areaCorners[2].x;
    }

    public void Update()
    {
        Vector3 position = _carriage.position;
        position.x += direction * _speed * Time.deltaTime;

        if (position.x >= rightBound)
        {
            position.x = rightBound;
            direction = -1f;
        }
        else if (position.x <= leftBound)
        {
            position.x = leftBound;
            direction = 1f;
        }

        _carriage.position = position;

        CheckInsideTarget();
    }

    private void CheckInsideTarget()
    {
        Vector2 carriageCenter = RectTransformUtility.WorldToScreenPoint(null, _carriage.position);

        bool newIsInside = RectTransformUtility.RectangleContainsScreenPoint(_targetArea, carriageCenter, null);

        if (newIsInside != IsInsideTarget)
        {
            IsInsideTarget = newIsInside;
        }
    }
}