using UnityEngine;

[RequireComponent(typeof(Decor))]
public class DecorView : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _mainRenderer;
    [Space]
    [SerializeField] protected Sprite _frontSprite;
    [SerializeField] protected Sprite _leftSprite;
    [SerializeField] protected Sprite _backSprite;
    [Tooltip("optional field"),
    SerializeField] protected Sprite _rightSprite;
    

    private Decor _decor;
    private PersistantStaticData _staticData;
    private Color _originalColor;
    private bool _canView;

    public void Initialize(Decor decor, PersistantStaticData staticData,  RotationState currenrRotationState)
    {
        _canView = true;
        _decor = decor;
        _staticData = staticData;
        _originalColor = _mainRenderer.color;

        UpdateSprite(currenrRotationState);
        UpdateView();

        _decor.DecorPlacedAction += OnPlaced;
        _decor.EndRotation += OnRotated;
    }

    public void OnRemoved()
    {
        _mainRenderer.color = _originalColor;
        _canView = false;
    }

    private void LateUpdate()
    {
        if (_decor.IsDragging && _canView)
            UpdateView();
    }


    private void UpdateView()
    {
        if (_decor.IsInside)
            _mainRenderer.color = _staticData.AllowedPositionColor;
        else
            _mainRenderer.color = _staticData.BannedPositionColor;
    }

    private void OnRotated(RotationState rotationState) => 
        UpdateSprite(rotationState);

    private void OnPlaced() => 
        _mainRenderer.color = _originalColor;

    private void UpdateSprite(RotationState state)
    {
        switch (state)
        {
            case RotationState.Front:
                _mainRenderer.sprite = _frontSprite;
                _mainRenderer.flipX = false;
                break;
            case RotationState.Left:
                _mainRenderer.sprite = CheckingSpriteExistence(_leftSprite, _rightSprite);
                break;
            case RotationState.Back:
                _mainRenderer.sprite = _backSprite;
                _mainRenderer.flipX = false;
                break;
            case RotationState.Right:
                _mainRenderer.sprite = CheckingSpriteExistence(_rightSprite, _leftSprite);
                break;
        }
    }

    private Sprite CheckingSpriteExistence(Sprite requiredSprite, Sprite replacementSprite)
    {
        if (requiredSprite == null)
        {
            _mainRenderer.flipX = true;
            return replacementSprite;
        }
        else
        {
            _mainRenderer.flipX = false;
            return requiredSprite;
        }
    }

    private void OnDisable()
    {
        _decor.DecorPlacedAction -= OnPlaced;
        _decor.EndRotation -= OnRotated;
    }
}
