using DragonBones;
using UnityEngine;
using UnityEngine.EventSystems;

public class BaseShowSign : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] protected GameObject _signObject;

    protected void Start()
    {
        HideSign();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        ShowSign();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        HideSign();
    }

    protected virtual void HideSign()
    {
        _signObject.SetActive(false);
    }

    protected virtual void ShowSign()
    {
        _signObject.SetActive(true);
    }
}
