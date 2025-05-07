using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Decor))]
public class DecorPlacer : MonoBehaviour
{
    private bool _canMove;
    private Decor _decor;
    private SpaceDeterminantor _spaceDeterminantor;
    private DecorHolder _decorHolder;
    private FloorMarker _floor;
    private Animator _animator;
    public GameObject placementEffectPrefab;

    public void Initialize(Decor decor, SpaceDeterminantor spaceDeterminantor, DecorHolder decorHolder)
    {
        _canMove = true;
        _decor = decor;
        _spaceDeterminantor = spaceDeterminantor;
        _decorHolder = decorHolder;
        _decor.Clicked += OnClicked;
        _animator = decor.GetComponent<Animator>();
        if (_animator == null)
        {
            Debug.LogWarning("No Animator component found on the Decor. Please add it.");
        }
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
                
                _floor = floor;
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
                _floor.AddDecor(_decor);
                if (_animator != null)
                {
                    _animator.SetTrigger("MakeScale");
                }
                if (placementEffectPrefab != null)
                {
                    GameObject effectInstance = Instantiate(placementEffectPrefab, _decor.transform.position, Quaternion.identity);

                    // Optionally destroy after duration to prevent clutter
                    Destroy(effectInstance, 5f); // or use main duration
                }
            }
        }
        else if (IsMouseOnObject() && _decorHolder.ActiveDecor == null)
        {
            if (_decor.TakeDecorIfCan())
            {
                _floor.DeleteDecor(_decor);
                _floor = null;
            }
        }
    }

    protected bool IsMouseOnObject()
    {
        
        Vector2 mouseScreenPos = Mouse.current.position.ReadValue();
        Ray ray = _decor.MainCamera.ScreenPointToRay(mouseScreenPos);
        RaycastHit2D[] hits = Physics2D.GetRayIntersectionAll(ray, Mathf.Infinity, LayerMask.GetMask("Decor"));

        foreach (var hit in hits)
        {
            if (hit.collider == _decor.CurrentClickableCollider)
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