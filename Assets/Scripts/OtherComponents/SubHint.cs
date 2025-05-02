using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SubHint : MonoBehaviour
{
    [SerializeField] private Image _iconParam;
    [SerializeField] private TMP_Text _valueParam;

    public void Fill(Sprite icon, int value)
    {
        if (value > 0)
        {
            _iconParam.sprite = icon;
            _valueParam.text = "+" + value.ToString();
        }
        else
        {
           this.gameObject.SetActive(false);
        }
    }
}
