using System;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Collider2D))]
public class Decor : MonoBehaviour
{
    [SerializeField] private Collider2D _collider;
    [SerializeField] private SpriteRenderer _mainRenderer;
    [SerializeField] private float _cellSize;
    [SerializeField] private Vector2 _occupiedCells;
    [SerializeField] private InputActionReference _clickAcrion;

    private const string FloorTag = "Floor";
    private Camera _mainCamera;

    private bool _isPlacing;
    private bool _canBuild;

    [SerializeField] private Color _blue;
    private Color _red;
    private Color _normColor;

    public event Action PlacedAcrion;

    private void Awake()
    {
        _collider.enabled = false;
        _normColor = new Color(1, 1, 1, 1);
        _red = new Color(1, 0.2f, 0.2f, 0.5f);

        _clickAcrion.action.performed += OnClick;
    }

    private void OnClick(InputAction.CallbackContext context)
    {
        if (_canBuild && _isPlacing)
        {
            PlaceObject();
        }
    }

    public void Initialize()
        => _isPlacing = true;

    private void FixedUpdate()
    {
        if (_isPlacing)
        {
            transform.position = GetSnappedMouseWorldPosition();
            CheckIfCanBuild();

            //// Размещение объекта по клику мышки
            //if (Mouse.current.leftButton.wasPressedThisFrame)
            //{
            //    Debug.Log("Mouse");
            //    if (_canBuild)
            //    {
            //        Debug.Log("_canBuild");
            //        PlaceObject();
            //    }
            //}
        }
    }

    private Vector3 GetSnappedMouseWorldPosition()
    {
        CheckCamera();

        Vector3 mouseWorldPosition = FindCursorPosition();

        mouseWorldPosition.z = 0f;

        // Округляем позицию мыши к ближайшей сетке с учетом _cellSize
        mouseWorldPosition.x = Mathf.Floor(mouseWorldPosition.x / _cellSize) * _cellSize;
        mouseWorldPosition.y = Mathf.Floor(mouseWorldPosition.y / _cellSize) * _cellSize;

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

        // Смещение для расчета клеток относительно центра объекта
        Vector2 offset = new Vector2(
            -(_occupiedCells.x / 2f) * _cellSize + _cellSize / 2f,
            -(_occupiedCells.y / 2f) * _cellSize + _cellSize / 2f
        );

        // Проверяем каждую клетку, которую будет занимать объект
        _canBuild = true; // Предполагаем, что можно строить

        for (int x = 0; x < _occupiedCells.x; x++)
        {
            for (int y = 0; y < _occupiedCells.y; y++)
            {
                // Вычисляем позицию клетки с учетом смещения
                Vector2 cellPosition = new Vector2(
                    snappedPosition.x + offset.x + x * _cellSize,
                    snappedPosition.y + offset.y + y * _cellSize
                );

                // Проверяем каждую клетку на наличие объекта с тегом "Floor"
                RaycastHit2D hit = Physics2D.Raycast(cellPosition, Vector2.zero);
                if (hit.collider == null || !hit.collider.CompareTag(FloorTag))
                {
                    _canBuild = false; // Если хотя бы одна клетка не подходит, строить нельзя
                    break;
                }
            }
            if (!_canBuild)
                break;
        }

        // Меняем цвет в зависимости от возможности строить
        _mainRenderer.material.color = _canBuild ? _blue : _red;
    }

    private void PlaceObject()
    {
        // Завершаем размещение объекта
        _isPlacing = false;

        // Устанавливаем нормальный цвет
        _mainRenderer.material.color = _normColor;

        // При необходимости делаем дополнительные действия:
        // Например, создаём новый объект для размещения
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
    }
}
