using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] GameObject[] enemyPrefabs;

    [SerializeField] int baseEnemies;
    [SerializeField] int spawnRate;
    [SerializeField] int timeBetweenWaves;
    [SerializeField] int enemyAmmoutScaling;

    int currentWave = 1;
    float timeSinceSpawn;
    float enemiesAlive;
    float enemiesLeftToSpawn;
    bool isSpawning = false;

    void Start()
    {

    }

    void Update()
    {
        if (!isSpawning) return;

        timeSinceSpawn += Time.deltaTime;

        if (timeSinceSpawn >= (1f / spawnRate))
        {
            Debug.Log("Spawn enemy");
        }
    }
    void StartWave()
    {
        isSpawning = true;
        enemiesLeftToSpawn = EnemiesPerWave();
    }
    int EnemiesPerWave()
    {
        return Mathf.RoundToInt(baseEnemies * Mathf.Pow(currentWave, enemyAmmoutScaling));
    }
}
