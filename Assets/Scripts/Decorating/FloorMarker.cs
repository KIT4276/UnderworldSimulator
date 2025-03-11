using System;
using System.Collections.Generic;
using UnityEngine;

public class FloorMarker : MonoBehaviour
{
    [SerializeField] private PolygonCollider2D _collider;
    [SerializeField] private ClickHandler _clickHandler;
    [Space]
    [SerializeField] private int _startParameter_1;
    [SerializeField] private int _startParameter_2;
    [SerializeField] private int _startParameter_3;

    public PolygonCollider2D Collider {  get => _collider;}
    public List<Decor> InstalledDecor { get; private set; }

    public int Parameter_1 { get; private set; }
    public int Parameter_2 { get; private set; }
    public int Parameter_3 { get; private set; }

    public event Action<FloorMarker> ChangeParameter;

    private void Start()
    {
        InstalledDecor = new();
        _clickHandler.ClickAction += ShowParameters;

        Parameter_1 = _startParameter_1;
        Parameter_2 = _startParameter_2;
        Parameter_3 = _startParameter_3; 
    }

    public void AddDecor(Decor decor)
    {

        InstalledDecor.Add(decor);
        Parameter_1 += decor.Parameter_1;
        Parameter_2 += decor.Parameter_2;
        Parameter_3 += decor.Parameter_3;
        ShowParameters();
    }

    public void DeleteDecor(Decor decor)
    {
        InstalledDecor.Remove(decor);
        Parameter_1 -= decor.Parameter_1;
        Parameter_2 -= decor.Parameter_2;
        Parameter_3 -= decor.Parameter_3;
        ShowParameters();
    }

    private void ShowParameters()
    {
        ChangeParameter?.Invoke(this);
    }
}
