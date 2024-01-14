using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] GameObject[] enemyPrefabs;

    /*[SerializeField] int baseEnemies;
    [SerializeField] float spawnRate = 0.5f;
    [SerializeField] float timeBetweenWaves;
    [SerializeField] float enemyAmountScaling;*/
    int baseEnemies = 6;
    float spawnRate = 0.7f;
    float timeBetweenWaves = 5f;
    float enemyAmountScaling = 0.5f;

    public static UnityEvent onEnemyDestroy = new UnityEvent();

    int currentWave = 1;
    float timeSinceSpawn;
    float enemiesAlive;
    float enemiesLeftToSpawn;
    bool isSpawning = false;

    private void Awake()
    {
        onEnemyDestroy.AddListener(EnemyDestroyed);
    }
    void Start()
    {
        StartCoroutine(StartFirstWave());
    }

    IEnumerator StartFirstWave()
    {
        yield return new WaitForSeconds(6f); 
        StartCoroutine(StartWave());
    }
    void Update()
    {
        if (!isSpawning) return;

        timeSinceSpawn += Time.deltaTime;

        if (timeSinceSpawn >= (1f / spawnRate) && enemiesLeftToSpawn > 0)
        {
            SpawnEnemy();
            enemiesLeftToSpawn--;
            enemiesAlive++;
            timeSinceSpawn = 0f;
        }
        if (enemiesAlive == 0 && enemiesLeftToSpawn == 0)
        {
            EndWave();
        }
    }
    void EnemyDestroyed()
    {
        enemiesAlive--;
    }
    IEnumerator StartWave()
    {
        yield return new WaitForSeconds(timeBetweenWaves);
        isSpawning = true;
        enemiesLeftToSpawn = EnemiesPerWave();
    }
    void EndWave()
    {
        isSpawning = false;
        timeSinceSpawn = 0f;
        StartCoroutine(StartWave());
    }
    void SpawnEnemy()
    {
        GameObject prefabToSpawn = enemyPrefabs[0];
        Instantiate(prefabToSpawn, Waypoints.main.startWaypoint.position, Quaternion.identity);
    }
    int EnemiesPerWave()
    {
        return Mathf.RoundToInt(baseEnemies * Mathf.Pow(currentWave, enemyAmountScaling));
    }
}
