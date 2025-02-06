using System;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(DecorView),(typeof(DecorRotator)))]
[RequireComponent (typeof(DecorDrag),(typeof(DecorPlacer)))]
public class Decor : MonoBehaviour, IInventoryObject
{
    [SerializeField] private DecorView _decorView;
    [SerializeField] private DecorDrag _decorDrag;
    [SerializeField] private DecorPlacer _decorPlacer;
    [SerializeField] private DecorRotator _decorRotator;
    [Space]
    [SerializeField] protected InputActionReference _clickAction;
    [SerializeField] protected InputActionReference _cancelAction;
    [SerializeField] protected InputActionReference _rotationAction;
    [Space]
    [SerializeField] protected Sprite _icon;
    [Space]
    [SerializeField] protected GameObject _warningSign;

    protected DecorData _decorData;

    protected DecorationSystem _decorationSystem;
    private bool _canPlace = true;
    protected bool _isOnDecorState;
    private RotationState _currentRotationState;

    public bool IsInside { get; private set; }
    public bool IsDragging { get; private set; }
    public Collider2D CurrentDecorCollider { get; private set; }
    public Camera MainCamera { get; private set; }
    public Collider2D CurrentClickableCollider { get; private set; }

    public event Action DecorPlacedAction;
    public event Action Clicked;
    public event Action<RotationState> Rotated;
    public event Action<RotationState> EndRotation;

    public void Initialize(PersistantStaticData staticData, DecorationSystem decorationSystem,
        SpaceDeterminantor spaceDeterminantor)
    {
        IsInside = true;
        IsDragging = false;

        _decorationSystem = decorationSystem;

        _currentRotationState = RotationState.Front;
        IsDragging = true;

        _warningSign.SetActive(false);

        _decorView.Initialize(this, staticData, _currentRotationState);
        _decorDrag.Initialize( this, staticData, spaceDeterminantor);
        _decorPlacer.Initialize(this);
        _decorRotator.Initialize(this, _currentRotationState);
        CheckCamera();

        _clickAction.action.performed += OnClick;
        _rotationAction.action.performed += OnRotate;
        _cancelAction.action.performed += OnCancel;
    }


    private void OnCancel(InputAction.CallbackContext context)
    {
        //TODO
    }

    private void FixedUpdate()
    {
        if (!CheckCamera()) return;
    }

    public Sprite GetIcon()
        => _icon;

    public void SetIsOnDecorState(bool isOnDecorState) => 
        _isOnDecorState = isOnDecorState;

    public void SetIsInside(bool isInside)
        => IsInside = isInside;

    public void SetCurrentClickableCollider(Collider2D collider2D) =>
        CurrentClickableCollider = collider2D;

    public void TakeDecorIfCan()
    {
        if (_decorationSystem.ActivateDecorIfCan(this))
            IsDragging = true;
    }

    private void OnClick(InputAction.CallbackContext context)
    {
        if (!_canPlace || !_isOnDecorState) return;

        Clicked?.Invoke();
    }

    public void BanActions() =>
        _canPlace = false;

    public void AllowActions() =>
         _canPlace = true;

    public void PlaceObject()
    {
        IsDragging = false;
        _decorationSystem.InstanriateDecor(this);
        DecorPlacedAction?.Invoke();
    }

    private void OnRotate(InputAction.CallbackContext context)
    {
        if (!IsDragging || !_isOnDecorState) return;

        Rotated?.Invoke(_currentRotationState);
    }

    public void SetRotationState(RotationState rotationState)
    {
        _currentRotationState = rotationState;
        EndRotation?.Invoke(_currentRotationState);
    }

    public void SetCurrentDecorCollider(Collider2D collider)
        => CurrentDecorCollider = collider;

    //private void UpdateColliders()
    //{
    //    switch (CurrentRotationState)
    //    {
    //        case RotationState.Front:
    //            CurrentDecorCollider = _frontDecorCollider;
    //            break;
    //        case RotationState.Left:
    //            if (_sideDecorCollider == null)
    //                CurrentDecorCollider = _frontDecorCollider;
    //            else CurrentDecorCollider = _sideDecorCollider;
    //            break;
    //        case RotationState.Back:
    //            CurrentDecorCollider = _frontDecorCollider;
    //            break;
    //        case RotationState.Right:
    //            if (_sideDecorCollider == null)
    //                CurrentDecorCollider = _frontDecorCollider;
    //            else CurrentDecorCollider = _sideDecorCollider;
    //            break;
    //    }
    //}

