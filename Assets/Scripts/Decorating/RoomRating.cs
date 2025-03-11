using TMPro;
using UnityEngine;
using Zenject;

public class RoomRating : MonoBehaviour
{
    [SerializeField] private TMP_Text _name;
    [SerializeField] private TMP_Text _parameter_1;
    [SerializeField] private TMP_Text _parameter_2;
    [SerializeField] private TMP_Text _parameter_3;

    [Inject] private SpaceDeterminantor _spaceDeterminantor;

    public void Start()
    {
      //  Debug.Log("Start RoomRating");
        OnFindFloor();
        _spaceDeterminantor.Find += OnFindFloor;
    }

    private void OnFindFloor()
    {
       // Debug.Log("OnFindFloor");
        
        foreach (var floor in _spaceDeterminantor.FloorMarkers)
        {
            floor.ChangeParameter += ShowParameters;
        }
        ShowParameters(_spaceDeterminantor.FloorMarkers[0]);
    }

    private void ShowParameters(FloorMarker floor)
    {
        _name.text = floor.name;
        _parameter_1.text = floor.Parameter_1.ToString();
        _parameter_2.text = floor.Parameter_2.ToString();
        _parameter_3.text = floor.Parameter_3.ToString();
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
