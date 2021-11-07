using UnityEngine;

namespace _Game.Scripts.Enemies
{
    public class AltarEnemyLife : EnemyLife
    {
        [HideInInspector]
        public AltarEnemySpawner sourceSpawner;

        public bool isBoss;

        public override void Die()
        {
            if (!isBoss)
                sourceSpawner.OnSpawnedEnemyDies();
            else
                sourceSpawner.OnBossDies();

            base.Die();
        }
    }
}
