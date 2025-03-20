public class RabbitPeriodicAnimations : NPCPeriodicAnimations
{
    private const string Move = "idle_animation";

    protected override void AnimateMove()
    {
        _move = Move;
        base.AnimateMove();
    }
}
