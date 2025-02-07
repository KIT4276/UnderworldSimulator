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
            Vector3 snappedPosition = new Vector3(snappedX, snappedY, _decor.transform.position.z);

            bool isInsideFloor = false;
            foreach (var floor in _spaceDeterminantor.FloorMarkers)
            {
                if (floor.Collider.OverlapPoint(snappedPosition))
                {
                    isInsideFloor = true;
                    break;
                }
            }

            if (isInsideFloor)
            {
                _decor.transform.position = snappedPosition;
            }
        }
    }
}
