using DragonBones;
using System;
using System.Collections;
using UnityEngine;

public class AnimatedLootInteract : LootInteract
{
    [SerializeField] private UnityArmatureComponent _armature;
    [SerializeField] private Collider2D _triggerCollider;
    [SerializeField] private GameObject _particles;

    public override void Restart()
    {
        base.Restart();
        _armature.animation.Play("tree_idle", 0);
    }
    public override void Despawn()
    {
        base.Despawn();
        _triggerCollider.enabled = false;
        _particles.SetActive(false);
    }

    protected override IEnumerator RespawnRoutine()
    {
        yield return new WaitForSeconds(_respawnTime);
        _armature.AddDBEventListener(EventObject.COMPLETE, OnMoveAnimationComplete);
        _armature.animation.Play("tree_sneeze", 1);
        //Debug.Log("tree_sneeze");
        StartCoroutine(Crutch());
    }

    private IEnumerator Crutch()
    {
        yield return new WaitForSeconds(8);
        _triggerCollider.enabled = true;
        _particles.SetActive(true);
    }

    private void OnMoveAnimationComplete(string type, EventObject eventObject)
    {
        //Debug.Log("OnMoveAnimationComplete");
        Restart();
    }
}

