using System;
using UnityEngine;

public class ProgressSystem : IProgressSystem
{
    private float _currentProgress = 0;
    
    public float CurrentValue { get => _currentProgress; }

    public event Action Change;

    public void ChangeProgress(int value)
    {
        _currentProgress += value;
        Change?.Invoke();   
    }

}

public class MilestoneSystem : IProgressSystem
{
    private readonly ProgressSystem _progressSystem;
    private readonly MilestonesData _milestonesData;

    public Milestone CurrentMilestone { get; private set; }

    public float CurrentValue { get => CurrentMilestone.ProgressValue; }

    public event Action Change;

    public MilestoneSystem(ProgressSystem progressSystem, MilestonesData milestones)
    {
        _progressSystem = progressSystem;
        _milestonesData = milestones;

        CurrentMilestone = _milestonesData.Milestones[0];
        _progressSystem.Change += OnProgressChange;
    }

    public int CurrentMilestonesIndex()
    {
        return Array.IndexOf(_milestonesData.Milestones, CurrentMilestone);
    }

    private void OnProgressChange()
    {
        if(_progressSystem.CurrentValue >= CurrentMilestone.ProgressValue)
        {
            GiveReward();
            int i = Array.IndexOf(_milestonesData.Milestones, CurrentMilestone);
            i++;
            if (i < _milestonesData.Milestones.Length)
            {
                CurrentMilestone = _milestonesData.Milestones[i];
                Change?.Invoke();
            }
        }
    }

    private void GiveReward()
    {
      Debug.Log(CurrentMilestone.Reward);
       
    }
}

public interface  IProgressSystem
{
    public float CurrentValue{ get; }

    public event Action Change;
}
