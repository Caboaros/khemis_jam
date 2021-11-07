using System;
using System.Collections;
using _Game.Scripts.Player;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.AI;

namespace _Game.Scripts.Enemies
{
    public class EnemyMovement : MonoBehaviour
    {
        [SerializeField, ReadOnly] private MovementDirection _movementDirection;
        [Space] [SerializeField] private float movementSpeed;

        private NavMeshAgent _agent;
        private EnemyController _controller;

        public MovementDirection MovementDirection
        {
            get => _movementDirection;
            set
            {
                if (_movementDirection == value) return;

                _movementDirection = value;

                _controller.Animations.SetDirection(_movementDirection);
                _controller.Combat.SetDirection(_movementDirection);
            }
        }

        public float RemainingDistance => _agent.remainingDistance;

        public float StoppingDistance => _agent.stoppingDistance;

        public void Init(EnemyController controller)
        {
            _controller = controller;
            
            _agent = GetComponent<NavMeshAgent>();
            _agent.updateRotation = false;
            _agent.updateUpAxis = false;
            _agent.stoppingDistance = 1;
        }

        public void MoveToPoint(Vector3 point, Action onPathEnded)
        {
            StartCoroutine(MoveToPointCoroutine(point, onPathEnded));
        }

        public IEnumerator MoveToPointCoroutine(Vector3 point, Action onPathEnded = null)
        {
            NavMeshPath path = new NavMeshPath();
            NavMesh.CalculatePath(transform.position, point, 1, path);
            
            if (path.corners.Length <= 0) yield break;

            for (int i = 0; i < path.corners.Length; i++)
            {
                Vector2 direction = (path.corners[i] - transform.position).normalized;
                MovementDirection = GetDirection(direction);
                _controller.Animations.MovementAnimation(movementSpeed, false);
                yield return transform.DOMove(path.corners[i], movementSpeed).SetSpeedBased(true).SetEase(Ease.Linear).WaitForCompletion();
            }
            
            _controller.Animations.MovementAnimation(0, false);
            
            onPathEnded?.Invoke();
        }

        private void Update()
        {
            /*
            float velocity = _agent.velocity.normalized.magnitude;

            _controller.Animations.MovementAnimation(velocity);
            MovementDirection = GetDirection(_agent.velocity);
            */
        }

        private MovementDirection GetDirection(Vector2 velocity)
        {
            if (Mathf.Abs(velocity.x) > Mathf.Abs(velocity.y))
            {
                if (velocity.x > 0)
                    return MovementDirection.Right;
                else
                    return MovementDirection.Left;
            }
            else
            {
                if (velocity.y > 0)
                    return MovementDirection.Top;
                else
                    return MovementDirection.Down;
            }
        }

        public void ChasePlayer(Transform target)
        {
            _agent.destination = target.position;
        }
    }
}