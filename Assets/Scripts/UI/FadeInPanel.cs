using UnityEngine;

public class FadeInPanel : LoadingCurtain
{
   // [SerializeField] private float _fadeDelay = 2;
    [SerializeField] private GameObject _gameObject;

    private void Start()
    {
        _gameObject.SetActive(false);
    }

    public override void Show()
    {
        _gameObject.SetActive(true);
        _curtain.alpha = 1;

    }

    protected override void OffGameObject()
    {

        _gameObject.SetActive(false);
    }

}
