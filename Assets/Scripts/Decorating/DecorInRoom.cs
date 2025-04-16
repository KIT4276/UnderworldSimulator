using UnityEngine;

public class DecorInRoom : MonoBehaviour
{
    [SerializeField] private DecorSlot[] _decorSlots;

    public void Fill(Decor[] decors, ParameterData parameterData)
    {
        int i = 0;

        for (; i < decors.Length; i++)
        {
            if (i < _decorSlots.Length)
            {
                _decorSlots[i].FillSlot(decors[i], parameterData);
            }
            else
            {
                Debug.Log(" Не хватает слотов в DecorInRoom");
            }
        }

        if(decors.Length< _decorSlots.Length)
        {
            for (; i < _decorSlots.Length; i++)
            {
                _decorSlots[i].FillEmpty();
            }
        }
    }
}
