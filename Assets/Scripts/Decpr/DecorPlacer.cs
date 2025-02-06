using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Decor))]
public class DecorPlacer : MonoBehaviour
{
    private Decor _decor;

    public void Initialize(Decor decor)
    {
        _decor = decor;
        _decor.Clicked += OnClicked;
    }

    public void OnRemoved()
    {
        _decor.Clicked -= OnClicked;
        _decor.Removed -= OnRemoved;
    }

    private void OnClicked()
    {
        if (_decor.IsDragging)
        {
            if (_decor.IsInside)
            {
                _decor.PlaceObject();
            }
        }
        else if (IsMouseOnObject())
        {
            _decor.TakeDecorIfCan();
        }
    }

    private bool IsMouseOnObject()
    {
        Vector2 mouseScreenPos = Mouse.current.position.ReadValue();
        Ray ray = _decor.MainCamera.ScreenPointToRay(mouseScreenPos);
        RaycastHit2D[] hits = Physics2D.GetRayIntersectionAll(ray, Mathf.Infinity, LayerMask.GetMask("Decor"));

        foreach (var hit in hits)
        {
            if (hit.collider == _decor.CurrentClickableCollider || hit.collider.transform.IsChildOf(transform))
            {
                return true;
            }
        }
        return false;
    }
}