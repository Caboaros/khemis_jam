using Sirenix.OdinInspector;
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
    [ReadOnly, SerializeField]
    private AltarState currentState;

    private void Awake()
    {
        InitializeAltar();
    }

    public void InitializeAltar()
    {
        altarStateManager.OnAltarInitialization();

        currentState = AltarState.IDLE;
    }

    public void ActivateAltar()
    {
        altarStateManager.OnAltarActivate();

        Invoke(nameof(SpawnNextWave), 1);

        currentState = AltarState.INPROGRESS;
    }

    private void SpawnNextWave()
    {
        print(currentWave);

        for(int i = enemiesCountPerWave[currentWave] - 1; i >= 0; i--)
        {
            AltarEnemySpawnEffect currentSpawnEffect = Instantiate(spawnMinionEffect, transform.position, Quaternion.identity);

            Vector2 spawnPoint = (Vector2)transform.position + Random.insideUnitCircle * spawnRadius;

            currentSpawnEffect.Initialize(spawnPoint, possibleEnemies[Random.Range(0, possibleEnemies.Count)], this);

            currentEnemiesAlive++;
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

        Vector2 spawnPoint = (Vector2)transform.position + Random.insideUnitCircle * spawnRadius;

        currentSpawnEffect.Initialize(spawnPoint, bossEnemy, this);
    }

    public void OnBossDies()
    {
        CompleteAltar();
    }

    private void CompleteAltar()
    {
        altarStateManager.OnAltarComplete();

        currentState = AltarState.COMPLETE;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(currentState == AltarState.IDLE)
        {
            if(collision.gameObject.layer == 8) //Layer do PLAYER
            {
                ActivateAltar();
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, spawnRadius);
    }

    public enum AltarState
    {
        IDLE,
        INPROGRESS,
        COMPLETE
    }
}
