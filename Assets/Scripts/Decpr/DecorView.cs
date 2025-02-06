using System;
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

    public void Initialize(Decor decor, PersistantStaticData staticData,  RotationState currenrRotationState)
    {
        _decor = decor;
        _staticData = staticData;
        _originalColor = _mainRenderer.color;

        UpdateSprite(currenrRotationState);

        _decor.DecorPlacedAction += OnPlaced;
        _decor.EndRotation += OnRotated;
    }

    public void OnRemoved()
    {
        _mainRenderer.color = _originalColor;
        UpdateView();

        _decor.DecorPlacedAction -= OnPlaced;
        _decor.EndRotation -= OnRotated;
        _decor.Removed -= OnRemoved;
    }

    private void LateUpdate()
    {
        if (_decor.IsDragging)
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

    protected void OnDisable()
    {
        _decor.DecorPlacedAction -= OnPlaced;
        _decor.EndRotation -= OnRotated;
    }
}
