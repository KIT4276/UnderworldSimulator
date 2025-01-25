using System;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Collider2D))]
public class Decor : MonoBehaviour
{
    [SerializeField] private Collider2D[] _colliders;
    [SerializeField] private SpriteRenderer _mainRenderer;
    [SerializeField] private Vector2 _occupiedCells;
    [SerializeField] private InputActionReference _clickAcrion;
    [SerializeField] private InputActionReference _cancelAcrion;
    [SerializeField] private InputActionReference _rotationAction;
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
    public event Action CanceledAcrion;
    public event Action<GridCell> OnOccupyCell;
    public event Action OnEmptyCell;

    public void Initialize(PersistantStaticData staticData, DecorationSystem decorationSystem
        , SpaceDeterminantor spaceDeterminantor, DecorHolder decorHolder)
    {
        _rotationState = RotationState.Front;
        _isPlacing = true;
        _staticData = staticData;
        _decorationSystem = decorationSystem;
        _spaceDeterminantor = spaceDeterminantor;
        _decorHolder = decorHolder;

        _clickAcrion.action.performed += OnClick;
        _cancelAcrion.action.performed += OnCanceled;
        _rotationAction.action.performed += OnRotate;

        foreach (var collider in _colliders)
            collider.enabled = false;

        _decorData = new(this);
    }

    private void OnRotate(InputAction.CallbackContext context)
    {
        if(!_isPlacing)  return;

        switch (_rotationState)
        {
            case RotationState.Front:
                _mainRenderer.sprite = ReplacingNullIPictures(_leftSprite, _rightSprite);
                _rotationState = RotationState.Left;
                break;
            case RotationState.Left:
                _mainRenderer.sprite = ReplacingNullIPictures(_backSprite, _frontSprite);
                _rotationState = RotationState.Back;
                break;
            case RotationState.Back:
                _mainRenderer.sprite = ReplacingNullIPictures(_rightSprite, _leftSprite);
                _rotationState = RotationState.Right;
                break;
            case RotationState.Right:
                _mainRenderer.sprite = ReplacingNullIPictures(_frontSprite, _backSprite);
                _rotationState = RotationState.Front;
                break;
        }
    }

    private Sprite ReplacingNullIPictures(Sprite neededSprite, Sprite replaceSprite)
    {
        if (neededSprite == null)
        {
            _mainRenderer.flipX = true;
            return replaceSprite;
        }
        else
        {
            _mainRenderer.flipX = false;
            return neededSprite;
        }
    }

