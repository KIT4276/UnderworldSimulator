using UnityEngine;
using UnityEngine.EventSystems;

public class ZoomSwitch : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private CameraMove _cameraMove;
    
    private void Start()
    {
        _cameraMove = Camera.main.GetComponent<CameraMove>(); //Crutch!!!
        
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _cameraMove.SetCanZoom(false);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _cameraMove.SetCanZoom(true);
    }
}
