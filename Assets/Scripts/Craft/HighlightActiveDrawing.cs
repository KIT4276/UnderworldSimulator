using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class HighlightActiveDrawing : MonoBehaviour
{
    [SerializeField] private CraftSlot _slot;
    [Space]
    [SerializeField] private Image _buttonImage;
    [Space]
    [SerializeField] private Color _activeColor;
    [SerializeField] private Color _unactiveColor;


    private CraftSystem _craftSystem;

   
    public void Construct(CraftSystem craftSystem)
    {
        _craftSystem = craftSystem;

        _craftSystem.DrawingSelected += OnDrawingSelected;

    }

    private void Awake()
    {
        if (_craftSystem == null) return;

        if (_craftSystem.ActiveDrawing != null)
        {
            OnDrawingSelected(_craftSystem.ActiveDrawing);
        }
    }

    private void OnDrawingSelected(Drawing drawing)
    {
        if (_slot.Drawing == drawing)
        {
            _buttonImage.color = _activeColor;
        }
        else
        {
            _buttonImage.color = _unactiveColor;
        }

        //Debug.Log(_buttonImage.color);
        //RGBA(0.168, 1.000, 0.000, 1.000)
    }

    private void OnDestroy()
    {
        if (_craftSystem != null)
        {

            _craftSystem.DrawingSelected -= OnDrawingSelected;
        }
            _buttonImage.color = _unactiveColor;
    }
}
