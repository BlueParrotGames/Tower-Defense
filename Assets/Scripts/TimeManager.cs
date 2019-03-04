using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    [SerializeField] [Range(0f, 2f)] float timeSpeed = 1f;

    void Update()
    {
        Time.timeScale = timeSpeed;
    }
}
