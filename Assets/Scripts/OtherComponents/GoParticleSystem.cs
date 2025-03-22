using System;
using System.Linq;
using UnityEngine;

public class GoParticleSystem : MonoBehaviour
{
    [SerializeField]private ParticleSystem[] _particleSystem;

    public void GoAnimate()
    {
        foreach (var particle in _particleSystem)
        {
            particle.Play();
        }
    }

}
