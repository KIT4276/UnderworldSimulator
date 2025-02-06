using System;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(DecorView))]
public class Decor : MonoBehaviour, IInventoryObject
{
    [SerializeField] private DecorView _decorView;

    [SerializeField] private Collider2D _frontDecorCollider;
    [SerializeField] private Collider2D _sideDecorCollider;
    [Space]
    [SerializeField] protected InputActionReference _clickAction;
    [SerializeField] protected InputActionReference _cancelAction;
    [SerializeField] protected InputActionReference _rotationAction;
    [Space]
    [SerializeField] protected Transform _impassableZone;
    [Space]
    [SerializeField] protected Sprite _icon;

    //[SerializeField] private SpriteRenderer _mainRenderer;
    //[Space]
    //[SerializeField] protected Sprite _frontSprite;
    //[SerializeField] protected Sprite _leftSprite;
    //[SerializeField] protected Sprite _backSprite;
    //[SerializeField] protected Sprite _rightSprite;
    //[Space]
    //[SerializeField] protected Sprite _icon;
    [Space]
    [SerializeField] protected GameObject _warningSign;

    //protected PersistantStaticData _staticData;
    protected DecorationSystem _decorationSystem;
    protected SpaceDeterminantor _spaceDeterminantor;
    protected IAssets _assets;
    protected DecorData _decorData;
    protected bool _isOnDecorState;

    [Header("Новые параметры")]

    private Collider2D _currentDecorCollider;
    //private Color _originalColor;
    private Camera _mainCamera;
    private bool _canPlace = true;

    public RotationState RotationState { get; private set; }
    public bool IsInside { get; private set; }
    public bool IsDragging { get; private set; }

    public event Action DecorPlacedAction;
    public event Action DecorRotated;

    public void Initialize(PersistantStaticData staticData, DecorationSystem decorationSystem,
        SpaceDeterminantor spaceDeterminantor, IAssets assets)
    {
        //_decorData = new DecorData(this);

        IsInside = true;
        IsDragging = false;

        _decorationSystem = decorationSystem;
        _spaceDeterminantor = spaceDeterminantor;
        _assets = assets;

        RotationState = RotationState.Front;
        IsDragging = true;
        UpdateColliders();
        //_originalColor = _mainRenderer.color;
        _currentDecorCollider = _frontDecorCollider;
        _clickAction.action.performed += OnClick;
        _rotationAction.action.performed += OnRotate;
        _cancelAction.action.performed += OnCancel;

        _warningSign.SetActive(false);
        //UpdateSprite(_rotationState);

        _decorView.Initialize(staticData);
        CheckCamera();
    }


    public Sprite GetIcon()
        => _icon;

    private void OnCancel(InputAction.CallbackContext context)
    {
        //TODO
    }

    private void FixedUpdate()
    {
        if (!CheckCamera())
        {
            Debug.Log("Waiting for the camera...");
            return;
        }

        if (IsDragging)
        {
            DragObjeect();
            //UpdateView();
        }
    }

    //private void UpdateView()
    //{
    //    if (_isInside)
    //        _mainRenderer.color = _staticData.AllowedPositionColor;
    //    else
    //        _mainRenderer.color = _staticData.BannedPositionColor;
    //}


    private void DragObjeect()
    {
        Vector3 mousePosition = Mouse.current.position.ReadValue();
        Ray ray = _mainCamera.ScreenPointToRay(mousePosition);
        Plane xyPlane = new Plane(Vector3.forward, Vector3.zero);

        if (xyPlane.Raycast(ray, out float distance))
        {
            Vector3 worldPosition = ray.GetPoint(distance);
            transform.position = new Vector3(worldPosition.x, worldPosition.y, transform.position.z);
        }

        CheckPlacement();
    }

    private void CheckPlacement()
    {
        IsInside = false;

        foreach (var floor in _spaceDeterminantor.FloorMarkers)
        {
            bool allPointsInside = true;

            foreach (Vector2 point in _currentDecorCollider.bounds.GetCorners())
            {
                if (!floor.Collider.OverlapPoint(point))
                {
                    allPointsInside = false;
                    break;
                }
            }

            if (allPointsInside)
            {
                IsInside = true;
                break;
            }
        }
    }

    private void OnClick(InputAction.CallbackContext context)
    {
        if (!_canPlace || !_isOnDecorState) return;

        if (IsDragging)
        {
            if (IsInside)
                PlaceObject();
        }
        else if (IsMouseOnObject())
        {
            if (_decorationSystem.ActivateDecorIfCan(this))
                IsDragging = true;
        }
    }

    public void BanActions() =>
        _canPlace = false;

    public void AllowActions() =>
         _canPlace = true;

    private bool IsMouseOnObject()
    {
        Vector2 mouseScreenPos = Mouse.current.position.ReadValue();
        Ray ray = _mainCamera.ScreenPointToRay(mouseScreenPos);
        RaycastHit2D[] hits = Physics2D.GetRayIntersectionAll(ray, Mathf.Infinity, LayerMask.GetMask("Decor"));

        foreach (var hit in hits)
        {
            if (hit.collider == _currentDecorCollider || hit.collider.transform.IsChildOf(transform))
            {
                return true;
            }
        }
        return false;
    }

    private void PlaceObject()
    {
        IsDragging = false;
        //_mainRenderer.color = _originalColor;
        _decorationSystem.InstanriateDecor(this);
        DecorPlacedAction?.Invoke();
    }

    public void SetIsOnDecorState(bool isOnDecorState)
    {
        _isOnDecorState = isOnDecorState;
    }

    

    protected virtual void OnRotate(InputAction.CallbackContext context)
    {
        if (!IsDragging || !_isOnDecorState) return;

        RotationState = (RotationState)(((int)RotationState + 1) % 4);
        DecorRotated?.Invoke();
        //UpdateSprite(RotationState);
        _impassableZone.rotation *= Quaternion.Euler(0, 0, 90);
        UpdateColliders();
    }

    private void UpdateColliders()
    {
        switch (RotationState)
        {
            case RotationState.Front:
                _currentDecorCollider = _frontDecorCollider;
                break;
            case RotationState.Left:
                if (_sideDecorCollider == null)
                    _currentDecorCollider = _frontDecorCollider;
                else _currentDecorCollider = _sideDecorCollider;
                break;
            case RotationState.Back:
                _currentDecorCollider = _frontDecorCollider;
                break;
            case RotationState.Right:
                if (_sideDecorCollider == null)
                    _currentDecorCollider = _frontDecorCollider;
                else _currentDecorCollider = _sideDecorCollider;
                break;
        }
    }

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
        if (_mainCamera != null)
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
                _mainCamera = Camera.main;
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
        _impassableZone.transform.rotation = Quaternion.Euler(0, 0, 0);
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
