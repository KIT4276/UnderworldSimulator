using System;
using UnityEngine;
using Zenject;

public class LootMiniGameUI : MonoBehaviour
{
    [SerializeField] private GameObject _miniGameCanvas;
    [SerializeField] private CompositeLoot _compositeLoot;
    [SerializeField] private TargetAreaMiniGame _targetArea;
    [SerializeField] private LootInteract _lootInteract;
    [Space]
    [SerializeField] private RectTransform _carriage;
    [SerializeField] private RectTransform _area;
    [SerializeField] private float _speed;
    [Space]
    [SerializeField] private MiniGameStage[] _miniGameStages;

    private bool _running = false;

    [Inject] private LootMiniGameModel _lootMiniGameModel;

    public event Action Stoped;
    public event Action Started;

    public MiniGameStage[] MiniGameStages {  get => _miniGameStages; } 

    private void Start()
    {
        _targetArea.gameObject.SetActive(false);
        _miniGameCanvas.SetActive(false);
        _compositeLoot.MiniGameOpen += OpenMiniGame;
        _lootMiniGameModel.EndMiniGame += OnMiniGameEnd;
        _lootMiniGameModel.Init(_carriage);
    }

    private void OnMiniGameEnd(int count)
    {
        _miniGameCanvas.SetActive(false);
        _lootInteract.DoLoot(count);
    }

    public void StartOrStopMimGame()
    {
        if (!_running)
        {
            StartFirstTime();
        }
        else
        {
            TriggerNextStage();
        }
    }

    private void StartFirstTime()
    {
        _running = true;
        _miniGameCanvas.SetActive(true);
        _targetArea.gameObject.SetActive(true);
        _targetArea.RandomizeCarriagePosition();
        _lootMiniGameModel.StartMiniGame(_carriage, _area, _targetArea, _speed, this, _miniGameStages);
        Started?.Invoke();
    }

    private void TriggerNextStage()
    {
        _lootMiniGameModel.EvaluateStage();
        Started?.Invoke();
    }

    private void OpenMiniGame()
    {
        _running = false;
        _miniGameCanvas.SetActive(true);
        _lootMiniGameModel.StartMiniGame(_carriage, _area, _targetArea, _speed, this, _miniGameStages);
    }

    private void Update()
    {
        if (_running)
        {
            _lootMiniGameModel.Update();
        }
    }

    private void OnDisable()
    {
        _compositeLoot.MiniGameOpen -= OpenMiniGame;
        _lootMiniGameModel.EndMiniGame -= OnMiniGameEnd;
    }
}
