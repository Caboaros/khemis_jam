using System;
using UnityEngine;

namespace _Game.Scripts.Player
{
    public class PlayerAnimations : MonoBehaviour
    {
        [SerializeField] private Animator animator;
        
        private Action _onAttack;
        
        private static readonly int Walking = Animator.StringToHash("IsWalking");
        private static readonly int Attack = Animator.StringToHash("Attack");
        private static readonly int Die = Animator.StringToHash("Die");

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

        public void OnAttackEvent()
        {
            _onAttack?.Invoke();
        }
    }
}
