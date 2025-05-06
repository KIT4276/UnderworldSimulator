using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class MilestonesPanel : MonoBehaviour
{
    [SerializeField] private FadeInPanel _fadeInSign;
    [SerializeField] private Image _newGuestImage;
    [SerializeField] private GameObject _newGuestText;
    [SerializeField] private GameObject _victoryEffectPrefab;

    [SerializeField] private RectTransform _panelTransform;

    private GameObject _activeEffectInstance;

    private MilestoneSystem _milestoneSystem;
    private StateMachine _stateMachine;
    private GuestsSystem _guestsSystem;

    private bool _isInit;
    private Sprite _guestSprite;

    [Inject]
    private void Construct(MilestoneSystem milestoneSystem, StateMachine stateMachine, GuestsSystem guestsSystem)
    {
        _milestoneSystem = milestoneSystem;
        _stateMachine = stateMachine;
        _guestsSystem = guestsSystem;

        stateMachine.ChangeStateAction += OnChangeState;
    }

    private void OnGuestBecameAvailable(BaseHandledReward reward)
    {
        _guestSprite = ((Guest)reward).MilestonesIcon;
        _newGuestImage.sprite = _guestSprite;
    }

    public void Ok()
    {
        _fadeInSign.Hide();

        _stateMachine.EnterPredioslyState();
    }

    private void OnChangeState(IExitableState state)
    {
        if (state is GameLoopState && !_isInit)
        {
            _milestoneSystem.Change += ShowPanel;

            foreach (var guest in _guestsSystem.Guests)
            {
                guest.BecameAvailable += OnGuestBecameAvailable;
            }
            _isInit = true;
        }
    }

    private void ShowPanel()
    {
        _fadeInSign.Show();

        // Convert UI position to world space behind UI
        Vector3 screenPos = RectTransformUtility.WorldToScreenPoint(null, _panelTransform.position);
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(new Vector3(screenPos.x, screenPos.y, 5f)); // Z = behind UI

        // Clean up previous effect if needed
        if (_activeEffectInstance != null)
            Destroy(_activeEffectInstance);
        
        // Instantiate the prefab and position it
        if (_victoryEffectPrefab != null)
        {
            _activeEffectInstance = Instantiate(_victoryEffectPrefab, worldPos, Quaternion.identity);
            Destroy(_activeEffectInstance, 2f);
        }

        if (_guestSprite == null)
        {
            _newGuestImage.gameObject.SetActive(false);
            _newGuestText.gameObject.SetActive(false);
        }
        else
        {
            _newGuestImage.sprite = _guestSprite;
            _guestSprite = null;
        }
    }

    private void OnDestroy()
    {
        _stateMachine.ChangeStateAction -= OnChangeState;
        _milestoneSystem.Change -= ShowPanel;

        foreach (var guest in _guestsSystem.Guests)
        {
            guest.BecameAvailable -= OnGuestBecameAvailable;
        }
    }
}
