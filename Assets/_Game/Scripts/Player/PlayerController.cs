using UnityEngine;

namespace _Game.Scripts.Player
{
    public class PlayerController : MonoBehaviour
    {
        public static PlayerController Instance { get; set; }

        public PlayerStatus Status;

        [HideInInspector] public PlayerCombat Combat;
        [HideInInspector] public PlayerLife Life;
        [HideInInspector] public PlayerAnimations Animations;
        [HideInInspector] public PlayerMovement Movement;
        [HideInInspector] public PlayerInventory Inventory;
        [HideInInspector] public PlayerSounds Sounds;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }

            Combat = GetComponent<PlayerCombat>();
            Life = GetComponent<PlayerLife>();
            Movement = GetComponent<PlayerMovement>();
            Inventory = GetComponent<PlayerInventory>();
            Animations = GetComponentInChildren<PlayerAnimations>();
            Sounds = GetComponent<PlayerSounds>();
        }

        private void Start()
        {
            Life.Init(OnPlayerDied);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.U))
            {
                Life.TakeDamage(2, Vector2.zero);
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

            Status = PlayerStatus.Dead;
        }
    }

    public enum PlayerStatus
    {
        Idle, Walking, Attack, Dead
    }
}