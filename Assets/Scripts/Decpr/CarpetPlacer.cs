public class CarpetPlacer : DecorPlacer
{
    protected override bool CheckOtherDecor(bool isInside)
    {
        return isInside;
    }
}