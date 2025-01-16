using ModestTree;
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
    [Space]
    [SerializeField] private GameObject _tempPrefab;

    private PersistantStaticData _staticData;
    private DecorationSystem _decorationSystem;
    private SpaceDeterminantor _spaceDeterminantor;
    private Camera _mainCamera;
    private bool _isPlacing;
    private bool _canBuild;
    private GridCell _closestCell;

    public event Action PlacedAction;
    public event Action CanceledAcrion;

    public void Initialize(PersistantStaticData staticData, DecorationSystem decorationSystem
        , SpaceDeterminantor spaceDeterminantor)
    {
        _isPlacing = true;
        _staticData = staticData;
        _decorationSystem = decorationSystem;
        _spaceDeterminantor = spaceDeterminantor;

        _clickAcrion.action.performed += OnClick;
        _cancelAcrion.action.performed += OnCanceled;

        foreach (var collider in _colliders)
            collider.enabled = false;
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
                    Debug.Log("DA");
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


            transform.position = new Vector3(closestCell._centerX, closestCell._centerY, 0f);
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
                    float checkX = closestCell._centerX + offset.x + x * _staticData.CellSize;
                    float checkY = closestCell._centerY + offset.y + y * _staticData.CellSize;

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
                float distance = Vector2.Distance(new Vector2(cell._centerX, cell._centerY), position);
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


                if (Mathf.Abs(cell._centerX - x) < _staticData.Epsilon && Mathf.Abs(cell._centerY - y) < _staticData.Epsilon)
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

        

        OccupyCells();
        PlacedAction?.Invoke();
    }

    private void OccupyCells()
    {
        _closestCell.IsOccupied = true;
        //Instantiate(_tempPrefab, new Vector3(_closestCell._centerX, _closestCell._centerY, 0f), Quaternion.identity);// for tests

        if (_occupiedCells.x == 3)
        {
            var x = ((_closestCell._centerX + _staticData.CellSize / 2) + _staticData.CellSize / 2);
            var y = _closestCell._centerY;

            var rightCell = GetGridCellAt(x, y);
            rightCell.IsOccupied = true;
            //Instantiate(_tempPrefab, new Vector3( rightCell._centerX, rightCell._centerY, 0), Quaternion.identity);

            x = ((_closestCell._centerX - _staticData.CellSize / 2) - _staticData.CellSize / 2);

            var leftCell = GetGridCellAt(x, y);
            leftCell.IsOccupied = true;
            //Instantiate(_tempPrefab, new Vector3(leftCell._centerX, leftCell._centerY, 0), Quaternion.identity);
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


