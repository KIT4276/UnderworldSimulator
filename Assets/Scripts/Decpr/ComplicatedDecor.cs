using UnityEngine;
using UnityEngine.InputSystem;

public class ComplicatedDecor : Decor
{
    [SerializeField] private RotationState _excludedDirection;


    protected override void OnRotate(InputAction.CallbackContext context)
    {
        if (!_isPlacing || !_isOnDecorState) return;
        //..
        _rotationState = (RotationState)(((int)_rotationState + 1) % 4);
        _impassableZone.rotation *= Quaternion.Euler(0, 0, 90);
        {
            if (_rotationState == _excludedDirection)
                _rotationState = (RotationState)(((int)_rotationState + 1) % 4);
            _impassableZone.rotation *= Quaternion.Euler(0, 0, 90);
        }

        UpdateSprite(_rotationState);
        _impassableZone.rotation *= Quaternion.Euler(0, 0, 90);
        UpdatePolygonSplitter();
        //_polygonSplitter.RemoveCells();
        //_polygonSplitter.Initialize(_assets, _staticData);
    }
}