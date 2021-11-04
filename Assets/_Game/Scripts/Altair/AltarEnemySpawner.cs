using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AltarEnemySpawner : MonoBehaviour
{
    [Header("Enemies:")]
    [SerializeField]
    private GameObject bossEnemy;
    [SerializeField]
    private List<GameObject> possibleEnemies;

    [Header("Settings:")]
    [SerializeField]
    private List<int> enemiesCountPerWave = new List<int>();
    [SerializeField]
    private float spawnRadius;

    [Header("Visuals:")]
    [SerializeField]
    private AltarEnemyStateManager altarStateManager;
    [SerializeField]
    private AltarEnemySpawnEffect spawnMinionEffect;
    [SerializeField]
    private AltarEnemySpawnEffect spawnBossEffect;

    private int currentWave = 0;
    private int currentEnemiesAlive = 0;

    public void InitializeAltar()
    {
        altarStateManager.OnAltarInitialization();
    }

    public void ActivateAltar()
    {
        altarStateManager.OnAltarActivate();

        Invoke(nameof(SpawnNextWave), 1);
    }

    private void SpawnNextWave()
    {
        for(int i = enemiesCountPerWave[currentWave]; i >= 0; i--)
        {
            AltarEnemySpawnEffect currentSpawnEffect = Instantiate(spawnMinionEffect, transform.position, Quaternion.identity);

            currentSpawnEffect.Initialize(Random.insideUnitCircle * spawnRadius, possibleEnemies[Random.Range(0, possibleEnemies.Count)], this);
        }
    }

    public void OnSpawnedEnemyDies()
    {
        currentEnemiesAlive--;

        if (currentEnemiesAlive <= 0)
        {
            OnCompleteCurrentWave();
        }
    }

    private void OnCompleteCurrentWave()
    {
        currentWave++;

        if(currentWave == enemiesCountPerWave.Count - 1)
        {
            SpawnBoss();
        }
        else
        {
            SpawnNextWave();
        }
    }

    private void SpawnBoss()
    {
        AltarEnemySpawnEffect currentSpawnEffect = Instantiate(spawnMinionEffect, transform.position, Quaternion.identity);

        currentSpawnEffect.Initialize(Random.insideUnitCircle * spawnRadius, bossEnemy, this);
    }

    private void OnBossDies()
    {
        CompleteAltar();
    }

    public void CompleteAltar()
    {
        altarStateManager.OnAltarComplete();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, spawnRadius);
    }
}
