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
        
        private Action _onAttack;
        
        private static readonly int Walking = Animator.StringToHash("IsWalking");
        private static readonly int Attack = Animator.StringToHash("Attack");
        private static readonly int Die = Animator.StringToHash("Die");
        private static readonly int Vertical = Animator.StringToHash("Vertical");
        private static readonly int Horizontal = Animator.StringToHash("Horizontal");
        private static readonly int Direction = Animator.StringToHash("Direction");

        public bool IsWalking
        {
            set => animator.SetBool(Walking, value);
        }

        public void PlayAttackAnimation(Action onAttack)
        {
            animator.SetTrigger(Attack);
            _onAttack = onAttack;
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

        public void SetDirection(MovementDirection direction)
        {
            mecanim.ClearState();

            switch (direction)
            {
                case MovementDirection.Top:
                    mecanim.skeletonDataAsset = backAsset;
                    break;
                case MovementDirection.Down:
                    mecanim.skeletonDataAsset = frontAsset;
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
            _onAttack?.Invoke();
        }
    }
}
