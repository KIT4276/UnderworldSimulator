using UnityEngine;

public class SlotShowSign : BaseShowSign
{
    [SerializeField] private Hint _hint;
    [SerializeField] private InventorySlot _slot;
    [SerializeField] private RectTransform _rectTransform;
    [SerializeField] private float _indentX = 3;
    [SerializeField] private float _indentY = 3;
    [SerializeField] private ParameterData _parameterData;

    protected override void ShowSign()
    {
        if (_slot.IsOccupied)
        {
            base.ShowSign();
            _hint.AddIntent(_rectTransform.position + new Vector3(_indentX, _indentY, 0));
            _hint.AddText(_slot.Items[0], _parameterData);
            // _hintRectTransform.position = _rectTransform.position + new Vector3(_indentX, _indentY, 0);
            //_hintText.text = _slot.Items[0].GetHint();
        }
    }

    protected override void HideSign()
    {
        base.HideSign();
    }
}
