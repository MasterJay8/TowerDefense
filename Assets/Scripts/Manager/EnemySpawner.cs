using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.Events;
using TMPro;

public class EnemySpawner : MonoBehaviour
{
    public static EnemySpawner main;
    [SerializeField] GameObject[] enemyPrefabs;
    [SerializeField] TextMeshProUGUI countdownUI;
    [SerializeField] TextMeshProUGUI waveUI;
    [SerializeField] LayerMask layerMask;
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

    public int currentWave = 1;
    float timeSinceSpawn;
    float enemiesAlive;
    float enemiesLeftToSpawn;
    bool isSpawning = false;

    int[] prefabIndex = new int[2];

    //Waypoits
    [SerializeField] Waypoints[] Wpoints;

    private void Awake()
    {
        enemySpawnerScript = this;
        onEnemyDestroy.AddListener(EnemyDestroyed);
    }
    void Start()
    {
        main = this;
        StartCoroutine(StartFirstWave());
    }

    IEnumerator StartFirstWave()
    {
        while (timeBetweenWaves > 0)
        {
            countdownUI.text = Mathf.CeilToInt(timeBetweenWaves).ToString(); //time before next wave
            yield return new WaitForSeconds(1f);
            timeBetweenWaves -= 1f;
        }
        //countdownUI.text = "";
        countdownUI.enabled = false;
        StartCoroutine(StartWave());
    }
    void Update()
    {
        if (!isSpawning) return;

        timeSinceSpawn += Time.deltaTime;

        if (timeSinceSpawn >= (1f / spawnRate) && enemiesLeftToSpawn > 0)
        {
            SpawnEnemy();
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
        countdownUI.text = Mathf.CeilToInt(timeBetweenWaves).ToString();
        yield return new WaitForSeconds(timeBetweenWaves);

        CheckForTowers();
        //Debug.Log(currentWave);
        //Debug.Log(prefabIndex[0]);
        //Debug.Log(prefabIndex[1]);

        isSpawning = true;
        enemiesLeftToSpawn = EnemiesPerWave();
    }
    void EndWave()
    {
        isSpawning = false;
        timeSinceSpawn = 0f;
        currentWave++;
        waveUI.text = "Wave: " + currentWave.ToString();
        StartCoroutine(StartWave());
    }
    void SpawnEnemy()
    {
        if (currentWave < 4)
        {
            Instantiate(enemyPrefabs[2], Wpoints[0].startWaypoint.position, Quaternion.identity);
            enemiesLeftToSpawn -= 2;
        }
        else
        {
            GameObject enemyObject = Instantiate(enemyPrefabs[prefabIndex[0]], Wpoints[0].startWaypoint.position, Quaternion.identity);
            enemyObject.GetComponent<Enemy>().Initialize(0);
            if (prefabIndex[0] == 2) enemiesLeftToSpawn -= 2;
            else enemiesLeftToSpawn--;

            GameObject enemyObject2 = Instantiate(enemyPrefabs[prefabIndex[1]], Wpoints[1].startWaypoint.position, Quaternion.identity);
            enemyObject2.GetComponent<Enemy>().Initialize(1);
            if (prefabIndex[1] == 2) enemiesLeftToSpawn -= 2;
            else enemiesLeftToSpawn--;

        }
    }
    int EnemiesPerWave()
    {
        return Mathf.RoundToInt(baseEnemies * Mathf.Pow(currentWave, enemyAmountScaling));
    }
    void CheckForTowers()
    {
        for (int i = 0; i < Wpoints.Length; i++)
        {
            Waypoints waypoints = Wpoints[i];
            int[] towerCounts = new int[enemyPrefabs.Length];

            foreach (Transform waypoint in waypoints.waypoints)
            {
                Collider2D[] colliders = Physics2D.OverlapCircleAll(waypoint.position, 1);

                foreach (Collider2D collider in colliders)
                {
                    if (collider.CompareTag("Tower"))
                    {
                        if (collider.transform.name == "Tower 1(Clone)")
                        {
                            towerCounts[0]++;
                        }
                        else if(collider.transform.name == "Tower 2(Clone)")
                        {
                            towerCounts[1]++;
                        }
                        else if (collider.transform.name == "Tower 3(Clone)")
                        {
                            towerCounts[2]++;
                        }
                    }
                }
            }
            int mostTypeTowerIndex = towerCounts.ToList().IndexOf(towerCounts.Max());
            prefabIndex[i] = mostTypeTowerIndex;
        }
    }

}
