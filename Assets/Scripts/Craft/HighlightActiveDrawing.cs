using UnityEngine;

public class HighlightActiveDrawing : MonoBehaviour
{
    [SerializeField] private CraftSlot _slot;
    [Space]
    [SerializeField] private ButtonEnterChangeImage _buttonEnterChangeImage;
    [SerializeField] private CraftMenu _craftMenu;
    [Space]
    [SerializeField] private Sprite _selectedNormImage;
    [SerializeField] private Sprite _selectedHighlightImage;
    [SerializeField] private Sprite _unselectedNormImage;
    [SerializeField] private Sprite _unselectedHighlightImage;


    private CraftSystem _craftSystem;

   
    public void Construct(CraftSystem craftSystem)
    {
        _craftSystem = craftSystem;

        _craftSystem.DrawingSelected += OnDrawingSelected;
        _craftMenu.Filled += OnFilled;
    }

    private void OnFilled()
    {
        OnDrawingSelected(_craftSystem.ActiveDrawing);
        _buttonEnterChangeImage.NormalizeImage();
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
            _buttonEnterChangeImage.ChangeImageToSelected(_selectedNormImage, _selectedHighlightImage);
            _buttonEnterChangeImage.HighlightImage();
        }
        else
        {
            _buttonEnterChangeImage.ChangeImageToSelected(_unselectedNormImage, _unselectedHighlightImage);
            _buttonEnterChangeImage.NormalizeImage();
        }
    }

    private void OnDestroy()
    {
        if (_craftSystem != null)
        {
            _craftSystem.DrawingSelected -= OnDrawingSelected;
        }
    }
}
