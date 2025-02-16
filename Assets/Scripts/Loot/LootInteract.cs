using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class LootInteract : InteractableObstacle
{
    [SerializeField] private float _interactionTime = 1;
    [SerializeField] private GameObject _progressBar;
    [SerializeField] private Image _bar;

    [Inject] private LootSystem _lootSystem;

    private Coroutine _interactionCoroutine;

    private void Start()
    {
        _bar.fillAmount = 0;
        _progressBar.SetActive(false);
    }

    protected override void Interac()
    {
        _progressBar.SetActive(true);
        _lootSystem.EnterLootState();

        if (_interactionCoroutine == null)
        {
            _interactionCoroutine = StartCoroutine(InteractionProgress());
        }
    }

    private IEnumerator InteractionProgress()
    {
        float elapsedTime = 0f;
        _bar.fillAmount = 0f;

        while (elapsedTime < _interactionTime)
        {
            elapsedTime += Time.deltaTime;
            _bar.fillAmount = Mathf.Clamp01(elapsedTime / _interactionTime);
            yield return null;
        }

        OpenMenu();
        _interactionCoroutine = null;
    }

    private void OpenMenu()
    {
        _bar.fillAmount = 1f;
        _lootSystem.OpenMenu();
        _progressBar.SetActive(false);
    }
}
