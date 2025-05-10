using UnityEngine;

[RequireComponent(typeof(Decor))]
public class DecorRotator : MonoBehaviour
{
    [Tooltip("required field"), SerializeField] private Transform _frontImpassableZone;
    [Tooltip("optional field"), SerializeField] private Transform _sideImpassableZone;
    [Space]
    [Tooltip("required field"), SerializeField] private Collider2D _frontClickableCollider;
    [Tooltip("optional field"), SerializeField] private Collider2D _sideClickableCollider;
    [Space]
    [Tooltip("required field"), SerializeField] private Collider2D _frontOccupiedZone;
    [Tooltip("optional field"), SerializeField] private Collider2D _sideOccupiedZone;

    private Decor _decor;

    public void Initialize(Decor decor, RotationState currentRotationState)
    {
        _decor = decor;
        UpdateColliders(currentRotationState);
        UpdateImpassableZone(currentRotationState);
        UpdateClickableColliders(currentRotationState);
        _decor.Rotated += OnRotate;
        _decor.SetCurrentClickableCollider(UpdateClickableColliders(RotationState.Front));
    }

    public void OnRemoved()
    {
        //_decor.SetRotationState(RotationState.Front);

    }

    private void OnRotate(RotationState predioslyRotationState)
    {
        
        var newRotationState = (RotationState)(((int)predioslyRotationState + 1) % 4);
        //Debug.Log(newRotationState);
        _decor.SetRotationState(newRotationState);
        UpdateImpassableZone(newRotationState);
        UpdateColliders(newRotationState);
        _decor.SetCurrentClickableCollider(UpdateClickableColliders(newRotationState));
    }

    private void UpdateImpassableZone(RotationState newRotationState)
    {
        if (_sideImpassableZone == null) return;

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


        if (newRotationState == RotationState.Front || newRotationState == RotationState.Back
            || _sideClickableCollider == null)
        {

            _sideClickableCollider.gameObject.SetActive(false);
            _frontClickableCollider.gameObject.SetActive(true);
            return _frontClickableCollider;
        }
        else
        {
            _frontClickableCollider.gameObject.SetActive(false);
            _sideClickableCollider.gameObject.SetActive(true);
            return _sideClickableCollider;
        }
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
    private void OnDisable()
    {
        _decor.Rotated -= OnRotate;
    }
}