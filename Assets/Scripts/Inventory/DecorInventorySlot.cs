

public class DecorInventorySlot : InventorySlot
{
    public void SetDecor(Decor decor)
    {
        IsOccupied = true;
        _icon = decor.GetIcon();
        nventoryObject.Add(decor);
        SettingParameters();
        CheckingAndShow();
    }
}
