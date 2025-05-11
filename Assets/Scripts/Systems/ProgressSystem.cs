using System;
using System.Collections.Generic;
using UnityEngine;

public class ProgressSystem : IProgressSystem
{
    private float _currentProgress = 0;
    private List<Task> _completedTasks = new();

    public float CurrentValue { get => _currentProgress; }


    public event Action Change;

    public void RemoveCompletedTask(Task task)
    {
        if (_completedTasks.Contains(task))
        {
            _completedTasks.Remove(task);
            /// Debug.Log("RemoveCompletedTask " + task);
            UpdateProgress();
        }
    }

    public void AddCompletedTask(Task task)
    {
        if (!_completedTasks.Contains(task))
        {
            _completedTasks.Add(task);
            //Debug.Log("AddCompletedTask " + task);
            UpdateProgress();
        }
    }

    public void UpdateProgress()
    {
        _currentProgress = 0;
        foreach (var task in _completedTasks)
        {
            //Debug.Log(task);
            _currentProgress += task.XP;
        }
        Change?.Invoke();
    }

    public void ChangeProgress(int v)
    {
        _currentProgress += v;
        Change?.Invoke();
    }
}

public class MilestoneSystem : IProgressSystem
{
    private readonly ProgressSystem _progressSystem;
    private readonly MilestonesData _milestonesData;
    private readonly StateMachine _stateMachine;

    public Milestone ReachedMilestone { get; private set; }
    public Milestone CurrentMilestone { get; private set; }

    public float CurrentValue { get => CurrentMilestone.ProgressValue; }

    public event Action Change;

    public MilestoneSystem(ProgressSystem progressSystem, MilestonesData milestones, StateMachine stateMachine)
    {
        _progressSystem = progressSystem;
        _milestonesData = milestones;
        _stateMachine = stateMachine;
        CurrentMilestone = _milestonesData.Milestones[0];
        _progressSystem.Change += OnProgressChange;
    }

    public int CurrentMilestonesIndex()
    {
        return Array.IndexOf(_milestonesData.Milestones, CurrentMilestone);
    }

    private void OnProgressChange()
    {
        if (_progressSystem.CurrentValue >= CurrentMilestone.ProgressValue)
        {
            GiveReward();
            int i = Array.IndexOf(_milestonesData.Milestones, CurrentMilestone);
            i++;
            if (i < _milestonesData.Milestones.Length)
            {
                ReachedMilestone = _milestonesData.Milestones[i - 1];
                CurrentMilestone = _milestonesData.Milestones[i];
                _stateMachine.Enter<MilestoneState>();
                Change?.Invoke();
            }
        }
    }

    private void GiveReward()
    {
        //Debug.Log(CurrentMilestone.Reward);
        AudioReciever.Instance.PlayMilestoneReached();
    }
}

public interface IProgressSystem
{
    public float CurrentValue { get; }

    public event Action Change;
}
