using _Game.Scripts.Enemies;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Enemies:")]
    [SerializeField]
    private List<GameObject> possibleEnemies;

    [Header("Settings:")]
    [SerializeField]
    private int maxEnemyCount;
    [SerializeField]
    private float spawnRadius;

    private int currentEnemiesAlive = 0;

    private void Awake()
    {
        InitializeAltar();

        StartCoroutine(CheckEnemyCount());
    }

    private void InitializeAltar()
    {
        for (int i = maxEnemyCount; i >= 0; i--)
        {
            SpawnEnemy();
        }
    }

    private void SpawnEnemy()
    {
        Vector2 spawnPoint = (Vector2)transform.position + Random.insideUnitCircle * spawnRadius;

        GameObject spawnedEnemy = Instantiate(possibleEnemies[Random.Range(0, possibleEnemies.Count)], spawnPoint, Quaternion.identity);

        EnemyLife enemyLife = spawnedEnemy.GetComponent<EnemyLife>();

        enemyLife.sourceSpawner = this;

        currentEnemiesAlive++;
    }

    public void OnSpawnedEnemyDies()
    {
        currentEnemiesAlive--;
    }

    IEnumerator CheckEnemyCount()
    {
        while (true)
        {
            if(currentEnemiesAlive < maxEnemyCount)
            {
                SpawnEnemy();
            }

            yield return new WaitForSeconds(Random.Range(30, 120));
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, spawnRadius);
    }
}
