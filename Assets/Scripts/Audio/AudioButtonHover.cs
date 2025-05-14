using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AudioButtonHover : MonoBehaviour, IPointerEnterHandler
{
    public void OnPointerEnter(PointerEventData eventData)
    {
        AudioManager.Instance.Play(SoundEnum.Item_Hover_v2);
    }


    // public void OnPointerExit(PointerEventData eventData)
    // {
    //     NormalizeImage();
    // }
}
