using UnityEngine;


[CreateAssetMenu(fileName = "DrawingData", menuName = "ScriptableObjects/DrawingData", order = 3)]
public class DrawingData : ScriptableObject
{
    
    [SerializeField] private Drawing[] _drawings;

   public Drawing[] Drawings { get => _drawings; }
    
}
