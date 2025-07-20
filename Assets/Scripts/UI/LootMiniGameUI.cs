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

    private LootMiniGamePresenter _presenter;

    public MiniGameStage[] MiniGameStages { get => _miniGameStages; }

    [Inject]
    public void Construct(LootMiniGamePresenter presenter)
    {
        _presenter = presenter;

        _presenter.Init(_miniGameCanvas, _targetArea, _carriage, _area, _speed, _miniGameStages);
    }

    private void Start()
    {
        _targetArea.gameObject.SetActive(false);
        _miniGameCanvas.SetActive(false);

        _compositeLoot.MiniGameOpen += OpenMiniGame;
        _presenter.EndMiniGame += OnMiniGameEnd;
    }

    private void Update()
    {
        _presenter.Update();
    }

    public void StartOrStopMimGame()
    {
        _presenter.StartOrStopMimGame();
    }

    private void OpenMiniGame()
    {
        _miniGameCanvas.SetActive(true);
        _presenter.OpenMiniGame();
    }

    private void OnMiniGameEnd(int count)
    {
        _miniGameCanvas.SetActive(false);
        _lootInteract.DoLoot(count);
    }

    private void OnDisable()
    {
        _compositeLoot.MiniGameOpen -= OpenMiniGame;
        _presenter.EndMiniGame -= OnMiniGameEnd;
    }
}
