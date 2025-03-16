using System;
using UnityEngine;
using Zenject;

public class ProgressSystem : IProgressSystem
{
    private int _currentProgress = 0;
    
    public int Current { get => _currentProgress; }

    public event Action Change;
}

public class MilestoneSystem : IProgressSystem
{
    private int _currentMilestone = 50;

    public int Current { get => _currentMilestone; }

    public event Action Change;
}

public interface  IProgressSystem
{
    public int Current{ get; }

    public event Action Change;
}
