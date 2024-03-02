using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Alf.Utils
{

[RequireComponent(typeof(ParticleSystem))]
public class ParticleSelfDestruct : MonoBehaviour
{
    
    ParticleSystem _particlesystem;

    void Awake()
    {
        _particlesystem = GetComponent<ParticleSystem>();
    }

    void Update()
    {
        if(!_particlesystem.isPlaying)
            Destroy(gameObject);
    }

}
}