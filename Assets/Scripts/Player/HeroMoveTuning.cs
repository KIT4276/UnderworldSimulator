using UnityEngine;
using UnityEngine.UI;

public class HeroMoveTuning : MonoBehaviour
{
    [SerializeField] private HeroMove _heroMove;
    [SerializeField] private Slider _slider;
    [SerializeField] private Text _text;


    private void Start()
    {
        _slider.value = _heroMove.MoveSpeed;
        _text.text = _heroMove.MoveSpeed.ToString();
    }

    public void ChandeSpeed()
    {
        _heroMove.ChangeMoveSpeed(_slider.value);
        _text.text = _heroMove.MoveSpeed.ToString();
    }
}
