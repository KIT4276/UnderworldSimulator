using UnityEngine;
using UnityEngine.UI;

public class HotelViewSwitch : MonoBehaviour
{
    [SerializeField] private Image _hotelViewImage;
    [Space]
    [SerializeField] private Sprite _withRoofSprite;
    [SerializeField] private Sprite _withWallsSprite;
    [SerializeField] private Sprite _withFloorSprite;

    private HotelViewState _currentlViewState;

    private void Start() => 
        _currentlViewState = HotelViewState.Roof;

    public void SwitchUpViewState()
    {
        _currentlViewState = (HotelViewState)(((int)_currentlViewState + 1) % 3);
        UpdateImage();
    }

    public void SwitchDownViewState()
    {
        _currentlViewState = (HotelViewState)(((int)_currentlViewState - 1 + 3) % 3);
        UpdateImage();
    }

    private void UpdateImage()
    {
        switch (_currentlViewState)
        {
            case HotelViewState.Roof:
                _hotelViewImage.sprite = _withRoofSprite;
                break;
            case HotelViewState.Walls:
                _hotelViewImage.sprite = _withWallsSprite;
                break;
            case HotelViewState.Floor:
                _hotelViewImage.sprite = _withFloorSprite;
                break;
        }
    }


}

public enum HotelViewState
{
    Floor,
    Walls,
    Roof,
}
