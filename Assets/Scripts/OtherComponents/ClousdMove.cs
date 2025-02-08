using System.Collections.Generic;
using UnityEngine;

public class ClousdMove : MonoBehaviour, IRandomizedPosition
{
    [SerializeField] private Transform[] _transforms;
    [SerializeField] private float _moveSpeedMax = 5;
    [SerializeField] private float _moveSpeedMin = 1;
    [Space]
    [SerializeField] private float _maxPositionX = 80;
    [SerializeField] private float _minPositionX = -50;
    [SerializeField] private float _maxPositionY = 45;
    [SerializeField] private float _minPositionY = -55;

    private Dictionary<Transform, float> _speedsDict = new();

    public Transform[] GetTransforms()
    {
        return _transforms;
    }

    public Vector2 GetMinBounds()
    {
        return new Vector2(_minPositionX, _minPositionY);
    }

    public Vector2 GetMaxBounds()
    {
        return new Vector2 (_maxPositionX, _maxPositionY);
    }

    private void Start()
    {
        foreach (Transform transform in _transforms)
        {
            _speedsDict.Add(transform, SelectSpeed());
        }
    }

    private void Update()
    {
        foreach (Transform transform in _transforms)
        {
            if (transform == null) Debug.Log("Ooooops!__________");

            if (transform.position.x < _minPositionY)
            {
                transform.position = new Vector3(_maxPositionX, transform.position.y, transform.position.z);
                _speedsDict[transform] = SelectSpeed();
            }
            else if (transform.position.x > _maxPositionX)
            {
                transform.position = new Vector3(_minPositionY, transform.position.y, transform.position.z);
                _speedsDict[transform] = SelectSpeed();
            }
            else
            {
                transform.position += new Vector3(_speedsDict[transform] * Time.deltaTime, 0, 0);
            }
        }
    }

    private float SelectSpeed() =>
        Random.Range(_moveSpeedMin, _moveSpeedMax);
}
