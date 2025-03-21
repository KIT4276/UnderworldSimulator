using DragonBones;
using System.Collections;
using UnityEngine;

public class AnimatedLootInteract : LootInteract
{
    [SerializeField] private UnityArmatureComponent _armature;

    public override void Restart()
    {
        base.Restart();
        _armature.animation.Play("tree_idle", 0);
        StartAnimate();
    }

    private void StartAnimate() 
    {
        StopAllCoroutines();
        StartCoroutine(RespawnRoutine());
    }

    protected override IEnumerator RespawnRoutine()
    {
        yield return new WaitForSeconds(_respawnTime);
        Debug.Log("tree_sneeze");
        _armature.AddDBEventListener(EventObject.COMPLETE, OnMoveAnimationComplete);
        _armature.animation.Play("tree_sneeze", 1);
       
    }

    private void OnMoveAnimationComplete(string type, EventObject eventObject)
    {
        Debug.Log("OnMoveAnimationComplete");
        _interactiveObject.SetActive(true); 
        Restart();
        StartAnimate();
    }
}

