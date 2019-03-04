using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildVFX : MonoBehaviour
{
    [SerializeField] GameObject[] vfxPrefab;
    [SerializeField] Transform firePoint;
    public void InstantiateParticles()
    {
        Debug.Log("Spawning Particles");

        foreach(GameObject v in vfxPrefab)
        {
            Instantiate(v, firePoint.position, Quaternion.identity);

        }
    }
}
