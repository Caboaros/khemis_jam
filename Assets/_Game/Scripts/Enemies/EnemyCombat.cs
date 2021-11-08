using System;
using System.Collections.Generic;
using _Game.Scripts.Player;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _Game.Scripts.Enemies
{
    public class EnemyCombat : MonoBehaviour
    {
        [SerializeField] private List<AttackPoint> leftAttackPoints;
        [SerializeField] private List<AttackPoint> rightAttackPoints;
        [Space] [SerializeField] private EnemyAnimations animations;

        private EnemyController _controller;
        private List<AttackPoint> _attackPoints;
        private AttackPoint _currentAttackPoint;
        private int _currentAttackIndex;

        public void Init(EnemyController controller)
        {
            _controller = controller;
        }

        public void SetDirection(MovementDirection direction)
        {
            switch (direction)
            {
                case MovementDirection.Left:
                    _attackPoints = leftAttackPoints;
                    break;
                case MovementDirection.Right:
                    _attackPoints = rightAttackPoints;
                    break;
            }
        }

        public void StartAttack(Action onEndAttack)
        {
            _currentAttackIndex = Random.Range(0, 2);
            animations.PlayPunchAttackAnimation(OnAttack, _currentAttackIndex, onEndAttack);
        }

        private void OnAttack()
        {
            _currentAttackPoint = _attackPoints[_currentAttackIndex];
            _currentAttackPoint.ApplyDamage(_currentAttackIndex + 1);
        }
    }
}