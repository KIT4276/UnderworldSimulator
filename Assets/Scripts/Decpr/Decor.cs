using System;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Collider2D))]
public class Decor : MonoBehaviour
{
    [SerializeField] private Collider2D[] _colliders;
    [SerializeField] private SpriteRenderer _mainRenderer;
    [SerializeField] private InputActionReference _clickAction;
    [SerializeField] private InputActionReference _cancelAction;
    [SerializeField] private InputActionReference rotationAction;
    [SerializeField] private DecorPolygonSplitter _polygonSplitter;
    [Space]
    [SerializeField] private Sprite _frontSprite;
    [SerializeField] private Sprite _leftSprite;
    [SerializeField] private Sprite _backSprite;
    [SerializeField] private Sprite _rightSprite;

    private PersistantStaticData _staticData;
    private DecorationSystem _decorationSystem;
    private SpaceDeterminantor _spaceDeterminantor;
    private DecorHolder _decorHolder;
    private Camera _mainCamera;
    private bool _isPlacing;
    private bool _canBuild;
    private GridCell _closestCell;
    private DecorData _decorData;
    private RotationState _rotationState;

    public event Action PlacedAction;
    public event Action CanceledAction;
    public event Action<GridCell> OnOccupyCell;
    public event Action OnEmptyCell;

    public void Initialize(PersistantStaticData staticData, DecorationSystem decorationSystem,
        SpaceDeterminantor spaceDeterminantor, DecorHolder decorHolder, IAssets assets)
    {
        this._staticData = staticData;
        this._decorationSystem = decorationSystem;
        this._spaceDeterminantor = spaceDeterminantor;
        this._decorHolder = decorHolder;

        _rotationState = RotationState.Front;
        _isPlacing = true;
        _decorData = new DecorData(this);

        _clickAction.action.performed += OnClick;
        _cancelAction.action.performed += OnCancel;
        rotationAction.action.performed += OnRotate;

        foreach (var collider in _colliders)
            collider.enabled = false;

        _polygonSplitter.Initialize(assets, staticData);
    }

    private void OnRotate(InputAction.CallbackContext context)
    {
        if (!_isPlacing) return;

        _rotationState = (RotationState)(((int)_rotationState + 1) % 4);
        UpdateSprite(_rotationState);
    }

    private void UpdateSprite(RotationState state)
    {
        switch (state)
        {
            case RotationState.Front:
                _mainRenderer.sprite = _frontSprite ?? _rightSprite;
                _mainRenderer.flipX = false;
                break;
            case RotationState.Left:
                _mainRenderer.sprite = _leftSprite ?? _rightSprite;
                _mainRenderer.flipX = _leftSprite == null;
                break;
            case RotationState.Back:
                _mainRenderer.sprite = _backSprite ?? _frontSprite;
                _mainRenderer.flipX = false;
                break;
            case RotationState.Right:
                _mainRenderer.sprite = _rightSprite ?? _leftSprite;
                _mainRenderer.flipX = _rightSprite == null;
                break;
        }
    }

    private void OnCancel(InputAction.CallbackContext context)
    {
        _isPlacing = false;
        CanceledAction?.Invoke();
    }

    private void OnClick(InputAction.CallbackContext context)
    {
        if (_isPlacing)
        {
            if (_canBuild)
                PlaceObject();
        }
        else if (_decorationSystem.ActiveDecor == null)
        {
            CheckCamera();
            Ray ray = _mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
            RaycastHit2D[] hits = Physics2D.GetRayIntersectionAll(ray);

            foreach (var hit in hits)
            {
                if (hit.collider != null && hit.collider.gameObject == gameObject)
                {
                    OnEmptyCell?.Invoke();
                    _decorationSystem.ReActivateDecor(this);
                    _isPlacing = true;
                    ToggleColliders(false);
                    break;
                }
            }
        }
    }

    private void FixedUpdate()
    {
        if (!_isPlacing) return;

        FollowMouseWithSnap();
        CheckIfCanBuild();
        UpdateColor();
    }

    private void FollowMouseWithSnap()
    {
        CheckCamera();
        _closestCell = GetClosestGridCell(FindCursorPosition());
        if (_closestCell != null)
            transform.position = new Vector3(_closestCell.CenterX, _closestCell.CenterY, 0f);
    }

    private void CheckIfCanBuild()
    {
        _canBuild = _closestCell != null && !_closestCell.IsOccupied;
    }

    private void UpdateColor()
    {
        _mainRenderer.material.color = _canBuild ? _staticData.AllowedPositionColor : _staticData.BannedPositionColor;
    }

    private GridCell GetClosestGridCell(Vector3 position)
    {
        float minDistance = float.MaxValue;
        GridCell bestCell = null;

        foreach (var gridHolder in _spaceDeterminantor.GreedHolders)
        {
            foreach (var cell in gridHolder.Grid)
            {
                float distance = Vector2.Distance(new Vector2(cell.CenterX, cell.CenterY), position);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    bestCell = cell;
                }
            }
        }
        return bestCell;
    }

    private Vector3 FindCursorPosition()
    {
        Vector2 screenPos = Mouse.current.position.ReadValue();
        return _mainCamera.ScreenToWorldPoint(new Vector3(screenPos.x, screenPos.y, -_mainCamera.transform.position.z));
    }

    private void PlaceObject()
    {
        _isPlacing = false;
        _mainRenderer.material.color = _staticData.NormalColor;
        ToggleColliders(true);
        _decorHolder.InstallDecor(this);
        OnOccupyCell?.Invoke(_closestCell);
        PlacedAction?.Invoke();
    }

    private void ToggleColliders(bool state)
    {
        foreach (var collider in _colliders)
            collider.enabled = state;
    }

    private void CheckCamera()
    {
        if (_mainCamera == null)
            _mainCamera = Camera.main;
    }

    private void OnDisable()
    {
        _isPlacing = false;
        _clickAction.action.performed -= OnClick;
        _cancelAction.action.performed -= OnCancel;
        rotationAction.action.performed -= OnRotate;
    }
}
