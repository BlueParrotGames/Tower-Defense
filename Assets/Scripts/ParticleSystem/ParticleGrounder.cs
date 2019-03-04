using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class ParticleGrounder : MonoBehaviour
{
    ParticleSystem p;

    void Start()
    {
        p = GetComponent<ParticleSystem>();
        InvokeRepeating("CheckStatus", 3, .2f);
    }

    void CheckStatus()
    {
        if(p.particleCount == 0)
        {
            Destroy(gameObject);
        }
    }
}
