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


    private WallsSystem _wallsSystem;

    [Inject]
    private void Construct(WallsSystem wallsSystem)
    {
        _wallsSystem = wallsSystem;
    }

    private void Start()
    {
        _wallsSystem.WallsStateChange += UpdateImage;
    }


    public void SwitchUpViewState()
    {
        _wallsSystem.SwitchUpViewState();
        UpdateImage();
    }

    public void SwitchDownViewState()
    {
        _wallsSystem.SwitchDownViewState();
        UpdateImage();
    }

    private void UpdateImage()
    {
        switch (_wallsSystem.CurrentlViewState)
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

    private void OnDestroy()
    {
        _wallsSystem.WallsStateChange -= UpdateImage;
    }
}

public enum HotelViewState
{
    Floor,
    Walls,
    Roof,
}
