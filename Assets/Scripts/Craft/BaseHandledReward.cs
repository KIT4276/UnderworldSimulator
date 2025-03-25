using UnityEngine;

public abstract class BaseHandledReward
{
    //[SerializeField] protected string _name;
    [SerializeField, Tooltip("Is it avalible on start")] protected bool _isAvalible;

    public abstract string Name { get; } //{ get /*=> _name*/; }
    public bool IsAvalible { get => _isAvalible; }

    public void MakeAvailable() =>
       _isAvalible = true;
}