using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DraftParameterUnit : MonoBehaviour
{
    [SerializeField] private Image _icon;
    [SerializeField] private TMP_Text _count;

    public void Fill(RoomParameter roomParameter)
    {
        _icon.gameObject.SetActive(true);
        _icon.sprite = roomParameter.Icon;
        _count.gameObject.SetActive(true);
        _count.text = roomParameter.Value.ToString();
    }
    public void FillEmpty()
    {
        _icon.gameObject.SetActive(false);
        _count.gameObject.SetActive(false);
    }
}
