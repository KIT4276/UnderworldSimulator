public class BlackCurtain : LoadingCurtain
{
    public void SpeedHide()
    {
        _curtain.alpha = 0;
        OffGameObject();
    }
}
