using TMPro;
using UnityEngine;

public class Hint : MonoBehaviour
{
    [SerializeField] private string _materialText = "Ресурс";
    [SerializeField] private string _decorText = "Декор";
    [Space]
    [SerializeField] private TMP_Text _hintText;
    [SerializeField] private TMP_Text _elementType;
    [SerializeField] private SubHint[] _paramsSlots;
    [Space]
    [SerializeField] private RectTransform _hintRectTransform;
    [Space]
    [SerializeField] private float _indentX = 30;
    [SerializeField] private float _indentY = 20;
    //[SerializeField] private float _textPadding = 10; // Отступы по ширине 
    //[SerializeField] private TextMeshProUGUI _nameTmp;
    //[SerializeField] private RectTransform _nameRectTransform;

    public void AddText(IBaseItem item, ParameterData parameterData)
    {
        _hintText.text = item.GetHint();

        if (item is Decor decor)
        {
            if (_elementType != null)
            {
                _elementType.text = _decorText;
            }

            foreach (var slot in _paramsSlots)
            {
                slot.gameObject.SetActive(false);
            }

            for (int i = 0; i < decor.Parameters.Parameters.Length; i++)
            //for (int i = 0; i < _paramsSlots.Length; i++)
            {
                if (decor.Parameters.Parameters[i] != null && decor.Parameters.Parameters[i].Value > 0)
                {
                    _paramsSlots[i].gameObject.SetActive(true);

                    Sprite icon = null;

                    foreach (var p in parameterData.Parameters)
                    {
                        if (p.ParameterType == decor.Parameters.Parameters[i].ParameterType)
                        {
                            icon = p.DecorParamIcon;
                        }
                    }

                    _paramsSlots[i].Fill(icon, decor.Parameters.Parameters[i].Value);
                }
            }
        }
        else
        {
            if (_elementType != null)
            {
                _elementType.text = _materialText;
            }
            if (_paramsSlots != null && _paramsSlots.Length != 0)
            {
                foreach (var p in _paramsSlots)
                {
                    p.gameObject.SetActive(false);
                }
            }
        }

        //UpdateNameSize();
    }

    //private void UpdateNameSize()
    //{
    //    if (_nameTmp.text == null) return;

    //    _nameTmp.ForceMeshUpdate(); // Обновление текста
    //    float preferredWidth = _nameTmp.GetPreferredValues().x;

    //    // Оставляем текущую высоту, меняем только ширину
    //    _nameRectTransform.sizeDelta = new Vector2(preferredWidth + _textPadding, _nameRectTransform.sizeDelta.y);
    //}

    public void AddIntent(Vector3 position)
    {
        _hintRectTransform.position = position + new Vector3(_indentX, _indentY, 0);
    }
}
