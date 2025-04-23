using System;
using TMPro;
using UnityEngine;
using Zenject;

public class MilestonesPanel : MonoBehaviour
{
    [SerializeField] private FadeInPanel _fadeInSign;
    //[SerializeField] private TMP_Text _xp;
    //[SerializeField] private TMP_Text _rewardText;

    private MilestoneSystem _milestoneSystem;
    private StateMachine _stateMachine;
    private ProgressSystem _progressSystem;
    private bool _isInit;

    [Inject]
    private void Construct(MilestoneSystem milestoneSystem, StateMachine stateMachine, ProgressSystem progressSystem)
    {
        _milestoneSystem = milestoneSystem;
        _stateMachine = stateMachine;
        _progressSystem = progressSystem;

        stateMachine.ChangeStateAction += OnChangeState;
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
            _isInit = true;
        }
    }

    private void ShowPanel()
    {
        _fadeInSign.Show();
        //_rewardText.text =  _milestoneSystem.ReachedMilestone.Reward;
        //_xp.text = _progressSystem.CurrentValue.ToString();
    }

    private void OnDestroy()
    {
        _stateMachine.ChangeStateAction -= OnChangeState;
        _milestoneSystem.Change -= ShowPanel;
    }
}
