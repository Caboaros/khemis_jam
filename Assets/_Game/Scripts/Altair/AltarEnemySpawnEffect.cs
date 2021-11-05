using _Game.Scripts.Enemies;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class AltarEnemySpawnEffect : MonoBehaviour
{
    public GameObject movementTrailEffect;
    public GameObject spawnEnemyEffect;

    public void Initialize(Vector2 destination, GameObject enemyToSpawn, AltarEnemySpawner sourceSpawner)
    {
        transform.DOMove(destination, Random.Range(1.5f, 2f)).SetEase(Ease.OutCubic).OnComplete(delegate 
        {
            movementTrailEffect.SetActive(false);

            spawnEnemyEffect.SetActive(true);

            AltarEnemyLife spawnedEnemyLife = Instantiate(enemyToSpawn, transform.position, Quaternion.identity).GetComponent<AltarEnemyLife>();

            spawnedEnemyLife.sourceSpawner = sourceSpawner;
        }); 
    }
}
