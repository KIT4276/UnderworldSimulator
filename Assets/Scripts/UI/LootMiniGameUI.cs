using TMPro;
using UnityEngine;
using UnityEngine.UI;
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
    [SerializeField] private Slider _slider;
    [SerializeField] private TMP_InputField _inputField;
    //[SerializeField] private TMP_Text _speedText;

    private LootMiniGamePresenter _presenter;
    private StateMachine _stateMachine;
    private bool _isOpen;

    public MiniGameStage[] MiniGameStages { get => _miniGameStages; }

    [Inject]
    public void Construct(LootMiniGamePresenter presenter, StateMachine stateMachine)
    {
        _presenter = presenter;
        _stateMachine = stateMachine;
        _presenter.Init(_miniGameCanvas, _targetArea, _carriage, _area, _speed, _miniGameStages);
    }

    private void Start()
    {
        _slider.value = _speed;
        _inputField.text = _speed.ToString();

        _targetArea.gameObject.SetActive(false);
        _miniGameCanvas.SetActive(false);

        _compositeLoot.Interacted += OnInteracted;
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

    public void OnSpeedChangeSlider()
    {
        _speed = _slider.value;
        _inputField.text = _speed.ToString();
        _presenter.OnSpeedChange(_speed);
    }

    public void OnSpeedChangeInputField()
    {
        float.TryParse(_inputField.text, out _speed);
        _slider.value = _speed;
        _presenter.OnSpeedChange(_speed);
    }

    private void OnInteracted()
    {
        if (!_isOpen)
        {
            OpenMiniGame();
        }
        else
        {
            StartOrStopMimGame();
        }
    }

    private void OpenMiniGame()
    {
        _miniGameCanvas.SetActive(true);
        _slider.value = _speed;
        _inputField.text = _speed.ToString();
        _presenter.OpenMiniGame();

        if(_stateMachine.IsTests)
        {
            _slider.gameObject.SetActive(true);
        }
        else
        {
            _slider.gameObject.SetActive(false);
        }
        _isOpen = true;
    }

    private void OnMiniGameEnd(int count)
    {
        _miniGameCanvas.SetActive(false);
        _lootInteract.DoLoot(count);
        _isOpen = false;
    }

    private void OnDisable()
    {
        _compositeLoot.Interacted -= OnInteracted;
        _presenter.EndMiniGame -= OnMiniGameEnd;
    }
}
