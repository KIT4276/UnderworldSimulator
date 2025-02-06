using System;
using UnityEngine;

[RequireComponent(typeof(Decor))]
public class DecorRotator : MonoBehaviour
{
    [SerializeField] private Transform _frontImpassableZone;
    [SerializeField] private Transform _sideImpassableZone;
    [Space]
    [SerializeField] private Collider2D _frontClickableCollider;
    [SerializeField] private Collider2D _sideClickableCollider;
    [Space]
    [SerializeField] private Collider2D _frontOccupiedZone;
    [Tooltip("optional field"),
    SerializeField]
    private Collider2D _sideOccupiedZone;

    private Decor _decor;

    public void Initialize(Decor decor, RotationState currentRotationState)
    {
        _decor = decor;
        UpdateColliders(currentRotationState);
        UpdateImpassableZone(currentRotationState);
        _decor.Rotated += OnRotate;
    }

    public void OnRemoved()
    {
        _decor.SetRotationState(RotationState.Front);
        _decor.Rotated -= OnRotate;
    }

    private void OnRotate(RotationState predioslyRotationState)
    {
        var newRotationState = (RotationState)(((int)predioslyRotationState + 1) % 4);
        _decor.SetRotationState(newRotationState);
        UpdateImpassableZone(newRotationState);
        UpdateColliders(newRotationState);
        _decor.SetCurrentClickableCollider(UpdateClickableColliders(newRotationState));
    }

    private void UpdateImpassableZone(RotationState newRotationState)
    {
        if (newRotationState == RotationState.Front || newRotationState == RotationState.Back)
        {
            _frontImpassableZone.gameObject.SetActive(true);
            _sideImpassableZone.gameObject.SetActive(false);
        }
        else
        {
            _frontImpassableZone.gameObject.SetActive(false);
            _sideImpassableZone.gameObject.SetActive(true);
        }
    }

    private Collider2D UpdateClickableColliders(RotationState newRotationState)
    {
        if (newRotationState == RotationState.Front || newRotationState == RotationState.Back)
            return _frontClickableCollider;
        else
            return _sideClickableCollider;
    }

    private void UpdateColliders(RotationState rotationState)
    {
        switch (rotationState)
        {
            case RotationState.Front:
                _decor.SetCurrentDecorCollider(_frontOccupiedZone);
                break;
            case RotationState.Left:
                if (_sideOccupiedZone == null)
                    _decor.SetCurrentDecorCollider(_frontOccupiedZone);
                else _decor.SetCurrentDecorCollider(_sideOccupiedZone);
                break;
            case RotationState.Back:
                _decor.SetCurrentDecorCollider(_frontOccupiedZone);
                break;
            case RotationState.Right:
                if (_sideOccupiedZone == null)
                    _decor.SetCurrentDecorCollider(_frontOccupiedZone);
                else _decor.SetCurrentDecorCollider(_sideOccupiedZone);
                break;
        }
    }
}