using UnityEngine;

public class TestInventorySlot : MonoBehaviour
{
    [SerializeField] private InventorySlot _slot;
    [SerializeField] private int _count;
    [SerializeField] private BaseItem _itemPrefab;

    private bool _isInited;

    private void Awake()
    {
        Debug.Log("Awake " + this.gameObject.name);
        _slot.InitializedAction += InitSlot;
    }

    private void InitSlot()
    {
       
        if (_isInited|| _count == 0 && _itemPrefab == null) return;

            for (int i = 0; i < _count; i++)
            {
                _slot.SetItem(_itemPrefab);
                _isInited = true;
            }
    }

    private void OnDisable()
    {
        _slot.InitializedAction -= InitSlot;
    }
}
