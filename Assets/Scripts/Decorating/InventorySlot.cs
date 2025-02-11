using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(ButtonEnterChangeImage), typeof(InventorySlotCounter))]
public class InventorySlot : MonoBehaviour
{
    [SerializeField] private Image _buttonIconImage;
    [SerializeField] private ButtonEnterChangeImage _buttonEnterChangeImage;
    [SerializeField] private Button _button;
    [Space]
    [SerializeField] private TMP_Text _textMeshPro;
    [SerializeField] private GameObject _x_TextTablet;

    public bool IsOccupied { get; private set; }
    public List<Decor> Decors { get; private set; }

    public event Action OnInitialized;

    private Sprite _icon;

    private void Awake()
    {
        //if (Decors == null) return;
        //SettingParameters();
        Debug.Log("Awake");
    }

    public void Initialized()
    {
        Decors = new List<Decor>();
        SettingParameters();
        OnInitialized?.Invoke();
    }

    public Decor TakeLastDecor()
    {
        var decor = Decors[^1];
        Decors.Remove(Decors[^1]);
        CheckingAndShow();
        return decor;
    }

    public Decor GetLastDecor()
    {
        if (Decors == null)
            Initialized();
        var decor = Decors[^1];
        return decor;
    }

    private void SettingParameters()
    {
        if (IsOccupied)
        {
            _button.interactable = true;
            _button.enabled = true;
            _buttonIconImage.gameObject.SetActive(true);
            _buttonEnterChangeImage.enabled = true;
            _buttonEnterChangeImage.Activate();
            _buttonIconImage.sprite = _icon;
        }
        else
        {
            Deactivate();
        }
        CheckingAndShow();
    }

    public void SetDecor(Decor decor)
    {
        IsOccupied = true;
        _icon = decor.GetIcon();
        Decors.Add(decor);
        SettingParameters();
        CheckingAndShow();
    }

    private void Deactivate()
    {
        Debug.Log("Deactivate");
        _buttonEnterChangeImage.enabled = false;
        IsOccupied = false;
        _buttonIconImage.sprite = null;
        _buttonEnterChangeImage.DeActivate();
        _buttonIconImage.gameObject.SetActive(false);
        _button.interactable = false;
        _icon = null;
    }

    private void CheckingAndShow()
    {
        if (Decors.Count > 1)
        {
            _textMeshPro.gameObject.SetActive(true);
            _x_TextTablet.SetActive(true);
            _textMeshPro.text = Decors.Count.ToString();
        }
        else
        {
            _textMeshPro.gameObject.SetActive(false);
            _x_TextTablet.SetActive(false);

            if (Decors.Count <= 0)
            {
                Deactivate();
            }
        }
    }
}