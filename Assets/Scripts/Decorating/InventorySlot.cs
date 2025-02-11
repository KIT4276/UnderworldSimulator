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
    //[SerializeField] private InventorySlotCounter _slotCounter;
    [SerializeField] private Button _button;
    [Space]
    [SerializeField] private TMP_Text _textMeshPro;
    [SerializeField] private GameObject _x_TextTablet;

    public bool IsOccupied { get; private set; }
    public List<Decor> Decors { get; private set; }

    public event Action OnInitialized;

    private Sprite _icon;

    //private void Awake()
    //{
    //    SettingParameters();
    //}

    public Decor TakeLastDecor()
    {
        var decor = Decors[^1];
        Decors.Remove(Decors[^1]);
        //Debug.Log(Decors.Count);
        CheckingAndShow();
        return decor;
    }

    public Decor GetLastDecor()
    {
        if (Decors == null)
            Initialized();
        var decor = Decors[^1];
        //Debug.Log(Decors.Count);
        return decor;
    }

    public void Initialized()
    {
        Decors = new List<Decor>();
        SettingParameters();
        OnInitialized?.Invoke();
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
       // Debug.Log("SetDecor " + decor.ID);
        IsOccupied = true;
        _icon = decor.GetIcon();
        Decors.Add(decor);
        SettingParameters();
       // Debug.Log(Decors.Count);
        CheckingAndShow();
    }

    private void Update()
    {
       // Debug.Log(Decors.Count);
    }

    public void Deactivate()
    {
        _buttonEnterChangeImage.enabled = false;
        IsOccupied = false;
        _buttonIconImage.sprite = null;
        _buttonEnterChangeImage.DeActivate();
        _buttonIconImage.gameObject.SetActive(false);
        _button.interactable = false;
        //CurrentDecor = null;
        _icon = null;
    }

    private void CheckingAndShow()
    {
        //_textMeshPro.gameObject.SetActive(true);
        //_x_TextTablet.SetActive(true);
        //_textMeshPro.text = Count.ToString();

        //if (_inventorySlot.Decors.Count > 1)
        //{
        _textMeshPro.gameObject.SetActive(true);
        _x_TextTablet.SetActive(true);
        _textMeshPro.text = Decors.Count.ToString();
        //}
        //else
        //{
        //    _textMeshPro.gameObject.SetActive(false);
        //    _x_TextTablet.SetActive(false);

        if (Decors.Count <= 0)
        {
            Deactivate();
        }
        //}
    }
}