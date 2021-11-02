using UnityEngine;

namespace _Game.Scripts.Player
{
    public class PlayerAnimations : MonoBehaviour
    {
        [SerializeField] private Animator animator;
        
        private static readonly int Walking = Animator.StringToHash("IsWalking");

        public bool IsWalking
        {
            set => animator.SetBool(Walking, value);
        }
    }
}
