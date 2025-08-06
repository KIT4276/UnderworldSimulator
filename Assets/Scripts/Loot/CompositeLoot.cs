using System;
using UnityEngine;
using Zenject;

public class CompositeLoot : MonoBehaviour 
{
    [SerializeField] private LootInteract _lootInteract;

    private LootMiniGameUI _lootMiniGameUI;

    [Inject]
    private void Construct(LootMiniGameUI lootMiniGameUI)
    {
        
        _lootMiniGameUI = lootMiniGameUI;
    }

    private void Awake()
    {
        _lootInteract.SetComposite();

        _lootInteract.InteractedCompositeLoot += OpenMiniGame;
    }


    private void OpenMiniGame()
    {
        _lootMiniGameUI.OnInteracted(_lootInteract);
    }

    private void OnDisable()
    {
        _lootInteract.InteractedCompositeLoot -= OpenMiniGame;
    }
}
