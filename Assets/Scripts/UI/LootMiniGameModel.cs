using System;
using System.Linq;
using UnityEngine;

public class LootMiniGameModel
{
    private RectTransform _carriage;
    private RectTransform _movementArea;
    private TargetAreaMiniGame _targetArea;
    private float _speed;

    private float direction = 1f;
    private float leftBound;
    private float rightBound;
    private MiniGameStage[] _miniGameStages;
    private LootMiniGameUI _lootMiniGameUI;
    private Vector2 _startPosition;

    public bool IsInsideTarget { get; private set; }

    public Action<int> EndMiniGame;

    private bool _isGameCompleted = false;

    public void Init(RectTransform carriage)
    {
        _startPosition = carriage.transform.position;
    }

    public void StartMiniGame(RectTransform carriage, RectTransform area, TargetAreaMiniGame targetArea, float speed, LootMiniGameUI lootMiniGameUI, MiniGameStage[] miniGameStages)
    {
        _isGameCompleted = false;
        IsInsideTarget = false;

        _carriage = carriage;
        _movementArea = area;
        _targetArea = targetArea;
        _speed = speed;
        _miniGameStages = miniGameStages;
        _lootMiniGameUI = lootMiniGameUI;

        carriage.transform.position = _startPosition;

        ResetAllStages();

        UnsubscribeEvents();
        _lootMiniGameUI.Started += StartStage;
        _lootMiniGameUI.Stoped += HandleStageStop;

        CalculateBounds();
    }

    public void EvaluateStage()
    {
        if (_isGameCompleted) return;

        if (_miniGameStages == null || _miniGameStages.Length == 0)
            return;

        int activeIndex = -1;
        for (int i = 0; i < _miniGameStages.Length; i++)
        {
            if (_miniGameStages[i] != null && _miniGameStages[i].CurrentState == StageState.Active)
            {
                activeIndex = i;
                break;
            }
        }

        if (activeIndex == -1) return;

        if (IsInsideTarget)
            _miniGameStages[activeIndex].SetStageState(StageState.Passed);
        else
            _miniGameStages[activeIndex].SetStageState(StageState.Failed);

        if (_miniGameStages.All(s => s != null &&
            (s.CurrentState == StageState.Passed || s.CurrentState == StageState.Failed)))
        {
            CompleteGame();
        }
    }

    private void HandleStageStop()
    {
        if (_isGameCompleted) return;

    }

    private void ResetAllStages()
    {
        if (_miniGameStages == null || _miniGameStages.Length == 0)
            return;

        foreach (var miniGameStage in _miniGameStages)
        {
            if (miniGameStage != null)
            {
                miniGameStage.SetStageState(StageState.Passive);
            }
        }
    }

    private void StartStage()
    {
        if (_isGameCompleted) return;

        _targetArea.RandomizeCarriagePosition();

        if (_miniGameStages == null || _miniGameStages.Length == 0)
            return;

        if (_miniGameStages.Any(s => s != null && s.CurrentState == StageState.Active))
            return;

        MiniGameStage firstPassive = _miniGameStages.FirstOrDefault(
            s => s != null && s.CurrentState == StageState.Passive);

        if (firstPassive != null)
        {
            firstPassive.SetStageState(StageState.Active);
        }
        else
        {
            CompleteGame();
        }
    }

    private void UnsubscribeEvents()
    {
        if (_lootMiniGameUI != null)
        {
            _lootMiniGameUI.Started -= StartStage;
            _lootMiniGameUI.Stoped -= StopStage;
        }
    }

    private void StopStage()
    {
        if (_isGameCompleted) return;

        if (_miniGameStages == null || _miniGameStages.Length == 0)
            return;

        int activeIndex = -1;
        for (int i = 0; i < _miniGameStages.Length; i++)
        {
            if (_miniGameStages[i] != null && _miniGameStages[i].CurrentState == StageState.Active)
            {
                activeIndex = i;
                break;
            }
        }

        if (activeIndex == -1) return;

        if (IsInsideTarget)
        {
            _miniGameStages[activeIndex].SetStageState(StageState.Passed);
        }
        else
        {
            _miniGameStages[activeIndex].SetStageState(StageState.Failed);
        }

        if (_miniGameStages.All(s => s != null &&
            (s.CurrentState == StageState.Passed || s.CurrentState == StageState.Failed)))
        {
            CompleteGame();
        }
    }

    private void CompleteGame()
    {
        if (_isGameCompleted) return;

        _isGameCompleted = true;
        int passedCount = _miniGameStages.Count(s => s != null && s.CurrentState == StageState.Passed);
        EndMiniGame?.Invoke(passedCount);
        UnsubscribeEvents();
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
        if (_isGameCompleted) return; 

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

        bool newIsInside = RectTransformUtility.RectangleContainsScreenPoint(_targetArea.GetComponent<RectTransform>(), carriageCenter, null);

        if (newIsInside != IsInsideTarget)
        {
            IsInsideTarget = newIsInside;
        }
    }

    private void End()
    {
        UnsubscribeEvents();
    }
}