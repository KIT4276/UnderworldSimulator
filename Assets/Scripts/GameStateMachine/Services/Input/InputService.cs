using UnityEngine;

public abstract class InputService : IInputService // parent class for different types of input
{
    public abstract Vector2 Axis { get; }

    public abstract Vector3 Position { get; }
}
