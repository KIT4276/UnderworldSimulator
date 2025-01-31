using UnityEngine;
using UnityEngine.EventSystems;

public class UITestButtone : MonoBehaviour, IPointerEnterHandler
{
    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("OnPointerEnter");
    }

    
}
