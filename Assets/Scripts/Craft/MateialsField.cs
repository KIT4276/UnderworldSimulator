using TMPro;
using UnityEngine;
using UnityEngine.UI;
//[Serializable]
public class MateialsField : MonoBehaviour
{
    [SerializeField] private TMP_Text _nameText;
    [SerializeField] private TMP_Text _neededCount;
    [SerializeField] private Image _materialsIcon;
    [Space]
    [SerializeField] private TMP_Text _availableCount;
    [SerializeField] private GameObject _slash;

    public TMP_Text Material { get => _nameText; }
    public TMP_Text MaterialsCount { get => _neededCount; }
    public Image MaterialsIcon { get => _materialsIcon; }
    public TMP_Text AvailableCount { get => _availableCount; }
    public GameObject Slash {  get => _slash; }
}