using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class ClickHandler : MonoBehaviour
{
    [SerializeField] private InputActionReference _click;
    [SerializeField] private Collider2D _collider;

    private Camera _camera;

    public event Action ClickAction;

    private void Start()
    {
        _click.action.performed += OnClick;
    }

    private void OnClick(InputAction.CallbackContext context)
    {
        CheckCamera();
        
        Vector2 mouseScreenPos = Mouse.current.position.ReadValue();
        Ray ray = _camera.ScreenPointToRay(mouseScreenPos);
        RaycastHit2D[] hits = Physics2D.GetRayIntersectionAll(ray, Mathf.Infinity, LayerMask.GetMask("Floor"));

        foreach (var hit in hits)
        {
            if (hit.collider == _collider)
            {
                ClickOnFloor();
                return;
            }
        }
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
}