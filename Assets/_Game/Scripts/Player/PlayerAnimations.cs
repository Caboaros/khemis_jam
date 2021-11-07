using System;
using Spine.Unity;
using UnityEngine;

namespace _Game.Scripts.Player
{
    public class PlayerAnimations : MonoBehaviour
    {
        [SerializeField] private Animator animator;
        [Space] [SerializeField] private SkeletonMecanim mecanim;
        [Space]
        [SerializeField] private SkeletonDataAsset frontAsset;
        [SerializeField] private SkeletonDataAsset backAsset;
        [SerializeField] private SkeletonDataAsset sideAsset;
        
        private Action _onMeleeAttack;
        private Action _onThrow;
        
        private static readonly int Walking = Animator.StringToHash("IsWalking");
        private static readonly int Attack = Animator.StringToHash("Attack");
        private static readonly int Die = Animator.StringToHash("Die");
        private static readonly int Vertical = Animator.StringToHash("Vertical");
        private static readonly int Horizontal = Animator.StringToHash("Horizontal");
        private static readonly int Direction = Animator.StringToHash("Direction");
        private static readonly int Sequence = Animator.StringToHash("Sequence");

        public void PlayPunchAttackAnimation(Action onAttack, int sequence)
        {
            _onMeleeAttack = onAttack;
            
            animator.SetInteger(Sequence, sequence);
            animator.SetTrigger(Attack);
        }

        public void PlayThrowAttack(Action onThrow)
        {
            _onThrow = onThrow;
            
            animator.SetInteger(Sequence, 1);
            animator.SetTrigger(Attack);
        }

        public void ResetSequence()
        {
            animator.SetInteger(Sequence, 0);
        }

        public void PlayDyingAnimation()
        {
            animator.SetTrigger(Die);
        }

        public void MovementAnimation(float vertical, float horizontal)
        {
            animator.SetFloat(Vertical, vertical);
            animator.SetFloat(Horizontal, Mathf.Abs(horizontal));
        }

        public void PlayHitAnimation()
        {
            
        }

        public void SetDirection(MovementDirection direction)
        {
            mecanim.ClearState();

            switch (direction)
            {
                case MovementDirection.Top:
                    mecanim.skeletonDataAsset = backAsset;
                    mecanim.initialFlipX = false;
                    break;
                case MovementDirection.Down:
                    mecanim.skeletonDataAsset = frontAsset;
                    mecanim.initialFlipX = false;
                    break;
                case MovementDirection.Left:
                case MovementDirection.Right:
                    mecanim.skeletonDataAsset = sideAsset;
                    mecanim.initialFlipX = direction == MovementDirection.Left;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(direction), direction, null);
            }
            
            mecanim.Initialize(true);
            
            animator.SetInteger(Direction, (int)direction);
        }

        public void OnAttackEvent()
        {
            _onMeleeAttack?.Invoke();
        }

        public void OnEndAttackEvent()
        {
            PlayerController.Instance.Combat.canAttack = true;
            PlayerController.Instance.Movement.CanMove = true;
        }

        public void OnThrowAttackEvent()
        {
            _onThrow?.Invoke();
            _onThrow = null;
        }
    }
}
