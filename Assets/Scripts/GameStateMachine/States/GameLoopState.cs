using NavMeshPlus.Components;
using UnityEngine;

public class GameLoopState : IState
{
    private const string NavMeshSurface = "NavMeshSurface";
    public void Enter() =>
        GameObject.FindWithTag(NavMeshSurface).GetComponent<NavMeshSurface>().enabled = true;//??

    public void Exit() { }
}