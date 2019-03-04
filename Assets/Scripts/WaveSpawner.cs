using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveSpawner : MonoBehaviour
{
    public static int enemyCount;

    public Wave[] waves;

    [SerializeField] Transform enemyPrefab;
    [SerializeField] Transform startPoint;

    [SerializeField] float timeBetweenWaves = 5f; //1f = 1s

    float countdown = 2f;
    int waveIndex;

    [SerializeField] Text waveCountdownText;

    private void Start()
    {
        enemyCount = 0;
        InvokeRepeating("LevelStatus", 0, .1f);
    }

    private void Update()
    {
        if(enemyCount > 0)
            return;

        if (countdown <= 0f)
        {
            StartCoroutine(SpawnWave());
            countdown = timeBetweenWaves;
            return;
        }
        countdown -= Time.deltaTime;

        countdown = Mathf.Clamp(countdown, 0f, Mathf.Infinity);

        waveCountdownText.text = string.Format("{0:00.00}", countdown);
    }

    private void LevelStatus()
    {
        if (waveIndex == waves.Length && enemyCount == 0)
        {
            Debug.Log("Level done, not yet implemented.");
            CancelInvoke();
            this.enabled = false;
        }
    }

    IEnumerator SpawnWave()
    {
        PlayerStats.rounds++;

        Wave wave = waves[waveIndex];

        for (int i = 0; i < wave.count; i++)
        {
            SpawnEnemy(wave.enemy);
            yield return new WaitForSeconds(1f / wave.spawnRate);
        }

        waveIndex++;
    }

    void SpawnEnemy(GameObject enemy)
    {
        Instantiate(enemy, startPoint.position, startPoint.rotation);
        enemyCount ++;
    }
}