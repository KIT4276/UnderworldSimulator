using UnityEngine;

[RequireComponent(typeof(DecorInventorySlot))]
public class TestInventorySlot : MonoBehaviour
{
    [SerializeField] private DecorInventorySlot _slot;
    [SerializeField] private int _count;
    [SerializeField] private Decor _decorPrefab;

    private bool _isInited;


    private void Awake()
    {
        _slot.InitializedAction += InitSlot;
    }

    private void InitSlot()
    {
        if (_isInited) return;
        for (int i = 0; i < _count; i++)
        {
            _slot.SetDecor(_decorPrefab);
            _isInited = true;
        }
    }

    private void OnDisable()
    {
        _slot.InitializedAction -= InitSlot;
    }
}
