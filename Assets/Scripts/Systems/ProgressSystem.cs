using System;
using UnityEngine;

public class ProgressSystem : IProgressSystem
{
    private float _currentProgress = 0;
    
    public float CurrentValue { get => _currentProgress; }

    public event Action Change;

    public void UpProgress(int value)
    {
        _currentProgress += value;
        Change?.Invoke();   
    }
}

public class MilestoneSystem : IProgressSystem
{
    private MilestoneData _currentMilestone;
    private readonly ProgressSystem _progressSystem;
    private readonly MilestonesData _milestones;

    public float CurrentValue { get => _currentMilestone.ProgressValue; }

    public event Action Change;

    public MilestoneSystem(ProgressSystem progressSystem, MilestonesData milestones)
    {
        _progressSystem = progressSystem;
        _milestones = milestones;

        _currentMilestone = _milestones.Milestones[0];
        _progressSystem.Change += OnProgressChange;
    }

    private void OnProgressChange()
    {
        if(_progressSystem.CurrentValue >= _currentMilestone.ProgressValue)
        {
            Debug.Log(_currentMilestone.Reward);

            int i = Array.IndexOf(_milestones.Milestones, _currentMilestone);
            i++;
            if (i < _milestones.Milestones.Length)
            {
                _currentMilestone = _milestones.Milestones[i];
                Change?.Invoke();
            }
        }
    }
}

public interface  IProgressSystem
{
    public float CurrentValue{ get; }

    public event Action Change;
}
