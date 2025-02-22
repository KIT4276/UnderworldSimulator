using UnityEngine;

public abstract class BaseItem : MonoBehaviour
{
    [SerializeField] protected Sprite _icon;
    [SerializeField] protected string _hints;
    public Sprite GetIcon() => _icon;

    public string GetHint() => _hints;
}
