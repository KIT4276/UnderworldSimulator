using TMPro;
using Unity.VisualScripting;
using UnityEngine;


public class InventorySlotCounter : MonoBehaviour
{
    [SerializeField] private int _count = 1; //tempotaty
    [SerializeField] private TMP_Text _textMeshPro;
    [SerializeField] private GameObject _x_TextTablet;
    [SerializeField] private InventorySlot _inventorySlot;

    public int Count { get; private set; }

    private void Awake()
    {
        Count = _count;
        CheckingAndShow();
    }

    public void DecreaseCount()
    {
        Count--;
        CheckingAndShow();
    }

    public void SetCount(int count)
    {
        Count = count;
        CheckingAndShow();
    }

    private void CheckingAndShow()
    {
        if (_textMeshPro == null || _x_TextTablet == null) return;

        if (Count > 1)
        {
            _textMeshPro.gameObject.SetActive(true);
            _x_TextTablet.SetActive(true);
            _textMeshPro.text = Count.ToString();
        }
        else
        {
            _textMeshPro.gameObject.SetActive(false);
            _x_TextTablet.SetActive(false);

            if (Count == 0)
                _inventorySlot.Deactivate();
        }

    }
}

