using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Wave 
{
    [Header("Required")]
    public GameObject enemy;
    public int count;
    public float spawnRate;

    [Header("Optional")]
    public GameObject boss;


}
