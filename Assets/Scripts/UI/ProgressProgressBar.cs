using Zenject;

public class ProgressProgressBar : ProgressBar
{
    [Inject] private ProgressSystem _progressSystem;
    [Inject] private MilestoneSystem _milestoneSystem;
    // private float _previousValue;

    private void Start()
    {
        _progressSystem.Change += OnValueChange;
        _milestoneSystem.Change += OnValueChange;

        // _previousValue = 0;
    }

    private void OnValueChange()
    {
        // if (_previousValue <= _progressSystem.CurrentValue) AudioManager.Instance.Play(SoundEnum.hotel_ui_milestone_remove);
        // else AudioManager.Instance.Play(SoundEnum.hotel_ui_milestone_add);

        ValueChange(_progressSystem.CurrentValue / _milestoneSystem.CurrentValue);
        // _previousValue = _progressSystem.CurrentValue;
    }

    private void OnDestroy()
    {
        _progressSystem.Change -= OnValueChange;
        _milestoneSystem.Change -= OnValueChange;
    }
}
