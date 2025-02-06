using System;
//using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Decor : MonoBehaviour, IInventoryObject
{
    [SerializeField] protected SpriteRenderer _mainRenderer;
    [SerializeField] protected InputActionReference _clickAction;
    [SerializeField] protected InputActionReference _cancelAction;
    [SerializeField] protected InputActionReference rotationAction;
    [SerializeField] protected DecorPolygonSplitter _frontPolygonSplitter;
    [SerializeField] protected DecorPolygonSplitter _leftPolygonSplitter;
    [SerializeField] protected Transform _impassableZone;
    [SerializeField] protected float _primuscus = 0.3f;
    [Space]
    [SerializeField] protected Sprite _frontSprite;
    [SerializeField] protected Sprite _leftSprite;
    [SerializeField] protected Sprite _backSprite;
    [SerializeField] protected Sprite _rightSprite;
    [Space]
    [SerializeField] protected Sprite _icon;
    [Space]
    [SerializeField] protected GameObject _warningSign;


    protected PersistantStaticData _staticData;
    protected DecorationSystem _decorationSystem;
    protected SpaceDeterminantor _spaceDeterminantor;
    protected DecorHolder _decorHolder;
    protected IAssets _assets;
    //protected Camera _mainCamera;
    protected bool _isPlacing;
    protected bool _canBuild;
    protected BaceCell _closestCell;
    protected DecorData _decorData;
    protected RotationState _rotationState;
    protected bool _isOnDecorState;
    protected DecorPolygonSplitter _currentPolygonSplitter;

    public event Action PlacedAction;
    public event Action<BaceCell> OnOccupyCell;
    public event Action OnEmptyCell;

    [Space]
    [Header("Новые параметры")]
    [SerializeField] private Collider2D _decorCollider;
    [SerializeField] private SpriteRenderer _decorRenderer;
    //[SerializeField] private PolygonCollider2D _floorCollider;
    [SerializeField] private Color _invalidColor = Color.red;
    [SerializeField] private Color _validColor = Color.green;

    private Color _originalColor;
    private Camera _mainCamera;
    private bool _isDragging = false;
    private bool _isInside = true;
    //private Collider2D _decorCollider;

    public void Initialize(PersistantStaticData staticData, DecorationSystem decorationSystem,
        SpaceDeterminantor spaceDeterminantor, DecorHolder decorHolder, IAssets assets)
    {
        _staticData = staticData;
        _decorationSystem = decorationSystem;
        _spaceDeterminantor = spaceDeterminantor;
        _decorHolder = decorHolder;
        _assets = assets;

        _rotationState = RotationState.Front;
        _isDragging = true;
        //UpdatePolygonSplitter();
        //_isPlacing = true;
        //_decorData = new DecorData(this);
        _originalColor = _decorRenderer.color;

        _clickAction.action.performed += OnClick;
        //_cancelAction.action.performed += OnCancel;
        //rotationAction.action.performed += OnRotate;

        _warningSign.SetActive(false);
        //UpdateSprite(_rotationState);
        //_currentPolygonSplitter.Initialize(assets, staticData);
        //if (_leftPolygonSplitter != null)
        //{
        //    _leftPolygonSplitter.Initialize(assets, staticData);
        //}
        //_frontPolygonSplitter.Initialize(assets, staticData);

        CheckCamera();
    }

    private void Update()
    {
        if (!CheckCamera())
        {
            Debug.Log("Waiting for the camera...");
            return;
        }

        if (_isDragging)
        {
            DragObjeect();
            UpdateView();
        }
    }

    private void UpdateView()
    {
        if (_isInside)
            _decorRenderer.color = _validColor;
        else
            _decorRenderer.color = _invalidColor;
    }

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

    private void DragObjeect()
    {
        //Vector2 mousePosition = _mainCamera.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        //transform.position = new Vector3(mousePosition.x, mousePosition.y, transform.position.z);


        Vector3 mousePosition = Mouse.current.position.ReadValue();
        Ray ray = _mainCamera.ScreenPointToRay(mousePosition);
        Plane xyPlane = new Plane(Vector3.forward, Vector3.zero); // Плоскость XY (Z=0)

        if (xyPlane.Raycast(ray, out float distance))
        {
            Vector3 worldPosition = ray.GetPoint(distance);
            transform.position = new Vector3(worldPosition.x, worldPosition.y, transform.position.z);
        }


        CheckPlacement();
    }

    private void CheckPlacement()
    {
        _isInside = false;

        foreach (var floor in _spaceDeterminantor.FloorObjects)
        {
            bool allPointsInside = true;

            foreach (Vector2 point in _decorCollider.bounds.GetCorners())
            {
                if (!floor.PolygonCollider.OverlapPoint(point))
                {
                    allPointsInside = false;
                    break;
                }
            }

            if (allPointsInside)
            {
                _isInside = true;
                break;
            }
        }


        //_isInside = true;

        //foreach (var floor in _spaceDeterminantor.FloorObjects)
        //{
        //    foreach (Vector2 point in _decorCollider.bounds.GetCorners())
        //    {
        //        if (!floor.PolygonCollider.OverlapPoint(point))
        //        {
        //            Debug.Log(floor.name);

        //            _isInside = false;
        //            break;
        //        }
        //    }
        //    _isInside = true;
        //    break;
        //}
    }

    private void OnClick(InputAction.CallbackContext context)
    {
        if (_isDragging && _isInside)
        {
            PlaceObject();
        }
        else if (IsMouseOnObject())
        {
            _isDragging = true;
        }
    }

    private bool IsMouseOnObject()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        RaycastHit2D[] hits = Physics2D.RaycastAll(mousePosition, Vector2.zero);

        foreach (var hit in hits)
        {
            if (hit.collider != null && hit.collider == _decorCollider || hit.collider.transform.IsChildOf(transform))
            {
                return true;
            }
        }
        return false;
    }

    private void PlaceObject()
    {
        _isDragging = false;
        _decorRenderer.color = _originalColor;
    }

    //protected void UpdatePolygonSplitter()
    //{
    //    switch (_rotationState)
    //    {
    //        case RotationState.Front:
    //            _currentPolygonSplitter = _frontPolygonSplitter;
    //            break;
    //        case RotationState.Left:
    //            if (_leftPolygonSplitter == null)
    //                _currentPolygonSplitter = _frontPolygonSplitter;
    //            else _currentPolygonSplitter = _leftPolygonSplitter;
    //            break;
    //        case RotationState.Back:
    //            _currentPolygonSplitter = _frontPolygonSplitter;
    //            break;
    //        case RotationState.Right:
    //            if (_leftPolygonSplitter == null)
    //                _currentPolygonSplitter = _frontPolygonSplitter;
    //            else _currentPolygonSplitter = _leftPolygonSplitter;
    //            break;
    //    }
    //}

    public void SetIsOnDecorState(bool isOnDecorState) =>
        _isOnDecorState = isOnDecorState;

    public Sprite GetIcon()
        => _icon;

    //protected virtual void OnRotate(InputAction.CallbackContext context)
    //{
    //    if (!_isPlacing || !_isOnDecorState) return;
    //    //..
    //    _rotationState = (RotationState)(((int)_rotationState + 1) % 4);
    //    UpdateSprite(_rotationState);
    //    _impassableZone.rotation *= Quaternion.Euler(0, 0, 90);
    //    UpdatePolygonSplitter();
    //    //_polygonSplitter.RemoveCells();
    //    //_polygonSplitter.Initialize(_assets, _staticData);
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
        _isPlacing = false;
        if (_leftPolygonSplitter != null)
            _leftPolygonSplitter.RemoveCells();

        _frontPolygonSplitter.RemoveCells();
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

    //protected void FixedUpdate()
    //{
    //    if (!_isPlacing || !_isOnDecorState) return;

    //    FollowMouseWithSnap();
    //    CheckIfCanBuild();
    //    UpdateColor();
    //}

    //protected void FollowMouseWithSnap()
    //{
    //    CheckCamera();
    //    _closestCell = GetClosestGridCell(FindCursorPosition());
    //    if (_closestCell != null)
    //        transform.position = new Vector3(_closestCell.CenterX, _closestCell.CenterY, 0f);
    //}

    //protected void CheckIfCanBuild()
    //{
    //    _canBuild = true;

    //    if (_currentPolygonSplitter == null) return;

    //    foreach (var potentialCell in _currentPolygonSplitter.PotentiallyOccupiedCells)
    //    {
    //        bool foundMatchingCell = false;

    //        foreach (var gridHolder in _spaceDeterminantor.GreedHolders)
    //        {
    //            foreach (var cell in gridHolder.Grid)
    //            {
    //                Vector3 worldPosition = transform.TransformPoint(new Vector3(potentialCell.CenterX, potentialCell.CenterY, 0));
    //                float tolerance = _staticData.CellSize * _primuscus;

    //                if (Mathf.Abs(cell.CenterX - worldPosition.x) <= tolerance &&
    //                    Mathf.Abs(cell.CenterY - worldPosition.y) <= tolerance)
    //                {
    //                    foundMatchingCell = true;
    //                    if (cell.IsOccupied)
    //                    {
    //                        _canBuild = false;
    //                        return;
    //                    }
    //                }
    //            }
    //        }

    //        if (!foundMatchingCell)
    //        {
    //            _canBuild = false;
    //            return;
    //        }
    //    }

    //    //foreach (var potentialCell in _polygonSplitter.PotentiallyOccupiedCells)
    //    //{
    //    //    bool isOccupiedOrOutOfBounds = true;

    //    //    foreach (var gridHolder in _spaceDeterminantor.GreedHolders)
    //    //    {
    //    //        foreach (var cell in gridHolder.Grid)
    //    //        {
    //    //            Vector3 worldPosition = transform.TransformPoint(new Vector3(potentialCell.CenterX, potentialCell.CenterY, 0));

    //    //            float tolerance = _staticData.CellSize / 2;

    //    //            if (Mathf.Abs(cell.CenterX - worldPosition.x) <= tolerance &&
    //    //                Mathf.Abs(cell.CenterY - worldPosition.y) <= tolerance)
    //    //            {
    //    //                if (cell.IsOccupied)
    //    //                {
    //    //                    isOccupiedOrOutOfBounds = true;
    //    //                    break;
    //    //                }
    //    //                else
    //    //                {
    //    //                    isOccupiedOrOutOfBounds = false;
    //    //                }
    //    //            }
    //    //        }

    //    //        if (!isOccupiedOrOutOfBounds)
    //    //        {
    //    //            break;
    //    //        }
    //    //    }

    //    //    if (isOccupiedOrOutOfBounds)
    //    //    {
    //    //        _canBuild = false;
    //    //        break;
    //    //    }
    //    //}

    //}

    //protected void UpdateColor()
    //{
    //    _mainRenderer.material.color = _canBuild ? _staticData.AllowedPositionColor : _staticData.BannedPositionColor;
    //}

    //protected BaceCell GetClosestGridCell(Vector3 position)
    //{
    //    float minDistance = float.MaxValue;
    //    BaceCell bestCell = null;

    //    foreach (var gridHolder in _spaceDeterminantor.GreedHolders)
    //    {
    //        foreach (var cell in gridHolder.Grid)
    //        {
    //            float distance = Vector2.Distance(new Vector2(cell.CenterX, cell.CenterY), position);
    //            if (distance < minDistance)
    //            {
    //                minDistance = distance;
    //                bestCell = cell;
    //            }
    //        }
    //    }
    //    return bestCell;
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

    //protected void MarkOccupiedCells()
    //{
    //    //if (_currentPolygonSplitter == null || _spaceDeterminantor == null) return;
    //    // Debug.Log(_currentPolygonSplitter.name);
    //    float tolerance = _staticData.CellSize * _primuscus;

    //    Quaternion rotation = _currentPolygonSplitter.transform.rotation;
    //    Vector3 positionOffset = _currentPolygonSplitter.transform.position;

    //    foreach (var gridHolder in _spaceDeterminantor.GreedHolders)
    //    {
    //        foreach (var cell in gridHolder.Grid)
    //        {
    //            foreach (var potentialCell in _currentPolygonSplitter.PotentiallyOccupiedCells)
    //            {
    //                Vector3 localPosition = new Vector3(potentialCell.CenterX, potentialCell.CenterY, 0);
    //                Vector3 rotatedPosition = rotation * localPosition;
    //                Vector3 worldPosition = rotatedPosition + positionOffset;

    //                if (Mathf.Abs(cell.CenterX - worldPosition.x) <= tolerance &&
    //                    Mathf.Abs(cell.CenterY - worldPosition.y) <= tolerance)
    //                {
    //                    cell.OccupyCell();
    //                    OnOccupyCell?.Invoke(cell);
    //                }
    //            }
    //        }
    //    }
    //}

    //protected void ToggleColliders(bool state)
    //{
    //    //foreach (var collider in _colliders)
    //    //    collider.enabled = state;
    //}

    //protected void CheckCamera()
    //{
    //    if (_mainCamera == null)
    //        _mainCamera = Camera.main;
    //}

    protected void OnDisable()
    {
        _isPlacing = false;
        _clickAction.action.performed -= OnClick;
        //_cancelAction.action.performed -= OnCancel;
        //rotationAction.action.performed -= OnRotate;
    }
}
