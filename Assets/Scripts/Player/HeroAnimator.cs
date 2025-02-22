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
    private StateMachine _stateMachine;
    private PersistantStaticData _staticData;

    private const string StandName = "stand";
    private const string WalkName = "walk";
    private const string LootName = "loot_animation";

    public void Initialize(StateMachine stateMachine, PersistantStaticData staticData)
    {
        _stateMachine = stateMachine;
        _staticData = staticData;
    }

    private void Update()
    {
        Debug.Log(_currentArmature.animationName);
    }

    public void PlayLoot()
    {
        StartCoroutine(LootRoutine());
    }

    private IEnumerator LootRoutine()
    {
        //Debug.Log(_armatureFront.animationName);
        var predioslyAnimation = _armatureFront.animationName;
        _currentArmature.animation.Play(LootName);
        //Debug.Log(_armatureFront.animationName);
        yield return new WaitForSeconds(_staticData.LootInteractTime);
        _currentArmature.animation.Play(predioslyAnimation);
    }

    private void Start()
    {
        _heroMove.Mobilize();
        _currentArmature = _armatureFront;
        _currentArmature.animation.Play(StandName);
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
                if (_currentArmature.armature.animation.lastAnimationName == StandName) return;
                _currentArmature.armature.animation.Play(StandName);
            }
        }
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
