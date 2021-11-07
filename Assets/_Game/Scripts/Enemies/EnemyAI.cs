using System.Collections;
using System.Collections.Generic;
using System.Linq;
using _Game.Scripts.Map;
using _Game.Scripts.Player;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _Game.Scripts.Enemies
{
    public class EnemyAI : MonoBehaviour
    {
        public EnemyState State;

        private PlayerLife _player;
        private EnemyController _controller;
        [SerializeField] private PatrolPoint _patrolPoint;

        private List<PatrolPoint> _patrolPoints;

        public void Init(EnemyController controller)
        {
            _controller = controller;

            _patrolPoints = FindObjectsOfType<PatrolPoint>().ToList();

            StartPatrol();
        }

        private void Update()
        {
            if (State == EnemyState.Chase)
            {
                _controller.Movement.ChasePlayer(_player.GetDamagePoint(transform.position));

                if (_controller.Movement.RemainingDistance <= _controller.Movement.StoppingDistance)
                {
                    Attack();
                }
            }
        }

        public void StartPatrol()
        {
            State = EnemyState.Patrol;
            StartCoroutine(PatrolBehaviourCoroutine());
        }

        private IEnumerator PatrolBehaviourCoroutine()
        {
            while (State == EnemyState.Patrol)
            {
                yield return new WaitForSeconds(3);

                PatrolPoint newPatrolPoint = GetRandomPoint();

                if (newPatrolPoint == null) continue;

                if (_patrolPoint != null)
                {
                    _patrolPoint.IsAvailable = true;
                }

                _patrolPoint = newPatrolPoint;
                _patrolPoint.IsAvailable = false;
                yield return StartCoroutine(_controller.Movement.MoveToPointCoroutine(_patrolPoint.transform.position));
            }
        }

        private PatrolPoint GetRandomPoint()
        {
            List<PatrolPoint> availablePatrolPoints = new List<PatrolPoint>();
            for (int i = 0; i < _patrolPoints.Count; i++)
            {
                if (!_patrolPoints[i].IsAvailable) continue;

                availablePatrolPoints.Add(_patrolPoints[i]);
            }

            if (availablePatrolPoints.Count == 0) return null;

            return availablePatrolPoints[Random.Range(0, availablePatrolPoints.Count)];
        }

        public void ChasePlayer(PlayerLife player)
        {
            _controller.Movement.StopMovement();
            _player = player;
            State = EnemyState.Chase;
        }

        private void Attack()
        {
            State = EnemyState.Attack;
            _controller.Combat.StartAttack(() => State = EnemyState.Chase);
        }
    }

    public enum EnemyState
    {
        Patrol,
        Chase,
        Attack
    }
}