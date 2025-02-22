using TMPro;
using UnityEngine;

public class Hint : MonoBehaviour
{
    [SerializeField] private TMP_Text _hintText;
    [SerializeField] private RectTransform _hintRectTransform;
    [Space]
    [SerializeField] private float _indentX = 30;
    [SerializeField] private float _indentY = 20;

    public void AddText(string text)
    {
        _hintText.text = text;
    }

    public void AddIntent(Vector3 position)
    {
        _hintRectTransform.position = position + new Vector3(_indentX, _indentY, 0);
    }
}
