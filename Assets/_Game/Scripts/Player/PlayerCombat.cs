using System;
using System.Collections.Generic;
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
        [Space] [SerializeField] private SpriteRenderer weaponSpriteRenderer;
        [Space] [SerializeField] private float attackRange;
        [SerializeField] private LayerMask enemyLayers;
        [Space] [SerializeField] private List<AttackPoint> topAttackPoints;
        [SerializeField] private List<AttackPoint> downAttackPoints;
        [SerializeField] private List<AttackPoint> leftAttackPoints;
        [SerializeField] private List<AttackPoint> rightAttackPoints;
        [Space] [SerializeField] private Transform topThrowablePoint;
        [SerializeField] private Transform downThrowablePoint;
        [SerializeField] private Transform leftThrowablePoint;
        [SerializeField] private Transform rightThrowablePoint;

        public int Damage
        {
            get => CurrentWeapon != null ? CurrentWeapon.damage : 1;
        }

        private List<AttackPoint> _attackPoints;
        private AttackPoint _currentAttackPoint;
        private Transform _currentThrowablePoint;
        private int _sequence;
        [HideInInspector] public bool canAttack;
        private bool _isAttacking;
        private Ray _ray;

        private void Start()
        {
            _sequence = 0;
            canAttack = true;
        }

        private void Update()
        {
            if (!canAttack) return;

            if (Input.GetButtonDown("Jump"))
            {
                if (CurrentWeapon == null)
                {
                    StartMeleeAttack();
                }
                else
                {
                    if (CurrentWeapon.type == WeaponType.Throwable)
                    {
                        StartThrowableAttack();
                    }
                    else
                    {
                        StartWhipAttack();
                    }
                }
            }
        }

        private void StartMeleeAttack()
        {
            _isAttacking = true;
            _sequence = Mathf.Clamp(_sequence + 1, 0, 3);

            _currentAttackPoint = _attackPoints[_sequence - 1];

            PlayerController.Instance.Status = PlayerStatus.Attack;
            PlayerController.Instance.Movement.StopMovement();
            PlayerController.Instance.Animations.PlayPunchAttackAnimation(MeleeAttack, _sequence);
        }

        private void StartWhipAttack()
        {
            _isAttacking = true;
            _sequence = 3;

            _currentAttackPoint = _attackPoints[_sequence - 1];

            PlayerController.Instance.Status = PlayerStatus.Attack;
            PlayerController.Instance.Movement.StopMovement();
            PlayerController.Instance.Animations.PlayPunchAttackAnimation(MeleeAttack, _sequence);
        }

        private void StartThrowableAttack()
        {
            _isAttacking = true;
            _sequence = 0;

            PlayerController.Instance.Status = PlayerStatus.Attack;
            PlayerController.Instance.Movement.StopMovement();
            PlayerController.Instance.Animations.PlayThrowAttack(ThrowItem);
        }

        public void ResetSequence()
        {
            if (!_isAttacking) return;

            _isAttacking = false;
            _sequence = 0;
            PlayerController.Instance.Animations.ResetSequence();
        }

        public bool EquipWeapon(SO_Weapon weapon)
        {
            if (CurrentWeapon != null)
            {
                return false;
            }

            CurrentWeapon = weapon;
            if (weapon.type == WeaponType.Throwable)
            {
                weaponSpriteRenderer.sprite = weapon.sprite;
            }
            else
            {
                PlayerController.Instance.Animations.HasWhip = true;
            }

            DataEvent.Notify(new WeaponEquippedStruct(weapon));

            return true;
        }

        public void DropWeapon()
        {
            if (CurrentWeapon == null) return;

            ItemSpawner.SpawnWeapon(CurrentWeapon.id, transform.position + GetSpawnDirection());

            if (CurrentWeapon.type == WeaponType.Melee)
            {
                PlayerController.Instance.Animations.HasWhip = false;
            }

            CurrentWeapon = null;
            weaponSpriteRenderer.sprite = null;

            DataEvent.Notify(new WeaponEquippedStruct(CurrentWeapon));
        }

        public void SetDirection(MovementDirection direction)
        {
            switch (direction)
            {
                case MovementDirection.Top:
                    _attackPoints = topAttackPoints;
                    _currentThrowablePoint = topThrowablePoint;
                    break;
                case MovementDirection.Down:
                    _attackPoints = downAttackPoints;
                    _currentThrowablePoint = downThrowablePoint;
                    break;
                case MovementDirection.Left:
                    _attackPoints = leftAttackPoints;
                    _currentThrowablePoint = leftThrowablePoint;
                    break;
                case MovementDirection.Right:
                    _attackPoints = rightAttackPoints;
                    _currentThrowablePoint = rightThrowablePoint;
                    break;
            }
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

        private void MeleeAttack()
        {
            _currentAttackPoint.ApplyDamage(Damage);
            PlayerController.Instance.Sounds.PlayAirHitSound();

            if (_sequence == 3)
            {
                PlayerController.Instance.Sounds.PlayAttackSounds();
            }


            /*
            RaycastHit2D[] hit = new RaycastHit2D[5];

            Physics2D.LinecastNonAlloc(transform.position, _currentAttackPoint.position, hit, enemyLayers, 0, 10);

            foreach (RaycastHit2D enemy in hit)
            {
                if (enemy.collider == null) continue;

                if (enemy.transform.root.TryGetComponent(out EnemyLife enemyLife))
                {
                    print($"Inimigo: {enemy.transform.root.name} tomou dano de {Damage}");
                    enemyLife.TakeDamage(Damage, _currentAttackPoint.position);
                    CameraShakeController.Shake();
                }
            }

            return;
            */

            /*
            Collider2D[] hitEnemies = new Collider2D[5];
            hitEnemies = Physics2D.OverlapCapsuleAll(_currentAttackPoint.position, Vector2.one * 2, CapsuleDirection2D.Vertical, 5);
            //var size = Physics2D.OverlapCircleNonAlloc(_currentAttackPoint.position, attackRange, hitEnemies, enemyLayers);
            foreach (Collider2D enemy in hitEnemies)
            {
                if (enemy == null) continue;

                if (enemy.transform.root.TryGetComponent(out EnemyLife enemyLife))
                {
                    print($"Inimigo: {enemy.transform.root.name} tomou dano de {Damage}");
                    enemyLife.TakeDamage(Damage, _currentAttackPoint.position);
                    
                }
            }
            */
        }

        private void ThrowItem()
        {
            if (CurrentWeapon == null) return;

            if (!_isAttacking) return;

            _isAttacking = false;

            ItemSpawner.SpawnThrowable(CurrentWeapon.id, _currentThrowablePoint.position,
                (Vector2)_currentThrowablePoint.position - (Vector2)transform.position, 5, enemyLayers);

            CurrentWeapon = null;
            weaponSpriteRenderer.sprite = null;
            DataEvent.Notify(new WeaponEquippedStruct(CurrentWeapon));
        }

        private void OnDrawGizmosSelected()
        {
            if (_currentAttackPoint == null) return;

            Gizmos.DrawWireSphere(_currentAttackPoint.transform.position, attackRange);

            Gizmos.DrawLine(transform.position, _currentAttackPoint.transform.position);
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