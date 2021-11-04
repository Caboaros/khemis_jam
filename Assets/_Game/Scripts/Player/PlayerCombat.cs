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
    
        private PlayerAnimations _animations;
        private PlayerMovement _movement;
        
        public int Damage
        {
            get => CurrentWeapon != null ? CurrentWeapon.damage : 1;
        }

        private void Awake()
        {
            _animations = GetComponentInChildren<PlayerAnimations>();
            _movement = GetComponent<PlayerMovement>();
        }

        public void StartAttack()
        {
            _movement.StopMovement();
            _animations.PlayAttackAnimation(Attack);
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
            switch (_movement.MovementDirection)
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
                }
            }
            
            _movement.CanMove = true;
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
