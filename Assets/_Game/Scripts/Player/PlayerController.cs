using UnityEngine;

namespace _Game.Scripts.Player
{
    public class PlayerController : MonoBehaviour
    {
        public PlayerCombat Combat;
        public PlayerLife Life;
        public PlayerAnimations Animations;
        public PlayerMovement Movement;
        public PlayerInventory Inventory;

        private void Awake()
        {
            Combat = GetComponent<PlayerCombat>();
            Life = GetComponent<PlayerLife>();
            Movement = GetComponent<PlayerMovement>();
            Inventory = GetComponent<PlayerInventory>();
        }

        private void Start()
        {
            Life.Init(OnPlayerDied);
        }

        private void Update()
        {
            if (Input.GetButtonDown("Jump"))
            {
                Combat.StartAttack();
            }

            if (Input.GetKeyDown(KeyCode.U))
            {
                Life.TakeDamage(2);
            }

            if (Input.GetKeyDown(KeyCode.I))
            {
                Life.Heal(2);
            }

            if (Input.GetKeyDown(KeyCode.R))
            {
                Combat.DropWeapon();
            }
        }

        private void OnPlayerDied()
        {
            print("Game Over!");
            Animations.PlayDyingAnimation();
            Movement.CanMove = false;
        }
    }
}