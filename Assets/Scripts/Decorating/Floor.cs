using UnityEngine;
using Zenject;

public class Floor : MonoBehaviour
{
    [Inject] private PersistantStaticData _persistantStaticData;
    //[SerializeField] private SpriteRenderer _spriteRenderer;

    public float X_NumberOfSquares { get; private set; }
    public float Y_NumberOfSquares { get; private set; }

    private void Start()
    {
        //_spriteRenderer.enabled = false;

        X_NumberOfSquares = transform.localScale.x / _persistantStaticData.CellSize;
        Y_NumberOfSquares = transform.localScale.y / _persistantStaticData.CellSize;
    }

    public float GetCellSize()
    {
        return _persistantStaticData.CellSize;
    }
}
