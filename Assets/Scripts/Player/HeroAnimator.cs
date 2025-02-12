using DragonBones;
using System;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

public class HeroAnimator : MonoBehaviour
{
    [SerializeField] private UnityArmatureComponent _armatureFront;
    [SerializeField] private UnityArmatureComponent _armatureBack;
    [SerializeField] private UnityArmatureComponent _armatureLeft;
    [SerializeField] private UnityArmatureComponent _armatureRight;
    //[SerializeField] private UnityArmatureComponent _armatureIdle;
    [SerializeField] private HeroMove _heroMove;


    private UnityArmatureComponent _currentArmature;

    private void Start()
    {
        _heroMove.Mobilize();
        _currentArmature = _armatureFront;
        _currentArmature.animation.Play("stand");

        //_heroMove.MoveAction.performed += UpdateAnim;
    }

    private void LateUpdate()
    {
        //if (context.phase != InputActionPhase.Started || context.phase != InputActionPhase.Canceled) return;
        //Debug.Log("UpdateAnim");
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
                if (_currentArmature.armature.animation.lastAnimationName == "stand") return;
                _currentArmature.armature.animation.Play("stand");
            }
        }
    }

    private void FixedUpdate()
    {
        //Debug.Log(_currentArmature.armature.animation.lastAnimationName);
        
    }

    public void CrossArmatures(UnityArmatureComponent newArmature)
    {
        if (_currentArmature == newArmature)
        {
            if (_currentArmature.armature.animation.lastAnimationName != "walk")
                _currentArmature.animation.Play("walk");
            else
                return;
        }

            _currentArmature.gameObject.SetActive(false);
            newArmature.gameObject.SetActive(true);
            _currentArmature = newArmature;
            _currentArmature.animation.Play("walk");
    }
}
