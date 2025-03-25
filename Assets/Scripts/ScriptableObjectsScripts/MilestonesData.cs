using System;
using UnityEngine;

[CreateAssetMenu(fileName = "MilestonesData", menuName = "ScriptableObjects/MilestonesData", order = 6)]
public class MilestonesData : ScriptableObject
{
    [SerializeField] private Milestone[] _milestones;

    public Milestone[] Milestones { get { return _milestones; } }
}

[Serializable]
public class Milestone
{
   // [SerializeField] private int _id;
    [SerializeField] private int _progressValue;
    [SerializeField] private string _message;
    

    //[SerializeField] private RewardData _reward; //TODO real reward

    public int ProgressValue { get { return _progressValue; } }
    public string Reward { get { return _message; } }
    //public int ID { get { return _id; } }
    //public RewardData Reward { get { return _reward; } }
}

