using System;
using System.Collections;
using UnityEngine;
using Zenject;

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
        //todo
       // Debug.Log("OpenMiniGame");
        MiniGameOpen?.Invoke();
    }
}
