using UnityEngine;

public class GuestsPoint : MonoBehaviour
{
    [SerializeField] private GuestsType _type;
    [SerializeField] private SpriteRenderer _spriteRenderer;

    public GuestsType Type { get => _type; }

    private void Start()
    {
        _spriteRenderer.enabled = false;
    }

    private void OnDestroy()
    {
        _spriteRenderer.enabled = true;
    }
}
