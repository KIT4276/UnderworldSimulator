using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

public class Placer : MonoBehaviour
{
    public List<Placable> placedThings;

   // [Inject] private PlayerInput _playerInput;

    private TileMapHolder _grid;
    private Preview _placablePreview;
   // private Camera _mainCamera;

    private void Awake()
    {
        placedThings = new List<Placable>();
    }


    private TileMapHolder GetGrid()
    {
        if (_grid == null)
        {
            _grid = GetComponent<TileMapHolder>();
        }

        return _grid;
    }

    private void Update()
    {
        if (_placablePreview == null)
        {
            return;
        }

        //_placablePreview.transform.position = GetMouceWorldPosition();
        //Debug.Log(_placablePreview.transform.position);

        //if (Input.GetMouseButtonDown(1)) // если нажата ПкМ, то отменяем постройку
        //{
        //    Destroy(placablePreview.gameObject);
        //    placablePreview = null;
        //    return;
        //}
        //else if (Input.GetKeyDown(KeyCode.Return))// (KeyCode.KeypadEnter))
        //{
        //    InstantiatePlacable();
        //}

        //if (Input.GetMouseButton(0))// если нажата или удерживается ЛКМ 
        //{
        //    Vector3 mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //    Vector2Int gridPos = GetGrid().GetGridPosHere(mouse);

        //    Vector2 cellCenter;
        //    if (GetGrid().IsAreaBounded(gridPos.x, gridPos.y, Vector2Int.one))// в пределах ли нашей таблицы
        //    {
        //        cellCenter = GetGrid().GetGridCellPosition(gridPos);
        //    }
        //    else
        //    {
        //        cellCenter = mouse;
        //    }

        //    placablePreview.SetCurrentMousePosition(cellCenter, gridPos, () => GetGrid().IsBuildAvailable(gridPos, placablePreview));
        //}
    }

    

    public void ShowPlacablePreview(Preview preview)
    {
        if (_placablePreview != null)
        {
            Destroy(_placablePreview.gameObject);
        }

        


        _placablePreview = Instantiate(preview, Vector3.zero, Quaternion.identity);

        //Vector2Int gridPos = GetGrid().GetGridPosHere(_placablePreview.transform.position);

        //if (GetGrid().IsAreaBounded(gridPos.x, gridPos.y, Vector2Int.one))
        //{
        //    _placablePreview.SetSpawnPosition(gridPos);
        //    _placablePreview.SetBuildAvailable(GetGrid().IsBuildAvailable(gridPos, _placablePreview));
        //}
        //else
        //{
        //    _placablePreview.SetBuildAvailable(false);
        //}
    }

    private void InstantiatePlacable()
    {
        if (_placablePreview != null && _placablePreview.IsBuildAvailable())
        {
            Placable placableInstance = _placablePreview.InstantiateHere();

            placedThings.Add(placableInstance);
            OccupyCells(placableInstance.GridPlace);

            Destroy(_placablePreview.gameObject);

            if (_placablePreview != null)
            {
                _placablePreview = null;
            }
        }
    }

    private void OccupyCells(GridPlace place)
    {
        GetGrid().SetGridPlaceStatus(place, true);
    }
}
