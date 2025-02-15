﻿using UnityEngine;

public class AssetsProvider : IAssets
{
    public GameObject Instantiate(string path)
    {
        var prefab = Resources.Load<GameObject>(path);
        return Object.Instantiate(prefab);
    }

    public GameObject Instantiate(string path, Vector3 position)
    {
        var prefab = Resources.Load<GameObject>(path);
        
        return Object.Instantiate(prefab, position, Quaternion.identity);
    }
}
