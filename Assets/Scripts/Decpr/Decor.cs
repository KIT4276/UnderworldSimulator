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

    private const string FloorTag = "Floor";
    private PersistantStaticData _staticData;
    private DecorationSystem _decorationSystem;
    private Camera _mainCamera;
    private bool _isPlacing;
    private bool _canBuild;

    private int _groundlayerMask = (1 << 1) | (1 << 6) | (1 << 8)| (1 << 10);

    public event Action PlacedAction;
    public event Action CanceledAcrion;

    public void Initialize(PersistantStaticData staticData, DecorationSystem decorationSystem)
    {
        _isPlacing = true;
        _staticData = staticData;
        _decorationSystem = decorationSystem;

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
                Debug.Log(hit.collider.name);
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

                Ray ray = _mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());

                RaycastHit2D[] hits = Physics2D.GetRayIntersectionAll(ray);

                foreach (var hit in hits)
                {
                    if (hit.collider == null || !hit.collider.CompareTag(FloorTag))
                    {
                        _canBuild = false;
                        break;
                    }
                }




                //    RaycastHit2D hit = Physics2D.Raycast(cellPosition, Vector2.zero/*, Mathf.Infinity, _groundlayerMask*/);

                //if (hit.collider == null || !hit.collider.CompareTag(FloorTag))
                //{
                //    _canBuild = false;
                //    break;
                //}
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

        foreach (var collider in _colliders)
            collider.enabled = true;

        PlacedAction?.Invoke();
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
