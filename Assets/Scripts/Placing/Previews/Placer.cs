using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

public class Placer : MonoBehaviour
{
    public List<Placable> placedThings;

    private TileMapHolder grid;
    private Preview placablePreview;
    private PlayerInput _playerInput; // todo remove to separate object

    [Inject] private readonly DiContainer _container;

    private void Awake()
    {
        placedThings = new List<Placable>();
        StartCoroutine(WaitForPlayerInput());
    }

    private IEnumerator WaitForPlayerInput()
    {
        Debug.Log("WaitForPlayerInput");
        while (_container.Resolve<PlayerInput>() == null)
            Debug.Log("wait for PlayerInput");

        Debug.Log("find PlayerInput");
        _playerInput = _container.Resolve<PlayerInput>();
        Debug.Log(_playerInput);
        yield return null;
    }

    private TileMapHolder GetGrid()
    {
        if (grid == null)
        {
            grid = GetComponent<TileMapHolder>();
        }

        return grid;
    }

    private void Update()
    {
        if (placablePreview == null)
        {
            return;
        }

        if (Input.GetMouseButtonDown(1)) // если нажата ПкМ, то отменяем постройку
        {
            Destroy(placablePreview.gameObject);
            placablePreview = null;
            return;
        }
        else if (Input.GetKeyDown(KeyCode.Return))// (KeyCode.KeypadEnter))
        {
            InstantiatePlacable();
        }

        if (Input.GetMouseButton(0))// если нажата или удерживается ЛКМ 
        {
            Vector3 mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2Int gridPos = GetGrid().GetGridPosHere(mouse);

            Vector2 cellCenter;
            if (GetGrid().IsAreaBounded(gridPos.x, gridPos.y, Vector2Int.one))// в пределах ли нашей таблицы
            {
                cellCenter = GetGrid().GetGridCellPosition(gridPos);
            }
            else
            {
                cellCenter = mouse;
            }

            placablePreview.SetCurrentMousePosition(cellCenter, gridPos, () => GetGrid().IsBuildAvailable(gridPos, placablePreview));
        }
    }

    public void ShowPlacablePreview(Preview preview)
    {
        if (placablePreview != null)
        {
            Destroy(placablePreview.gameObject);
        }

        var cameraPos = Camera.main.transform.position;
        var instPreviewPos = new Vector2(cameraPos.x, cameraPos.y);

        placablePreview = Instantiate(preview, instPreviewPos, Quaternion.identity);

        Vector2Int gridPos = GetGrid().GetGridPosHere(placablePreview.transform.position);

        if (GetGrid().IsAreaBounded(gridPos.x, gridPos.y, Vector2Int.one))
        {
            placablePreview.SetSpawnPosition(gridPos);
            placablePreview.SetBuildAvailable(GetGrid().IsBuildAvailable(gridPos, placablePreview));
        }
        else
        {
            placablePreview.SetBuildAvailable(false);
        }
    }
        
    private void InstantiatePlacable()
    {
        if (placablePreview != null && placablePreview.IsBuildAvailable())
        {
            Placable placableInstance = placablePreview.InstantiateHere();

            placedThings.Add(placableInstance);
            OccupyCells(placableInstance.GridPlace);

            Destroy(placablePreview.gameObject);

            if (placablePreview != null)
            {
                placablePreview = null;
            }
        }
    }

    private void OccupyCells(GridPlace place)
    {
        GetGrid().SetGridPlaceStatus(place, true);
    }
}
