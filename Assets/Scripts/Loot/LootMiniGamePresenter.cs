using System;
using UnityEngine;
using Zenject;

public class LootMiniGamePresenter
{
    private LootMiniGameModel _model;
    private GameObject _miniGameCanvas;
    private TargetAreaMiniGame _targetArea;
    private RectTransform _carriage;
    private RectTransform _area;
    private float _speed;
    private MiniGameStage[] _miniGameStages;
    private bool _isRunning;

    public event Action<int> EndMiniGame;

    [Inject]
    public void Construct(LootMiniGameModel model)
    {
        _model = model;
    }

    public void Init(GameObject miniGameCanvas, TargetAreaMiniGame targetArea, RectTransform carriage, RectTransform area,
        float speed, MiniGameStage[] miniGameStages)
    {
        _miniGameCanvas = miniGameCanvas;
        _targetArea = targetArea;
        _carriage = carriage;
        _area = area;
        _speed = speed;
        _miniGameStages = miniGameStages;

        _model.Init(_carriage, _area, _targetArea, _speed, _miniGameStages);
        _model.EndMiniGame += OnMiniGameEnd;
    }

    public void OpenMiniGame()
    {
        _model.OpenMiniGame();
    }

    private void OnMiniGameEnd(int count)
    {
        _isRunning = false;
        EndMiniGame?.Invoke(count);
    }

    public void StartOrStopMimGame()
    {
        if (!_isRunning)
        {
            StartFirstTime();
        }
        else
        {
            TriggerNextStage();
        }
    }
    public void Update()
    {
        if (_isRunning)
        {
            _model.Update();
        }
    }

    private void StartFirstTime()
    {
        _isRunning = true;
        _miniGameCanvas.SetActive(true);
        _targetArea.gameObject.SetActive(true);
        _targetArea.RandomizeCarriagePosition();
        _model.StartMiniGame();
        _model.StartStage();
    }

    private void TriggerNextStage()
    {
        _model.EvaluateStage();
        _model.StartStage();
    }

    public void OnSpeedChange(float speed)
    {
        _speed = speed;
        _model.OnSpeedChange(speed);
    }
}
