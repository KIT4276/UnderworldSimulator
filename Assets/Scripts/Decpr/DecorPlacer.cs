using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Decor))]
public class DecorPlacer : MonoBehaviour
{
    private bool _canMove;
    private Decor _decor;
    private SpaceDeterminantor _spaceDeterminantor;
    private DecorHolder _decorHolder;

    public void Initialize(Decor decor, SpaceDeterminantor spaceDeterminantor, DecorHolder decorHolder)
    {
        _canMove = true;
        _decor = decor;
        _spaceDeterminantor = spaceDeterminantor;
        _decorHolder = decorHolder;
        _decor.Clicked += OnClicked;
    }

    public void OnRemoved()
    {
        _canMove = false;
    }

    private void FixedUpdate()
    {
        if (_decor.IsDragging && _canMove)
            _decor.SetIsInside(CheckPlacement());
    }

    private bool CheckPlacement()
    {
        bool isInside = false;

        foreach (var floor in _spaceDeterminantor.FloorMarkers)
        {
            bool allPointsInside = true;

            foreach (Vector2 point in _decor.CurrentDecorCollider.bounds.GetCorners())
            {
                if (!floor.Collider.OverlapPoint(point))
                {
                    allPointsInside = false;
                    break;
                }
            }

            if (allPointsInside)
            {
                isInside = true;
                break;
            }
        }

        if (isInside)
        {
            foreach (var otherDecor in _decorHolder.InstalledDecor)
            {
                if (otherDecor == _decor) continue;

                if (_decor.CurrentDecorCollider.bounds.Intersects(otherDecor.CurrentDecorCollider.bounds))
                {
                    isInside = false;
                    break;
                }
            }
        }
        return isInside;
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

    private void OnDisable()
    {
        _decor.Clicked -= OnClicked;
    }
}