    private void OnCanceled(InputAction.CallbackContext context)
    {
        _isPlacing = false;
        CanceledAcrion?.Invoke();
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
                //Debug.Log(hit.collider.name);
                if (hit.collider.gameObject == this.gameObject)
                {
                    //Debug.Log("DA");
                    OnEmptyCell?.Invoke();
                    _decorationSystem.ReActivateDecor(this);
                    _isPlacing = true;

                    foreach (var collider in _colliders)
                        collider.enabled = false;
                    break;
                }
            }
        }
    }

    private void FixedUpdate()
    {
        if (_isPlacing)
        {
            FollowMouseWithSnap();
            CheckIfCanBuild();
            UpdateColor();
        }
    }

    private void FollowMouseWithSnap()
    {
        CheckCamera();

        Vector3 mouseWorldPosition = FindCursorPosition();

        GridCell closestCell = GetClosestGridCell(mouseWorldPosition);

        if (closestCell != null)
        {
            _closestCell = closestCell;

            transform.position = new Vector3(closestCell.CenterX, closestCell.CenterY, 0f);
        }
    }


    private void CheckIfCanBuild()
    {
        _canBuild = true;

        GridCell closestCell = GetClosestGridCell(transform.position);

        if (closestCell == null || closestCell.IsOccupied)
        {
            _canBuild = false;
        }
        else
        {
            Vector2 offset = new Vector2(
                -(_occupiedCells.x / 2f) * _staticData.CellSize + _staticData.CellSize / 2f,
                -(_occupiedCells.y / 2f) * _staticData.CellSize + _staticData.CellSize / 2f
            );

            for (int x = 0; x < _occupiedCells.x; x++)
            {
                for (int y = 0; y < _occupiedCells.y; y++)
                {
                    float checkX = closestCell.CenterX + offset.x + x * _staticData.CellSize;
                    float checkY = closestCell.CenterY + offset.y + y * _staticData.CellSize;

                    //Debug.Log(IsPositionInGrid(checkX, checkY));
                    //Instantiate(_tempPrefab, new Vector3(checkX, checkY, 0f), Quaternion.identity);// for tests

                    if (!IsPositionInGrid(checkX, checkY) || GetGridCellAt(checkX, checkY).IsOccupied)
                    {
                        // Debug.Log(GetGridCellAt(checkX, checkY).IsOccupied);
                        _canBuild = false;
                        break;
                    }

                }
                if (!_canBuild)
                    break;
            }
        }
    }

    private void UpdateColor()
    {
        _mainRenderer.material.color = _canBuild ? _staticData.AllowedPositionColor : _staticData.BannedPositionColor;
    }

    private GridCell GetClosestGridCell(Vector3 position)
    {
        float minDistance = float.MaxValue;
        GridCell closestCell = null;

        foreach (var greedHolder in _spaceDeterminantor.GreedHolders)
        {
            foreach (var cell in greedHolder.Grid)
            {
                float distance = Vector2.Distance(new Vector2(cell.CenterX, cell.CenterY), position);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    closestCell = cell;
                }
            }
        }

        return closestCell;
    }

    private Vector3 FindCursorPosition()
    {
        Vector2 mouseScreenPosition = Mouse.current.position.ReadValue();

        Vector3 mouseWorldPosition = _mainCamera.ScreenToWorldPoint(new Vector3
            (mouseScreenPosition.x, mouseScreenPosition.y, -_mainCamera.transform.position.z));
        return mouseWorldPosition;
    }

    private bool IsPositionInGrid(float x, float y) //always fals
    {
        //Debug.Log(GetGridCellAt(x, y));
        return GetGridCellAt(x, y) != null;
    }

    private GridCell GetGridCellAt(float x, float y)
    {
        foreach (var greedHolder in _spaceDeterminantor.GreedHolders)
        {
            foreach (var cell in greedHolder.Grid)
            {

                if (Mathf.Abs(cell.CenterX - x) < _staticData.Epsilon && Mathf.Abs(cell.CenterY - y) < _staticData.Epsilon)
                {
                    //Debug.Log("DA!");
                    return cell;
                }

            }
        }
        return null;
    }

    private void PlaceObject()
    {
        _isPlacing = false;

        _mainRenderer.material.color = _staticData.NormalColor;

        foreach (var collider in _colliders)
            collider.enabled = true;

        _decorHolder.InstallDecor(this);

        OccupyCells();
        PlacedAction?.Invoke();
    }

    private void OccupyCells()
    {
        //_closestCell.OccupyCell();

        OnOccupyCell?.Invoke(_closestCell);

        if (_occupiedCells.x == 3)
        {
            var x = ((_closestCell.CenterX + _staticData.CellSize / 2) + _staticData.CellSize / 2);
            var y = _closestCell.CenterY;

            var rightCell = GetGridCellAt(x, y);
            //rightCell.OccupyCell();
            rightCell.SpriteRenderer.color = Color.red;

            x = ((_closestCell.CenterX - _staticData.CellSize / 2) - _staticData.CellSize / 2);

            var leftCell = GetGridCellAt(x, y);
            //leftCell.OccupyCell();
            leftCell.SpriteRenderer.color = Color.red;
            
            OnOccupyCell?.Invoke(rightCell);
            OnOccupyCell?.Invoke(leftCell);
        }
        if (_occupiedCells.y == 2)
        {

        }

    }

    private void CheckCamera()
    {
        if (_mainCamera == null)
            _mainCamera = Camera.main;
    }

    private void OnDisable()
    {
        _isPlacing = false;
        _clickAcrion.action.performed -= OnClick;
        _cancelAcrion.action.performed -= OnCanceled;
    }
}

public enum RotationState
{
    Front = 0,
    Left = 1,
    Back = 2,
    Right = 3,
}


