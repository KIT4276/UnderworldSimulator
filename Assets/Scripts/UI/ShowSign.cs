using UnityEngine;
using UnityEngine.EventSystems;

public class ShowSign : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private GameObject _signObject;

    private void Start()
    {
        _signObject.SetActive(false);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _signObject.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _signObject.SetActive(false);
    }
}
