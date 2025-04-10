using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class HotelViewSwitch : MonoBehaviour
{
    [SerializeField] private Image _hotelViewImage;
    [Space]
    [SerializeField] private Sprite _withRoofSprite;
    [SerializeField] private Sprite _withWallsSprite;
    [SerializeField] private Sprite _withFloorSprite;

    private HotelViewState _currentlViewState;
    private WallsSystem _wallsSystem;

    [Inject]
    private void Construct(WallsSystem wallsSystem)
    {
        _wallsSystem = wallsSystem;
    }

    private void Start() => 
        _currentlViewState = HotelViewState.Roof;

    public void SwitchUpViewState()
    {
        _currentlViewState = (HotelViewState)(((int)_currentlViewState + 1) % 3);
        UpdateImage();
        UpdateObjects();
    }

    public void SwitchDownViewState()
    {
        _currentlViewState = (HotelViewState)(((int)_currentlViewState - 1 + 3) % 3);
        UpdateImage();
        UpdateObjects();
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

    private void UpdateObjects()
    {
        switch (_currentlViewState)
        {
            case HotelViewState.Roof:
                _wallsSystem.SwitchToRoof();
                break;
            case HotelViewState.Walls:
                _wallsSystem.SwitchToBig();
                break;
            case HotelViewState.Floor:
                _wallsSystem.SwitchToSmall();
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
