using System;
using System.Collections;
using UnityEngine;

public class DecorClickHandler
{
    private readonly DecorHolder _decorHolder;
    private readonly ICoroutineRunner _coroutineRunner;

    public DecorClickHandler(DecorHolder decorHolder, ICoroutineRunner coroutineRunner)
    {
        _decorHolder = decorHolder;
        _coroutineRunner = coroutineRunner;
        _decorHolder.InstallDecor += OnDecorInstalled;
    }

    private void OnDecorInstalled(Decor decor)
    {
        foreach(var installedDecor in _decorHolder.InstalledDecor)
        {
            installedDecor.BanOnPick();
        }
        _coroutineRunner.StartCoroutine(BunDelay());
    }

    private IEnumerator BunDelay()
    {
        yield return new WaitForSeconds(0.5f);

        foreach (var installedDecor in _decorHolder.InstalledDecor)
        {
            installedDecor.AllowOnPick();
        }
    }
}