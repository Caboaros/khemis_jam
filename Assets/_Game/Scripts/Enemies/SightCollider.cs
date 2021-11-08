using _Game.Scripts.Player;
using UnityEngine;

namespace _Game.Scripts.Enemies
{
    public class SightCollider : MonoBehaviour
    {
        [SerializeField] private EnemyAI enemyAi;

        private PlayerLife _currentTarget;
    
        private void OnTriggerEnter2D(Collider2D other)
        {
            if(!other.transform.root.TryGetComponent(out PlayerLife player) || _currentTarget == player) return;

            _currentTarget = player;
            
            enemyAi.ChasePlayer(_currentTarget);
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if(!other.transform.root.TryGetComponent(out PlayerController player)) return;
        }
    }
}
