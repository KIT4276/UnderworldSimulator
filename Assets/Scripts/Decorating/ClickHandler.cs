using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class ClickHandler : MonoBehaviour
{
    [SerializeField] private InputActionReference _click;
    [SerializeField] private Collider2D _collider;

    public event Action ClickAction;

    private void Start()
    {
        _click.action.performed += OnClick;
    }

    private void OnClick(InputAction.CallbackContext context)
    {
        Vector2 mouseScreenPos = Mouse.current.position.ReadValue();
        Ray ray = Camera.main.ScreenPointToRay(mouseScreenPos);
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

    private void ClickOnFloor()
    {
        ClickAction?.Invoke();
    }
}