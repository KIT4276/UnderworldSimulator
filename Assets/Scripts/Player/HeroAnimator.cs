using DragonBones;
using System;
using System.Collections;
using UnityEngine;

public class HeroAnimator : MonoBehaviour
{
    [SerializeField] private UnityArmatureComponent _armatureFront;
    [SerializeField] private UnityArmatureComponent _armatureBack;
    [SerializeField] private UnityArmatureComponent _armatureLeft;
    [SerializeField] private UnityArmatureComponent _armatureRight;
    [SerializeField] private HeroMove _heroMove;


    private UnityArmatureComponent _currentArmature;
    //private StateMachine _stateMachine;
    private PersistantStaticData _staticData;

    private const string StandName = "stand";
    private const string WalkName = "walk";
    private const string LootName = "loot_animation";

    public void Initialize(StateMachine stateMachine, PersistantStaticData staticData)
    {
        _heroMove.Mobilize();
        _currentArmature = _armatureFront;
        _currentArmature.animation.Play(StandName);

        //_stateMachine = stateMachine;
        _staticData = staticData;
    }


    public void PlayLoot()
    {
        StartCoroutine(LootRoutine());
    }

    private IEnumerator LootRoutine()
    {
        var predioslyAnimation = _currentArmature.armature.animation.lastAnimationName;
        _currentArmature.animation.Play(LootName);
       // Debug.Log(_currentArmature.armature.animation.lastAnimationName);
        yield return new WaitForSeconds(_staticData.LootInteractTime);
        _currentArmature.animation.Play(predioslyAnimation);
       // Debug.Log(_currentArmature.armature.animation.lastAnimationName);
    }

    private void LateUpdate()
    {
        if (_heroMove.InputVector2.x > 0)
        {
            CrossArmatures(_armatureRight);

        }
        else if (_heroMove.InputVector2.x < 0)
        {
            CrossArmatures(_armatureLeft);
        }
        else
        {
            if (_heroMove.InputVector2.y > 0)
            {
                CrossArmatures(_armatureBack);
            }
            else if (_heroMove.InputVector2.y < 0)
            {
                CrossArmatures(_armatureFront);
            }
            else
            {
                //Debug.Log(_currentArmature);
                //Debug.Log(_currentArmature.armature);
                //Debug.Log(_currentArmature.animation);
                //Debug.Log(_currentArmature.animation.lastAnimationName);
                if (_currentArmature.armature.animation.lastAnimationName == LootName) return;
                if (_currentArmature.armature.animation.lastAnimationName == StandName) return;
                _currentArmature.animation.Play(StandName);
            }
        }

        //Debug.Log(_currentArmature.armature.animation.lastAnimationName);
    }

    public void CrossArmatures(UnityArmatureComponent newArmature)
    {
        if (_currentArmature == newArmature)
        {
            if (_currentArmature.armature.animation.lastAnimationName != WalkName)
                _currentArmature.animation.Play(WalkName);
            else
                return;
        }

        _currentArmature.gameObject.SetActive(false);
        newArmature.gameObject.SetActive(true);
        _currentArmature = newArmature;
        _currentArmature.animation.Play(WalkName);
    }
}
