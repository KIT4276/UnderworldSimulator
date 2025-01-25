using UnityEngine;

[CreateAssetMenu(fileName = "GameData", menuName = "ScriptableObjects/GameDataScriptableObject", order = 2)]

public class PersistantStaticData : ScriptableObject
{
    [Header("For Decoration")]
    [SerializeField] private float _cellSize = 0.5f;
    [SerializeField] private Color _allowedPositionColor = Color.blue;
    [SerializeField] private Color _bannedPositionColor = new Color(1, 0.2f, 0.2f, 0.5f);
    /*[SerializeField]*/ private Color _normColor = new Color(1, 1, 1, 1);
    [SerializeField] private float _epsilon = 0.01f;

    public float CellSize => _cellSize;
    public Color AllowedPositionColor => _allowedPositionColor;
    public Color BannedPositionColor => _bannedPositionColor;
    public Color NormalColor => _normColor; 
    public float Epsilon => _epsilon;
}
