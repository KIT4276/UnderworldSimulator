using DragonBones;
using UnityEngine;

public class HeroAnimator : MonoBehaviour
{
    [SerializeField] private UnityArmatureComponent _armatureFront;
    [SerializeField] private UnityArmatureComponent _armatureBack;
    [SerializeField] private UnityArmatureComponent _armatureLeft;
    [SerializeField] private UnityArmatureComponent _armatureRight;
    [SerializeField] private UnityArmatureComponent _armatureIdle;
    [SerializeField] private HeroMove _heroMove;

    private UnityArmatureComponent _currentArmature;

    private void Start()
    {
        _heroMove.Mobilize();
        _currentArmature = _armatureIdle;
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
                CrossArmatures(_armatureIdle);
            }
        }
    }

    public void CrossArmatures(UnityArmatureComponent newArmature)
    {
        if(_currentArmature == newArmature) return;

        _currentArmature.gameObject.SetActive(false);
        newArmature.gameObject.SetActive(true);
        _currentArmature = newArmature;
    }
}
