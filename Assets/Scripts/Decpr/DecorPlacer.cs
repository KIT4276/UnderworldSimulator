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

    protected void FixedUpdate()
    {
        if (_decor.IsDragging && _canMove)
            _decor.SetIsInside(CheckPlacement());
    }

    protected bool CheckPlacement()
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

        isInside = CheckOtherDecor(isInside);
        return isInside;
    }

    protected virtual bool CheckOtherDecor(bool isInside)
    {
        if (isInside)
        {
            foreach (var otherDecor in _decorHolder.InstalledDecor)
            {
                if (otherDecor == _decor || otherDecor is CarpetDecor) continue;

                if (_decor.CurrentDecorCollider.bounds.Intersects(otherDecor.CurrentDecorCollider.bounds))
                {
                    isInside = false;
                    break;
                }
            }
        }

        return isInside;
    }

    protected void OnClicked()
    {
        if (_decor.IsDragging)
        {
            if (_decor.IsInside)
            {
                _decor.PlaceObject();
            }
        }
        else if (IsMouseOnObject() && _decorHolder.ActiveDecor == null)
        {
            _decor.TakeDecorIfCan();
        }
    }

    protected bool IsMouseOnObject()
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

    protected void OnDisable()
    {
        _decor.Clicked -= OnClicked;
    }
}