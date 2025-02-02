using DragonBones;
using System.Collections;
using System.Timers;
using UnityEngine;

public class HeroAnimator : MonoBehaviour
{
    [SerializeField] private UnityArmatureComponent _armatureFront;
    [SerializeField] private UnityArmatureComponent _armatureBack;
    [SerializeField] private UnityArmatureComponent _armatureLeft;
    [SerializeField] private UnityArmatureComponent _armatureRight;
    [SerializeField] private UnityArmatureComponent _armatureIdle;
    [SerializeField] private HeroMove _heroMove;
    [SerializeField] private float _transitionTime = 0.2f;

    private UnityArmatureComponent _currentArmature;
    private Vector2 _lastInput = Vector2.zero;

    private void Start()
    {
        _heroMove.Mobilize();
        _currentArmature = _armatureIdle;
    }

    private void LateUpdate()
    {
        if (_heroMove.InputVector2.x > 0)
        {
            CrossfadeArmatures(_currentArmature, _armatureRight, _transitionTime);
        }
        else if (_heroMove.InputVector2.x < 0)
        {
            CrossfadeArmatures(_currentArmature, _armatureLeft, _transitionTime);
        }
        else
        {
            if (_heroMove.InputVector2.y > 0)
            {
                CrossfadeArmatures(_currentArmature, _armatureBack, _transitionTime);
            }
            else if (_heroMove.InputVector2.y < 0)
            {
                CrossfadeArmatures(_currentArmature, _armatureFront, _transitionTime);
            }
            else
            {
                CrossfadeArmatures(_currentArmature, _armatureIdle, _transitionTime);
            }
        }
    }

    public void CrossfadeArmatures(UnityArmatureComponent oldArmature, UnityArmatureComponent newArmature, float duration)
    {
        StartCoroutine(FadeArmatures(oldArmature, newArmature, duration));
    }

    private IEnumerator FadeArmatures(UnityArmatureComponent oldArmature, UnityArmatureComponent newArmature, float duration)
    {
        float time = 0;
        _currentArmature = newArmature;

        newArmature.gameObject.SetActive(true);
        SetArmatureAlpha(newArmature, 0);

        while (time < duration)
        {
            time += Time.deltaTime;
            float alpha = time / duration;

            SetArmatureAlpha(oldArmature, 1 - alpha);
            SetArmatureAlpha(newArmature, alpha);

            yield return null;
        }

        oldArmature.gameObject.SetActive(false);
    }

    private void SetArmatureAlpha(UnityArmatureComponent armature, float alpha)
    {
        foreach (Slot slot in armature.armature.GetSlots())
        {
            if (slot.display != null && slot.display is MeshRenderer meshRenderer)
            {
                Color color = meshRenderer.material.color;
                color.a = alpha;
                meshRenderer.material.color = color;
            }
        }
    }
}
