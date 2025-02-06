using System;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Decor))]
public class DecorDrag : MonoBehaviour
{
    private Decor _decor;
    private PersistantStaticData _staticData;
    private SpaceDeterminantor _spaceDeterminantor;

    private bool _canMove;

    public void Initialize(Decor decor, PersistantStaticData staticData, SpaceDeterminantor spaceDeterminantor)
    {
        _canMove = true;
        _decor = decor;
        _staticData = staticData;
        _spaceDeterminantor = spaceDeterminantor;
    }

    public void OnRemoved()
    {
        _canMove = false;
    }

    private void FixedUpdate()
    {
       // Debug.Log(_decor.IsDragging);
        if (_decor.IsDragging && _canMove)
            DragObjeect();
    }

    private void DragObjeect()
    {
        Vector3 mousePosition = Mouse.current.position.ReadValue();
        Ray ray = _decor.MainCamera.ScreenPointToRay(mousePosition);
        Plane xyPlane = new Plane(Vector3.forward, Vector3.zero);

        if (xyPlane.Raycast(ray, out float distance))
        {
            Vector3 worldPosition = ray.GetPoint(distance);

            float snappedX = Mathf.Round(worldPosition.x / _staticData.CellSize) * _staticData.CellSize;
            float snappedY = Mathf.Round(worldPosition.y / _staticData.CellSize) * _staticData.CellSize;

            _decor.transform.position = new Vector3(snappedX, snappedY, _decor.transform.position.z);
        }

        CheckPlacement();
    }

    private void CheckPlacement()
    {
        _decor.SetIsInside(false);

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
                _decor.SetIsInside(true);
                break;
            }
        }
    }

}
