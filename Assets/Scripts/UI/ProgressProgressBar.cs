using Zenject;

public class ProgressProgressBar : ProgressBar
{
    [Inject] private ProgressSystem _progressSystem;
    [Inject] private MilestoneSystem _milestoneSystem;

    private void Start()
    {
        _progressSystem.Change += OnValueChange;
        _milestoneSystem.Change += OnValueChange;
    }

    private void OnValueChange()
    {
        ValueChange(_progressSystem.CurrentValue/ _milestoneSystem.CurrentValue);
    }

    private void OnDestroy()
    {
        _progressSystem.Change -= OnValueChange;
        _milestoneSystem.Change -= OnValueChange;
    }
}
