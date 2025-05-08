using System;
using UnityEngine;

public class DecorInRoom : ScrollViewMenuRebootable
{
    [SerializeField] private OverDecorSlot[] _overDecorSlots;
   

    public void Fill(Decor[] decors, ParameterData parameterData, RoomsSystem roomsSystem)
    {
        ResetScroll();
        for (var i = 0; i < parameterData.Parameters.Length; i++)
        {
            Parameter param = parameterData.Parameters[i];
            _overDecorSlots[i].Fill(decors, parameterData, param, roomsSystem);
        }
    }

    
}
