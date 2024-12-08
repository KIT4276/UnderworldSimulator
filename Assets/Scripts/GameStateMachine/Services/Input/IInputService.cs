using UnityEngine;

public class IInputService : IService
{
    Vector2 Axis { get; }

    Vector3 Position { get; }
}

public abstract class InputService : IInputService
{
    public abstract Vector2 Axis { get; }

    public abstract Vector3 Position { get; }
}