    //protected void UpdateSprite(RotationState state)
    //{
    //    switch (state)
    //    {
    //        case RotationState.Front:
    //            _mainRenderer.sprite = _frontSprite;
    //            _mainRenderer.flipX = false;
    //            break;
    //        case RotationState.Left:
    //            _mainRenderer.sprite = CheckingSpriteExistence(_leftSprite, _rightSprite);
    //            break;
    //        case RotationState.Back:
    //            _mainRenderer.sprite = _backSprite;
    //            _mainRenderer.flipX = false;
    //            break;
    //        case RotationState.Right:
    //            _mainRenderer.sprite = CheckingSpriteExistence(_rightSprite, _leftSprite);
    //            break;
    //    }
    //}

    //protected Sprite CheckingSpriteExistence(Sprite requiredSprite, Sprite replacementSprite)
    //{
    //    if (requiredSprite == null)
    //    {
    //        _mainRenderer.flipX = true;
    //        return replacementSprite;
    //    }
    //    else
    //    {
    //        _mainRenderer.flipX = false;
    //        return requiredSprite;
    //    }
    //}
    private bool CheckCamera()
    {
        if (MainCamera != null)
        {
            return true;
        }
        else
        {
            if (Camera.main == null)
            {
                return false;
            }
            else
            {
                MainCamera = Camera.main;
                return true;
            }
        }
    }

    //protected void OnCancel(InputAction.CallbackContext context)
    //{
    //    if (_isPlacing)
    //    {
    //        if (_decorationSystem.CanPlace)
    //            _decorationSystem.TryToRemoveDecor(this);

    //    }
    //}

    //protected IEnumerator HideTAblet()
    //{
    //    yield return new WaitForSeconds(3);
    //    _warningSign.SetActive(false);
    //}

    public void RemoveThisDecor()
    {
        IsDragging = false;
        //if (_leftPolygonSplitter != null)
        //    _leftPolygonSplitter.RemoveCells();

        //_frontPolygonSplitter.RemoveCells();
        //_impassableZone.transform.rotation = Quaternion.Euler(0, 0, 0);
    }

    //protected void OnClick(InputAction.CallbackContext context)
    //{
    //    if (!_isOnDecorState) return;



    //    if (_isPlacing)
    //    {
    //        if (_canBuild)
    //            PlaceObject();
    //    }
    //    else if (_decorationSystem.ActiveDecor == null)
    //    {
    //        CheckCamera();
    //        Ray ray = _mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
    //        RaycastHit2D[] hits = Physics2D.GetRayIntersectionAll(ray);

    //        foreach (var hit in hits)
    //        {
    //            if ((hit.collider != null && hit.collider.GetComponentInParent<Decor>() == this))
    //            {
    //                OnEmptyCell?.Invoke();
    //                _decorationSystem.ReActivateDecor(this);
    //                _isPlacing = true;
    //                ToggleColliders(false);

    //                if (_leftPolygonSplitter != null)
    //                {
    //                    foreach (DecorsCell cell in _leftPolygonSplitter.PotentiallyOccupiedCells)
    //                    {
    //                        cell.ShowCell();
    //                    }
    //                }
    //                foreach (DecorsCell cell in _frontPolygonSplitter.PotentiallyOccupiedCells)
    //                {
    //                    cell.ShowCell();
    //                }
    //                break;
    //            }
    //        }
    //    }
    //}




    //protected Vector3 FindCursorPosition()
    //{
    //    Vector2 screenPos = Mouse.current.position.ReadValue();
    //    return _mainCamera.ScreenToWorldPoint(new Vector3(screenPos.x, screenPos.y, -_mainCamera.transform.position.z));
    //}

    //protected void PlaceObject()
    //{
    //    if (!_decorationSystem.CanPlace)
    //    {
    //        StopAllCoroutines();
    //        _warningSign.SetActive(true);
    //        StartCoroutine(HideTAblet());
    //        return;
    //    }


    //    _isPlacing = false;
    //    _mainRenderer.material.color = _staticData.NormalColor;
    //    ToggleColliders(true);
    //    UpdatePolygonSplitter();
    //    _decorHolder.InstallDecor(this);

    //    MarkOccupiedCells();
    //    if (_leftPolygonSplitter != null)
    //    {
    //        foreach (DecorsCell cell in _leftPolygonSplitter.PotentiallyOccupiedCells)
    //        {
    //            cell.HideCell();
    //        }
    //    }
    //    foreach (DecorsCell cell in _frontPolygonSplitter.PotentiallyOccupiedCells)
    //    {
    //        cell.HideCell();
    //    }

    //    //foreach (DecorsCell cell in _currentPolygonSplitter.PotentiallyOccupiedCells)
    //    //{
    //    //    cell.HideCell();
    //    //}

    //    PlacedAction?.Invoke();
    //}

    protected void OnDisable()
    {
        IsDragging = false;
        _clickAction.action.performed -= OnClick;
        //_cancelAction.action.performed -= OnCancel;
        _rotationAction.action.performed -= OnRotate;
    }

}
