using TMPro;
using UnityEngine;
using Zenject;

public class RoomRating : MonoBehaviour
{
    [SerializeField] private TMP_Text _name;
    [Space]
    [SerializeField] private TMP_Text _nameOfParameter_1;
    [SerializeField] private TMP_Text _parameter_1;
    [Space]
    [SerializeField] private TMP_Text _nameOfParameter_2;
    [SerializeField] private TMP_Text _parameter_2;
    [Space]
    [SerializeField] private TMP_Text _nameOfParameter_3;
    [SerializeField] private TMP_Text _parameter_3;

    [Inject] private SpaceDeterminantor _spaceDeterminantor;

    public void Start()
    {
        OnFindFloor();
        _spaceDeterminantor.Find += OnFindFloor;
    }

    private void OnFindFloor()
    {
        
        foreach (var floor in _spaceDeterminantor.FloorMarkers)
        {
            floor.ChangeParameter += ShowParameters;
        }
        ShowParameters(_spaceDeterminantor.FloorMarkers[0]);
    }

    private void ShowParameters(FloorMarker floor)
    {
        
        _name.text = floor.Name;
        _nameOfParameter_1.text = RoomParameterNames.Names[floor.SetOfParameters.Parameters[0].ParameterType];
        _parameter_1.text = floor.SetOfParameters.Parameters[0].Value.ToString();

        _nameOfParameter_2.text = RoomParameterNames.Names[floor.SetOfParameters.Parameters[1].ParameterType];
        _parameter_2.text = floor.SetOfParameters.Parameters[1].Value.ToString();

        _nameOfParameter_3.text = RoomParameterNames.Names[floor.SetOfParameters.Parameters[2].ParameterType];
        _parameter_3.text = floor.SetOfParameters.Parameters[2].Value.ToString();
    }


    private void OnDestroy()
    {
        _spaceDeterminantor.Find -= OnFindFloor;
        foreach (var floor in _spaceDeterminantor.FloorMarkers)
        {
            floor.ChangeParameter -= ShowParameters;
        }
    }
}
