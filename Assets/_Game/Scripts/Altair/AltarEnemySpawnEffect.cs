using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AltarEnemySpawnEffect : MonoBehaviour
{
    public void Initialize(Vector2 destination, GameObject enemyToSpawn, AltarEnemySpawner sourceSpawner)
    {
        transform.position = destination;

        Instantiate(enemyToSpawn, transform.position, Quaternion.identity);

        //Classe Enemy precisa de um bind para sourceSpawner e avisar o spawner que ele morreu
    }
}
