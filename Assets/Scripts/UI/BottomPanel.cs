using UnityEngine;
using Zenject;

public class BottomPanel : MonoBehaviour
{
    [SerializeField] private ProgressPanel _progressPanel;
    [SerializeField] private ProgressPanel _milestonePanel;

    private StatesTransitor _transitor;

    [Inject]
    private void Construct(StatesTransitor transitor, ProgressSystem progressSystem, MilestoneSystem milestoneSystem)
    {
        _transitor = transitor;

        _progressPanel.Init(progressSystem);
        _milestonePanel.Init(milestoneSystem);
    }

    public void OpenInventory()
    {
        _transitor.ConditionalToInventoryState();
    }
}
