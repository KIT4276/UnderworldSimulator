using TMPro;
using UnityEngine;

public class ProgressPanel : MonoBehaviour
{
    [SerializeField] protected TMP_Text _text;

    protected IProgressSystem _system;

    public void Init(IProgressSystem system)
    {
        _system = system;
        OnChange();

        _system.Change += OnChange;

    }

    protected virtual void OnChange()
    {
        _text.text = _system.CurrentValue.ToString();
    }

    protected void OnDestroy()
    {
        _system.Change -= OnChange;
    }
}
