using TMPro;
using UnityEngine;

public class SlotShowSign : BaseShowSign
{
    [SerializeField] private Hint _hint;
    [SerializeField] private InventorySlot _slot;
    [SerializeField] private RectTransform _rectTransform;
   
   

    protected override void ShowSign()
    {
        if (_slot.IsOccupied)
        {
            base.ShowSign();
            _hint.AddIntent(_rectTransform.position);
            _hint.AddText(_slot.Items[0].GetHint());
            // _hintRectTransform.position = _rectTransform.position + new Vector3(_indentX, _indentY, 0);
            //_hintText.text = _slot.Items[0].GetHint();
        }
    }

    protected override void HideSign()
    {
        base.HideSign();
    }
}
