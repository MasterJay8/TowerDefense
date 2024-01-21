using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.Events;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] GameObject[] enemyPrefabs;
    public static EnemySpawner enemySpawnerScript;


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

    //Dmg type
    int currentWaveType = 0;
    public int[] damageType;
    //public int pierce = 0;
    //public int electric = 0;

    private void Awake()
    {
        damageType = new int[enemyPrefabs.Length]; //Create array based on enemy prefab number
        enemySpawnerScript = this;
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
            if (currentWaveType == 1)enemiesLeftToSpawn -= 2;
            else enemiesLeftToSpawn--;
            enemiesAlive++;
            timeSinceSpawn = 0f;
        }
        if (enemiesAlive == 0 && enemiesLeftToSpawn <= 0)
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

        //Choose highest dmg type
        if (currentWave >= 4) currentWaveType = damageType.ToList().IndexOf(damageType.Max());
        Debug.Log(currentWaveType);
        for (int i = 0; i < damageType.Length; i++) damageType[i] = 0;

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
        GameObject prefabToSpawn = enemyPrefabs[currentWaveType];
        Instantiate(prefabToSpawn, Waypoints.main.startWaypoint.position, Quaternion.identity);
    }
    int EnemiesPerWave()
    {
        return Mathf.RoundToInt(baseEnemies * Mathf.Pow(currentWave, enemyAmountScaling));
    }
}
