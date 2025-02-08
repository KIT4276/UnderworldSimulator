using System.Collections.Generic;
using UnityEngine;

public class DecorHolder : ISavedProgress
{
    public List<Decor> InstalledDecor { get; private set; }
    public Decor ActiveDecor { get; private set; }

    public DecorHolder()
    {
        InstalledDecor = new List<Decor>();
    }

    public List<Decor> GetDecorsInScene()
    {
        List<Decor> allDecorsInScene = new List<Decor>();

        foreach (var decor in InstalledDecor)
        {
            allDecorsInScene.Add(decor);
        }

        if (ActiveDecor != null)
            allDecorsInScene.Add(ActiveDecor);


        return allDecorsInScene;
    }

    public void SetActiveDecor(Decor decor)
    {
        ActiveDecor = decor;

        if (InstalledDecor.Contains(decor))
            InstalledDecor.Remove(decor);
    }

    public void AddInstalledDecor(Decor decor)
    {
        ActiveDecor = null;
        InstalledDecor.Add(decor);
    }

    public void DeActiveDecor()
    {
        if (InstalledDecor.Contains(ActiveDecor))
            InstalledDecor.Remove(ActiveDecor);

        ActiveDecor = null;
    }

    public void UpdateProgress(PlayerProgress progress)
    {

        foreach (var decor in InstalledDecor)
        {
            progress.DecorsData.Add(new DecorData(decor));
        }
    }

    public void LoadProgress(PlayerProgress progress)
    {
        foreach (var decorData in progress.DecorsData)
        {
            //todo instantiate all decor on level
        }
    }
}
