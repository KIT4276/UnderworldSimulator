using UnityEngine;

public class DecorFactory : MonoBehaviour
{
    public Decor SpawnDecor(Decor decorPrefab)
    {
        var decpr =  Instantiate(decorPrefab);
        decpr.Initialize();

        return decpr;
    }
}
