using DragonBones;
using System.Collections;
using UnityEngine;

public class AnimatedLootInteract : LootInteract
{
    [SerializeField] private UnityArmatureComponent _armature;
    [SerializeField] private PeriodicAnimated _periodic;

    private const string LootTacen = "loot";
    private const string LootRespawned = "loot_rs";

    public override void Restart()
    {
        base.Restart();
        _periodic.StartAnimate(_armature);
    }


    protected override IEnumerator RespawnRoutine()
    {
        PlayAnimation(LootTacen);
        yield return new WaitForSeconds(_respawnTime);
        PlayAnimation(LootRespawned);
        Restart();
    }

    private void PlayAnimation(string name)
    {
        _armature.animation.Play(name, 1);
    }
}
