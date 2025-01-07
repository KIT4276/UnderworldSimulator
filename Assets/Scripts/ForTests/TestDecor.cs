using UnityEngine;
using Zenject;

public class TestDecor : MonoBehaviour
{
    [Inject] private DecorationSystem _decorationSystem;

    public void SpawnDecor(Decor decorPrefab)
    {
        _decorationSystem.InstantiateDecor(decorPrefab);
    }
}
