using TMPro;
using UnityEngine;

public class ProgressPanel : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;

     private IProgressSystem _system;

    public void Init(IProgressSystem system)
    {
        _system = system;
        OnChange();
        _system.Change += OnChange;
    }

    private void OnChange()
    {
        Debug.Log(gameObject.name);
        _text.text = _system.Current.ToString();
    }
}
