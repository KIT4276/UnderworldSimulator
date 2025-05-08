using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ChangeProgressbarColor : MonoBehaviour
{
    [SerializeField] private Color _startColor;
    [SerializeField] private Color _doneColor;
    [SerializeField] private Image _bar;

    private void Update()
    {
        if(_bar.fillAmount < 1)
            _bar.color = _startColor;
        else
            _bar.color = _doneColor;
    }

}
