using System;
using _Game.Scripts.Enemies;
using _Game.Scripts.Items;
using _Game.Scripts.Utility;
using Blazewing.DataEvent;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _Game.Scripts.Player
{
    public class PlayerCombat : MonoBehaviour
    {
        [ReadOnly] public SO_Weapon CurrentWeapon;
        [Space]
        [SerializeField] private SpriteRenderer weaponSpriteRenderer;
        [Space]
        [SerializeField] private Transform attackPoint;
        [SerializeField] private float attackRange;
        [SerializeField] private LayerMask enemyLayers;

        private int _sequence;
        
        public int Damage
        {
            get => CurrentWeapon != null ? CurrentWeapon.damage : 1;
        }

        public bool canAttack;

        private float _attackDelay = 0;

        private void Start()
        {
            _sequence = 0;
            canAttack = true;
        }
        
        private void Update()
        {
            //if(canAttack && )
        }

        public void StartAttack()
        {
            if(!canAttack) return;

            PlayerController.Instance.Status = PlayerStatus.Attack;
            
            _sequence = Mathf.Clamp(_sequence + 1, 0, 2);
            
            PlayerController.Instance.Movement.StopMovement();
            PlayerController.Instance.Animations.PlayAttackAnimation(Attack, _sequence);
        }

        public void EquipWeapon(SO_Weapon weapon)
        {
            CurrentWeapon = weapon;
            weaponSpriteRenderer.sprite = weapon.sprite;
            
            DataEvent.Notify(new WeaponEquippedStruct(weapon));
        }

        public void DropWeapon()
        {
            if(CurrentWeapon == null) return;
            
            ItemSpawner.SpawnWeapon(CurrentWeapon.id, transform.position + GetSpawnDirection());
            
            CurrentWeapon = null;
            weaponSpriteRenderer.sprite = null;
            
            DataEvent.Notify(new WeaponEquippedStruct(CurrentWeapon));
        }

        private Vector3 GetSpawnDirection()
        {
            switch (PlayerController.Instance.Movement.MovementDirection)
            {
                case MovementDirection.Top:
                    return new Vector3(0, 1);
                case MovementDirection.Down:
                    return new Vector3(0, -1);
                case MovementDirection.Left:
                    return new Vector3(-1, 0);
                case MovementDirection.Right:
                    return new Vector3(1, 0);
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void Attack()
        {
            Collider2D[] hitEnemies = new Collider2D[5];
            var size = Physics2D.OverlapCircleNonAlloc(attackPoint.position, attackRange, hitEnemies, enemyLayers);
            foreach (Collider2D enemy in hitEnemies)
            {
                if(enemy == null) continue;

                if (enemy.transform.root.TryGetComponent(out EnemyLife enemyLife))
                {
                    print($"Inimigo: {enemy.transform.root.name} tomou dano de {Damage}");
                    enemyLife.TakeDamage(Damage, attackPoint.position);
                    CameraShakeController.Shake();
                        
                }
            }
        }

        private void OnDrawGizmosSelected()
        {
            if(attackPoint == null) return;
        
            Gizmos.DrawWireSphere(attackPoint.position, attackRange);
        }
    }

    [Serializable]
    public struct WeaponEquippedStruct
    {
        public SO_Weapon weapon;

        public WeaponEquippedStruct(SO_Weapon weapon)
        {
            this.weapon = weapon;
        }
    }
}
