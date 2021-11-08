using UnityEngine;

namespace _Game.Scripts.Enemies
{
    public class EnemyController : MonoBehaviour
    {
        public EnemyAnimations Animations;
        public EnemyMovement Movement;
        public EnemyCombat Combat;
        public EnemyLife Life;
        public EnemyAI Ai;
        public EnemyDrop Drop;

        private void Awake()
        {
            Animations = GetComponentInChildren<EnemyAnimations>();
            Movement = GetComponent<EnemyMovement>();
            Combat = GetComponent<EnemyCombat>();
            Life = GetComponent<EnemyLife>();
            Ai = GetComponent<EnemyAI>();
            Drop = GetComponent<EnemyDrop>();
        }

        private void Start()
        {
            Movement.Init(this);
            Combat.Init(this);
            Life.Init(this);
            Animations.Init(this);
            Ai.Init(this);
        }
    }
}
