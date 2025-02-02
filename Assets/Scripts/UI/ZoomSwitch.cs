using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class ZoomSwitch : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private CameraMove _cameraMove;

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (_cameraMove == null)
            FindCamera();

        _cameraMove.SetCanZoom(false);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _cameraMove.SetCanZoom(true);
    }

    private void FindCamera()
    {
        _cameraMove = Camera.main.GetComponent<CameraMove>(); //Crutch!!!
    }

    private void OnDisable()
    {
        if (_cameraMove == null) return;
            
        _cameraMove.SetCanZoom(true);
    }
}
