using System;
using UnityEngine;

public class IntroState : IState
{
    private const string IntroScene = "Intro";
    private readonly SceneLoader _sceneLoader;
    private readonly LoadingCurtain _curtain;

    public IntroState( SceneLoader sceneLoader, LoadingCurtain curtain)
    {
        _sceneLoader = sceneLoader;
        _curtain = curtain;
    }
    
    public void Enter()
    {
        _sceneLoader.Load(IntroScene, OnLoaded);
    }

    private void OnLoaded()
    {
        _curtain.Hide();
    }

    public void Exit()
    {
    }
}

    