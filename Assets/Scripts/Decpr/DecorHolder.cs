using System.Collections.Generic;

public class DecorHolder : ISavedProgress
{
    public List <Decor> InstalledDecor { get; private set; }

    public DecorHolder()
    {
        InstalledDecor = new List <Decor>();
    }

    public void InstallDecor(Decor decor)
        => InstalledDecor.Add(decor);

    public void UnInstallDecor(Decor decor)
        => InstalledDecor.Remove(decor);

    public void UpdateProgress(PlayerProgress progress)
    {
        
        foreach (var decor in InstalledDecor)
        {
            progress.DecorsData.Add(new DecorData(decor));
        } 
    }

    public void LoadProgress(PlayerProgress progress)
    {
       foreach(var decorData in progress.DecorsData)
        {
            //todo instantiate all decor on level
        }
    }
}
