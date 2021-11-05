using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _Game.Scripts.Enemies
{
    public class AltarEnemyLife : EnemyLife
    {
        [HideInInspector]
        public AltarEnemySpawner sourceSpawner;

        public bool isBoss;

        protected override void Die()
        {
            if (!isBoss)
                sourceSpawner.OnSpawnedEnemyDies();
            else
                sourceSpawner.OnBossDies();

            base.Die();
        }
    }
}
