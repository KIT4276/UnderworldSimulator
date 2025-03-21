using System;
using UnityEngine;

[CreateAssetMenu(fileName = "MilestonesData", menuName = "ScriptableObjects/MilestonesData", order = 6)]
public class MilestonesData : ScriptableObject
{
    [SerializeField] private MilestoneData[] _milestones;

    public MilestoneData[] Milestones {  get { return _milestones; } }
}

[Serializable]
public class MilestoneData
{
    [SerializeField] private int _progressValue;
    [SerializeField] private string _reward; //TODO real reward

    public int ProgressValue { get { return _progressValue; } }
    public string Reward { get { return _reward; } }
}
