using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SpawnWaves : MonoBehaviour
{
    // UI
    private TextMeshProUGUI waveText;

    // Time
    [SerializeField] float currentTime = 0f;
    public float speedOverTime = 1f;
    float timeBetweenWaves = 2.5f;

    // Enemy
    [SerializeField] private List<Transform> spawns;
    [SerializeField] private GameObject enemyPrefab;
    private int howManyEnemies = 5;
    public List<EnemySO> enemyTypes;
    public List<GameObject> activeEnemies;

    // Wave
    [SerializeField] private bool isWaveFinished;
    [SerializeField] private bool isWaveDead;
    [SerializeField] private GameObject waveTextGameObject;
    private int waveNumber = 1;

    private void Awake()
    {
        waveText = waveTextGameObject.GetComponent<TextMeshProUGUI>();
    }

    void Start()
    {
        StartCoroutine(SpawnWave(5));
    }

    private void Update()
    {
        WaveSpawning();
        WaveUI();
        SpeedEnemiesOverTime();
    }

    private void WaveUI()
    {
        waveText.text = "Wave: " + waveNumber.ToString();
    }

    private void WaveSpawning()
    {
        if (isWaveFinished && currentTime == 0)
        {
            waveNumber++;

            StartCoroutine(SpawnWave(howManyEnemies++));
        }

        if (isWaveFinished && isWaveDead)
        {
            currentTime -= 1 * Time.deltaTime;

            if (currentTime <= 0)
            {
                currentTime = 0;
            }
        }

        if (isWaveFinished && activeEnemies.Count == 0)
        {
            isWaveDead = true;
        }
        else
        {
            isWaveDead = false;
        }
    }

    private IEnumerator SpawnWave(int enemyAmount)
    {
        isWaveFinished = false;

        for (int i = 0; i < enemyAmount; i++)
        {
            GameObject spawnedEnemy = Instantiate(enemyPrefab, spawns[Random.Range(0, spawns.Count)].position, Quaternion.identity);

            // Define enemy type
            if (enemyTypes[0].chanceToSpawn <= UnityEngine.Random.value) spawnedEnemy.GetComponent<EnemyScript>().enemySO = enemyTypes[0];
            else if (enemyTypes[1].chanceToSpawn <= UnityEngine.Random.value) spawnedEnemy.GetComponent<EnemyScript>().enemySO = enemyTypes[1];
            else if (enemyTypes[2].chanceToSpawn <= UnityEngine.Random.value) spawnedEnemy.GetComponent<EnemyScript>().enemySO = enemyTypes[2];
            else if (enemyTypes[3].chanceToSpawn <= UnityEngine.Random.value) spawnedEnemy.GetComponent<EnemyScript>().enemySO = enemyTypes[3];

            activeEnemies.Add(spawnedEnemy);

            yield return new WaitForSeconds(1);
        }

        isWaveFinished = true;
        currentTime = timeBetweenWaves;

        yield return null;
    }

    private void SpeedEnemiesOverTime()
    {
        speedOverTime += 0.01f * Time.deltaTime;
    }
}
