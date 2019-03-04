﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointManager : MonoBehaviour
{
    public static Transform[] waypoints;

    private void Awake()
    {
        waypoints = new Transform[transform.childCount];

        for (int i = 0; i < waypoints.Length; i++)
        {
            waypoints[i] = transform.GetChild(i);
        }
    }

    /*
     * 
    private void Update()
    {
        for (int i = 0; i < waypoints.Length; i++)
        {
            Debug.Log(waypoints[i]);
        }
    }

    */

}

