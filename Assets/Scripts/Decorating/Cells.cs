using UnityEngine;
using Zenject;

public class Cells : MonoBehaviour
{
    [SerializeField] private GameObject _cells;

    
    private DecorationState _decorationState;


    [Inject]
    public void Construct(DecorationState decorationState)
    {
        _cells.SetActive(true);
        _decorationState = decorationState;
        _decorationState.DecorationStateEnter += OnDecorStateEnter;
        _decorationState.DecorationStateExit += OnDecorStateExit;
    }

    private void OnDecorStateExit()
    {
        _cells.SetActive(false);
    }

    private void OnDecorStateEnter()
    {
        _cells.SetActive(true);
    }

    private void OnDisable()
    {
        _decorationState.DecorationStateEnter -= OnDecorStateEnter;
        _decorationState.DecorationStateExit -= OnDecorStateExit;
    }
}
