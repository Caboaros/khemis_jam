using _Game.Scripts.Utility;
using UnityEngine;

public class EnemyDrop : MonoBehaviour
{
    [SerializeField][Range(0, 1)] private float crystalDropPercentage;

    public void DropCrystal()
    {
        float random = Random.value;

        if (random <= crystalDropPercentage)
        {
            ItemSpawner.SpawnCrystal(transform.position);
        }
    }
}
