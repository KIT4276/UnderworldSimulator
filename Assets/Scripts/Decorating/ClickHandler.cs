using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class ClickHandler : MonoBehaviour
{
    [SerializeField] private InputActionReference _click;
    [SerializeField] private Collider2D _collider;
    [SerializeField] public EventSystem _eventSystem;

     private GraphicRaycaster[] _raycasters;
    private Camera _camera;

    public event Action ClickAction;

    private void Start()
    {
        _click.action.performed += OnClick;
    }

    private void OnClick(InputAction.CallbackContext context)
    {
        Vector2 mouseScreenPos = Mouse.current.position.ReadValue();

        if (_raycasters == null || _raycasters.Length == 0)
        {
            _raycasters = FindObjectsByType<GraphicRaycaster>(FindObjectsSortMode.None);
        }

        if (IsPointerOverUI(mouseScreenPos)) return;

        CheckCamera();
        if (_camera == null) return;

        Ray ray = _camera.ScreenPointToRay(mouseScreenPos);
        RaycastHit2D[] hits = Physics2D.GetRayIntersectionAll(ray, Mathf.Infinity, LayerMask.GetMask("Floor"));

        foreach (var hit in hits)
        {
            if (hit.collider == _collider)
            {
                ClickOnFloor();
                break;
            }
        }
    }

    private bool IsPointerOverUI(Vector2 screenPosition)
    {
        PointerEventData eventData = new(_eventSystem);
        eventData.position = screenPosition;

        List<RaycastResult> results = new ();

        foreach (var raycaster in _raycasters)
        {
            raycaster.Raycast(eventData, results);
            if (results.Count > 0)
                return true;
        }

        return false;
    }

    private void CheckCamera()
    {
       if(_camera == null)
            _camera = Camera.main;
    }

    private void ClickOnFloor()
    {
        ClickAction?.Invoke();
    }

    private void OnDestroy()
    {
        _click.action.performed -= OnClick;
    }
}