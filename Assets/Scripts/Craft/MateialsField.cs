using DG.Tweening.Core.Easing;
using System;
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
    [SerializeField] private GameObject _iconSubstrate;
    [Space]
    [SerializeField] private Image _checkImage;
    [SerializeField] private Sprite _availableIcon;
    [SerializeField] private Sprite _notAvailableIcon;

    public void Fill(string matName, Sprite matIcon, float neededCount, float availableCount)
    {
        //_materialsIcon.gameObject.SetActive(true);
        _checkImage.gameObject.SetActive(true);
        _slash.SetActive(true);
        _iconSubstrate.SetActive(true);

        _nameText.text = matName;
        _materialsIcon.sprite = matIcon;
        _availableCount.text = availableCount.ToString();
        _neededCount.text = neededCount.ToString();

        if (availableCount >= neededCount)
        {
            _checkImage.sprite = _availableIcon;
        }
        else
        {
            _checkImage.sprite = _notAvailableIcon;
        }

    }

    public void FillEmpty()
    {
        //_materialsIcon.gameObject.SetActive(false);
        _checkImage.gameObject.SetActive(false);
        _slash.SetActive(false);
        _iconSubstrate.SetActive(false);

        _nameText.text = string.Empty;
        _availableCount.text = string.Empty;
        _neededCount.text = string.Empty;
    }
}