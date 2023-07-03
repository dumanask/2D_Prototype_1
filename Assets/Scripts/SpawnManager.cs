using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class EnemyStructure
{
    [HideInInspector] public string Name;
    public GameObject enemyType;
    public float timeToNextEnemy;
}

[System.Serializable]
public class Waves
{
    [HideInInspector] public string Name;
    [NonReorderable]
    public EnemyStructure[] enemiesInWave;
}

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject gameManagerObject;
    GameManager gameManager;
    [Header("Waves")]
    [SerializeField]
    private int currentWave;
    [SerializeField]
    private bool spawning;
    [SerializeField]
    private Waves[] totalWaves;

    [Header("Setup")]
    [SerializeField]
    private Transform[] spawnPoints;
    [SerializeField]
    private Transform enemyHolder;

    [Header("Properties")]
    [SerializeField]
    private float timeBetweenWaves;

    private float waveCountdown;


    private void OnValidate()
    {
        RewriteArrays();
    }
    void RewriteArrays()
    {
        for (int i = 0; i < totalWaves.Length; i++)
        {
            totalWaves[i].Name = "Wave " + (i + 1);
            for (int x = 0; x < totalWaves[i].enemiesInWave.Length; x++)
            {
                totalWaves[i].enemiesInWave[x].Name = "Enemy " + (x + 1);
            }
        }
    }

    private void Start()
    {
        gameManager = gameManagerObject.gameObject.GetComponent<GameManager>();
        Transform transform = GetComponent<Transform>();
        spawning = false;
        waveCountdown = timeBetweenWaves;
        currentWave = 0;
    }

    private void Update()
    {
        if (gameManager.currentState == GameState.Playing)
        {
            if (!spawning)
            {
                waveCountdown -= Time.deltaTime;
                if (waveCountdown <= 0)
                {
                    currentWave++;
                    StartCoroutine(SpawnWave());
                }
            }
        }
    }

    IEnumerator SpawnWave()
    {
        int waveIndex = currentWave;
        spawning = true;
        waveCountdown = timeBetweenWaves;
        for (int i = 0; i < totalWaves[waveIndex - 1].enemiesInWave.Length; i++)
        {
            SpawnEnemy(totalWaves[waveIndex - 1].enemiesInWave[i].enemyType);
            yield return new WaitForSeconds(totalWaves[waveIndex - 1].enemiesInWave[i].timeToNextEnemy);
        }
        spawning = false;
    }

    void SpawnEnemy(GameObject enemy)
    {
        int randomIndex = Random.Range(0, spawnPoints.Length);
        Vector3 randomSpawnPosition = new Vector3(spawnPoints[randomIndex].position.x, spawnPoints[randomIndex].position.y, spawnPoints[randomIndex].position.z);
        transform.position = randomSpawnPosition;
        GameObject spawnedEnemy = Instantiate(enemy, transform.position, Quaternion.identity);
        spawnedEnemy.transform.SetParent(enemyHolder);
    }
}
