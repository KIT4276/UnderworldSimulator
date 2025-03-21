using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    [SerializeField] protected Image _bar;
    
    public void ValueChange(float value)
    {
        _bar.fillAmount = value;
    }

    protected void Awake()
    {
        _bar.fillAmount = 0;
    }
}
