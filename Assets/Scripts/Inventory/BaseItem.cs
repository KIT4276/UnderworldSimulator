using UnityEngine;

public abstract class BaseItem : MonoBehaviour
{
    [SerializeField] protected Sprite _icon;
    public Sprite GetIcon()
        => _icon;
}
