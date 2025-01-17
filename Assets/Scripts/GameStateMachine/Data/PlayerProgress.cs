using System;
using System.Collections.Generic;

[Serializable]
public class PlayerProgress
{
    public State PlayerState;
    public WorldData WorldData;
    public Stats PlayerStats;
    public List<DecorData> DecorsData;

    public PlayerProgress(string initialLevel)
    {
        WorldData = new WorldData(initialLevel);
        PlayerState = new State();
        PlayerStats = new Stats();
        DecorsData = new List<DecorData>();
    }
}
