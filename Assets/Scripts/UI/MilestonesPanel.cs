using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class MilestonesPanel : MonoBehaviour
{
    [SerializeField] private FadeInPanel _fadeInSign;
    [SerializeField] private Image _newGuestImage;
    [SerializeField] private GameObject _newGuestText;

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
