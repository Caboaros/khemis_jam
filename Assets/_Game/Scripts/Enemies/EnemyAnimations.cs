using System;
using _Game.Scripts.Player;
using Spine.Unity;
using UnityEngine;
using Utility;

namespace _Game.Scripts.Enemies
{
    public class EnemyAnimations : MonoBehaviour
    {
        [SerializeField] private Animator animator;
        
        [Space] [SerializeField] private SkeletonMecanim mecanim;

        private EnemyController _controller;
        private Action _onMeleeAttack;
        private Action _onEndAttack;
        
        private static readonly int MovSpeed = Animator.StringToHash("MovSpeed");
        private static readonly int Hit = Animator.StringToHash("Hit");
        private static readonly int Attack = Animator.StringToHash("Attack");
        private static readonly int AttackIndex = Animator.StringToHash("AttackIndex");

        public void Init(EnemyController controller)
        {
            _controller = controller;
        }

        public void MovementAnimation(float movementSpeed, bool maxSpeed)
        {
            animator.SetFloat(MovSpeed, movementSpeed.Map(0, movementSpeed, 0, maxSpeed ? 1f : 0.5f));
        }
        
        public void SetDirection(MovementDirection direction)
        {
            mecanim.ClearState();

            switch (direction)
            {
                case MovementDirection.Top:
                    //mecanim.initialFlipX = false;
                    break;
                case MovementDirection.Down:
                    //mecanim.initialFlipX = false;
                    break;
                case MovementDirection.Left:
                case MovementDirection.Right:
                    mecanim.initialFlipX = direction == MovementDirection.Left;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(direction), direction, null);
            }
            
            mecanim.Initialize(true);
        }
        
        public void PlayHitAnimation()
        {
            animator.SetTrigger(Hit);
        }
        
        public void PlayPunchAttackAnimation(Action onAttack, int sequence, Action onEndAttack)
        {
            _onMeleeAttack = onAttack;
            _onEndAttack = onEndAttack;
            
            animator.SetInteger(AttackIndex, sequence);
            animator.SetTrigger(Attack);
        }
        
        
        public void OnAttackEvent()
        {
            _onMeleeAttack?.Invoke();
            _onMeleeAttack = null;
        }

        public void OnEndAttackEvent()
        {
            _onEndAttack?.Invoke();
            _onEndAttack = null;
        }
    }
}
