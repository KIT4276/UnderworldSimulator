using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class LootInteract : InteractableObstacle
{
   // [SerializeField] private float _interactionTime = 1;
    [SerializeField] private GameObject _progressBar;
    [SerializeField] private Image _bar;
    [SerializeField] private Loot _loot;

    [Inject] private LootSystem _lootSystem;
    [Inject] private PersistantStaticData _staticData;

    private Coroutine _interactionCoroutine;

    private void Start()
    {
        _bar.fillAmount = 0;
        _progressBar.SetActive(false);
    }

    protected override void Interac()
    {
        _progressBar.SetActive(true);
        

        if (_interactionCoroutine == null)
        {
            _hero.GetHero.OnLoot();
            _interactionCoroutine = StartCoroutine(InteractionProgress());
        }

        foreach (var lootSetting in _loot.LootSettings)
        {
            _lootSystem.FillSlot(lootSetting.Loot, lootSetting.Count, this. gameObject);
        }

    }

    private IEnumerator InteractionProgress()
    {
        _hero.GetHero.Immobilize();

        float elapsedTime = 0f;
        _bar.fillAmount = 0f;

        while (elapsedTime < _staticData.LootInteractTime)
        {
            elapsedTime += Time.deltaTime;
            _bar.fillAmount = Mathf.Clamp01(elapsedTime / _staticData.LootInteractTime);
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
