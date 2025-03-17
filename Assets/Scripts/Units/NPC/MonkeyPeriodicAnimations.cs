using UnityEngine;

public class MonkeyPeriodicAnimations : NPCPeriodicAnimations
{
    [SerializeField] private int _repetitsNumber = 5;

    private readonly string[] _moves = new string[] { "idel_animation_01", "idel_animation_02" };

    protected override void AnimateMove()
    {
        _move = _moves[Mathf.RoundToInt(UnityEngine.Random.Range(0, _moves.Length))];
        _armatureComponent.animation.FadeIn(_move, _fadeInTime, _repetitsNumber);
    }
}
