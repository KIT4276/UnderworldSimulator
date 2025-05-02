using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OverDecorSlot : MonoBehaviour
{


    [SerializeField] private Image _iconOfParam;
    [SerializeField] private TMP_Text _nameOfParam;
    [SerializeField] private TMP_Text _paramValue;
    [Space]
    [SerializeField] private GameObject _decorSlotPrefab;

    private List<DecorSlot> _decorSlots = new();

    public void Fill(Decor[] decorsIn, ParameterData parameterData, Parameter parameter, RoomsSystem roomsSystem)
    {
        if (_decorSlots.Count == 0)
        {
            _decorSlots.Add(_decorSlotPrefab.GetComponent<DecorSlot>());
        }

        List<Decor> decors = new();

        foreach (Decor decor in decorsIn)
        {
            foreach (var param in decor.Parameters.Parameters)
            {
                if (param.ParameterType == parameter.ParameterType)
                {
                    if (param.Value > 0)
                    {
                        decors.Add(decor);
                        break;
                    }
                }
            }
        }


        FillHead(parameter, roomsSystem);

        int currentSlotCount = _decorSlots.Count;
        int decorCount = decors.Count;

        if (decorCount == 0)
        {
            _decorSlots[0].gameObject.SetActive(false);
            RemovingExtraSlots(currentSlotCount, 1);
            return;
        }

        int i = 0;
        i = FillingExistingSlots(decors, parameterData, parameter, currentSlotCount, decorCount, i);
        i = CreateIfNotEnoughSlots(decors, parameterData, parameter, decorCount, i);
        RemovingExtraSlots(_decorSlots.Count, decorCount);
    }

    private void FillHead(Parameter parameter, RoomsSystem roomsSystem)
    {
        _iconOfParam.sprite = parameter.DecorParamIcon;
        _nameOfParam.text = parameter.Name;

        foreach (var param in roomsSystem.SelectedRoom.SetOfParameters.Parameters)
        {
            if (param.ParameterType == parameter.ParameterType)
            {
                _paramValue.text = param.Value.ToString();
                break;
            }
        }
    }

    private RoomParameter FindParam(Parameter parameter, Decor decor)
    {
        foreach (var param in decor.Parameters.Parameters)
        {
            if (param.ParameterType == parameter.ParameterType)
            {
                return param;
            }
        }
        return null;
    }

    private DecorSlot SpawnSlot()
    {
        var obj = Instantiate(_decorSlotPrefab, this.gameObject.transform);
        obj.SetActive(true);
        var slot = obj.GetComponent<DecorSlot>();
        _decorSlots.Add(slot);
        return slot;
    }

    private void RemovingExtraSlots(int currentSlotCount, int decorCount)
    {
        for (int j = currentSlotCount - 1; j >= decorCount; j--)
        {
            if (j == 0)
            {
                _decorSlots[j].gameObject.SetActive(false);
            }
            else
            {
                Destroy(_decorSlots[j].gameObject);
                _decorSlots.RemoveAt(j);
            }
        }
    }

    private int CreateIfNotEnoughSlots(List<Decor> decors, ParameterData parameterData, Parameter roomParameter, int decorCount, int i)
    {
        for (; i < decorCount; i++)
        {
            DecorSlot newSlot = SpawnSlot();
            newSlot.FillSlot(decors[i], parameterData, FindParam(roomParameter, decors[i]));
        }

        return i;
    }

    private int FillingExistingSlots(List<Decor> decors, ParameterData parameterData, Parameter roomParameter, int currentSlotCount, int decorCount, int i)
    {
        for (; i < Mathf.Min(currentSlotCount, decorCount); i++)
        {
            _decorSlots[i].gameObject.SetActive(true);
            _decorSlots[i].FillSlot(decors[i], parameterData, FindParam(roomParameter, decors[i]));
        }

        return i;
    }
}
