using System;
using UnityEngine;

public class CompositeLoot : MonoBehaviour 
{
    [SerializeField] private LootInteract _lootInteract;

    public Action MiniGameOpen;

    private void Awake()
    {
        _lootInteract.SetComposite();

        _lootInteract.Interacted += OpenMiniGame;
    }


    private void OpenMiniGame()
    {
        MiniGameOpen?.Invoke();
    }
}
