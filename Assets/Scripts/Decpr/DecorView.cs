using System;
using UnityEngine;

[RequireComponent(typeof(Decor))]
public class DecorView : MonoBehaviour
{
    [SerializeField] private Decor _decor;
    [SerializeField] private SpriteRenderer _mainRenderer;
    [Space]
    [SerializeField] protected Sprite _frontSprite;
    [SerializeField] protected Sprite _leftSprite;
    [SerializeField] protected Sprite _backSprite;
    [SerializeField] protected Sprite _rightSprite;
    

    private Color _originalColor;
    private PersistantStaticData _staticData;

    public void Initialize(PersistantStaticData staticData)
    {
        _originalColor = _mainRenderer.color;
        _staticData = staticData;

        UpdateSprite(_decor.RotationState);

        _decor.DecorPlacedAction += OnPlaced;
        _decor.DecorRotated += OnRotated;
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

    private void OnRotated()
    {
        UpdateSprite(_decor.RotationState);
    }

    private void OnPlaced()
    {
        _mainRenderer.color = _originalColor;
    }

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
}
