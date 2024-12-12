using UnityEngine;
using UnityEngine.UI;

public class TestsMenu : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    [SerializeField] private HeroMove _heroMove;
    [SerializeField] private Text _text;
    public void ChangeSpeed()
    {
        _heroMove.ChangeMoveSpeed(_slider.value);
        UpdateSpeed();
    }

    private void Start()
    {
        UpdateSpeed();
    }

    private void UpdateSpeed()
    {
        _text.text = _heroMove.MoveSpeed.ToString();
    }
}
