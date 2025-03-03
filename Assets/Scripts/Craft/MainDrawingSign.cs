using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainDrawingSign : MonoBehaviour
{
    [SerializeField] private TMP_Text _name;
   
    [SerializeField] private Image _icon;

    [Space, Tooltip("Материалы")]
    [SerializeField] private TMP_Text[] _material;
    [SerializeField] private TMP_Text[] _materialscount;
    //TODO:
    //[SerializeField] private TMP_Text[] _availableCount;
    //[SerializeField] private Image[] _materialsIcon;


    public void FillSign(Drawing drawing)
    {
        _name.text = drawing.Name;
        _icon.sprite = drawing.Icon;

        for (int i = 0; i < drawing.DrawingComponents.Length; i++)
        {
            _material[i].text = drawing.DrawingComponents[i].Material.ToString();
            _materialscount[i].text = drawing.DrawingComponents[i].Count.ToString();
        //todo Fill _materialsIcon, _availableCount from Craft System
        }

    }
}