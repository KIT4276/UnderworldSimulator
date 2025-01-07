using System;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

[RequireComponent(typeof(Collider2D))]
public class Decor : MonoBehaviour
{
    [SerializeField] private Collider2D _collider;
    [SerializeField] private SpriteRenderer _mainRenderer;
    [SerializeField] private Vector2 _occupiedCells;
    [SerializeField] private InputActionReference _clickAcrion;
    [SerializeField] private InputActionReference _cancelAcrion;

    private const string FloorTag = "Floor";
    private PersistantStaticData _staticData;
    private Camera _mainCamera;
    private bool _isPlacing;
    private bool _canBuild;

    public event Action PlacedAcrion;
    public event Action CanceledAcrion;

    private void Awake()
    {
        //Debug.Log(_staticData);
        _collider.enabled = false;
        //_normColor = new Color(1, 1, 1, 1);
        //_red = new Color(1, 0.2f, 0.2f, 0.5f);

        _clickAcrion.action.performed += OnClick;
        _cancelAcrion.action.performed += OnCanceled;
    }

    private void OnCanceled(InputAction.CallbackContext context)
    {
        _isPlacing = false;
        CanceledAcrion?.Invoke();
    }

    private void OnClick(InputAction.CallbackContext context)
    {
        if (_canBuild && _isPlacing)
        {
            PlaceObject();
        }
    }

    public void Initialize(PersistantStaticData staticData)
    {
        _isPlacing = true;
        _staticData = staticData;
    }

    private void FixedUpdate()
    {
        if (_isPlacing)
        {
            transform.position = GetSnappedMouseWorldPosition();
            CheckIfCanBuild();
        }
    }

    private Vector3 GetSnappedMouseWorldPosition()
    {
        CheckCamera();

        Vector3 mouseWorldPosition = FindCursorPosition();

        mouseWorldPosition.z = 0f;

        mouseWorldPosition.x = Mathf.Floor(mouseWorldPosition.x / _staticData.CellSize) * _staticData.CellSize;
        mouseWorldPosition.y = Mathf.Floor(mouseWorldPosition.y / _staticData.CellSize) * _staticData.CellSize;

        return mouseWorldPosition;
    }

    private Vector3 FindCursorPosition()
    {
        Vector2 mouseScreenPosition = Mouse.current.position.ReadValue();

        Vector3 mouseWorldPosition = _mainCamera.ScreenToWorldPoint(new Vector3
            (mouseScreenPosition.x, mouseScreenPosition.y, -_mainCamera.transform.position.z));
        return mouseWorldPosition;
    }

    private void CheckIfCanBuild()
    {
        CheckCamera();

        Vector3 snappedPosition = GetSnappedMouseWorldPosition();

        Vector2 offset = new Vector2(
            -(_occupiedCells.x / 2f) * _staticData.CellSize + _staticData.CellSize / 2f,
            -(_occupiedCells.y / 2f) * _staticData.CellSize + _staticData.CellSize / 2f
        );

        _canBuild = true;

        for (int x = 0; x < _occupiedCells.x; x++)
        {
            for (int y = 0; y < _occupiedCells.y; y++)
            {
                Vector2 cellPosition = new Vector2(
                    snappedPosition.x + offset.x + x * _staticData.CellSize,
                    snappedPosition.y + offset.y + y * _staticData.CellSize
                );

                RaycastHit2D hit = Physics2D.Raycast(cellPosition, Vector2.zero);
                
                if (hit.collider == null || !hit.collider.CompareTag(FloorTag))
                {
                    _canBuild = false;
                    break;
                }
            }
            if (!_canBuild)
                break;
        }

        _mainRenderer.material.color = _canBuild ? _staticData.AllowedPositionColor : _staticData.BannedPositionColor;
    }

    private void PlaceObject()
    {
        _isPlacing = false;

        _mainRenderer.material.color = _staticData.NormalColor;

        _collider.enabled = true;
        PlacedAcrion?.Invoke();
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
