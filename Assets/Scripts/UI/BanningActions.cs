using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

public class BanningActions : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private CameraMove _cameraMove;

    [Inject] private DecorationSystem _decorationSystem;

    public void OnPointerEnter(PointerEventData eventData)
    {
        _decorationSystem.BanActions();
        
        if (_cameraMove == null)
            FindCamera();
        if (_cameraMove == null) return;

        _cameraMove.SetCanZoom(false);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (_cameraMove == null)
            FindCamera();
        if (_cameraMove == null) return;

        _decorationSystem.AllowActions();
        _cameraMove.SetCanZoom(true);
    }

    private void FindCamera()
    {
        if (Camera.main == null) return;
        // _cameraMove = 
        Camera.main.TryGetComponent<CameraMove>(out _cameraMove);


           // Camera.main.GetComponent<CameraMove>(); //Crutch!!!
    }

    private void OnDisable()
    {
        if (_cameraMove == null) return;
            
        _cameraMove.SetCanZoom(true);
    }
}
