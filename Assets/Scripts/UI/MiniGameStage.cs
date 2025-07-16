using UnityEngine;
using UnityEngine.UI;

public class MiniGameStage : MonoBehaviour
{
    [SerializeField] private Image _image;
    [Space]
    [SerializeField] private Color _passiveColor;
    [SerializeField] private Color _activeColor;
    [SerializeField] private Color _passedColor;
    [SerializeField] private Color _failedColor;

    public StageState CurrentState { get; private set; }

    private void Start()
    {
        SetStageState(StageState.Passive);
    }

    public void SetStageState(StageState state)
    {
        CurrentState = state;

        switch (state)
        {
            case StageState.Passive:
                _image.color = _passiveColor;
                break;
            case StageState.Active:
                _image.color = _activeColor;
                break;
            case StageState.Passed:
                _image.color = _passedColor;
                break;
            case StageState.Failed:
                _image.color = _failedColor;
                break;
        }
    }
}

public enum StageState
{
    Passive,
    Active,
    Passed,
    Failed,
}
