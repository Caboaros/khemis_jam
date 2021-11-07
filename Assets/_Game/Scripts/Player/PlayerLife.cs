using System;
using _Game.Scripts.Shared;
using Blazewing.DataEvent;
using DG.Tweening;
using UnityEngine;

namespace _Game.Scripts.Player
{
    public class PlayerLife : MonoBehaviour, IMortal
    {
        public int maxHearts = 5;
        [Space] [SerializeField] private Transform damagePointLeft;
        [SerializeField] private Transform damagePointRight;
        
        private bool _isDead;
        private int _currentHeartAmount;
        private Action _onPlayerDied;
        private Rigidbody2D _rigidbody;

        public int HeartsAmount
        {
            get => _currentHeartAmount;
            set
            {
                if(_isDead) return;
                
                _currentHeartAmount = Mathf.Clamp(value, 0, maxHearts);
                
                DataEvent.Notify(new PlayerHeartsStruct(_currentHeartAmount));

                if (_currentHeartAmount <= 0)
                {
                    Die();
                }
            }
        }

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        public void Init(Action onPlayerDied)
        {
            _onPlayerDied = onPlayerDied;

            HeartsAmount = maxHearts;
        }

        public void TakeDamage(int damage, Vector3 damagePoint)
        {
            HeartsAmount -= damage;
            
            _rigidbody.DOMove(transform.position + ((transform.position - damagePoint).normalized) * .5f,
                .25f);
            
            PlayerController.Instance.Animations.PlayHitAnimation();
        }

        public void Heal(int amount)
        {
            HeartsAmount += amount;
        }

        public void ApplyStun(float duration)
        {
            
        }

        public void Die()
        {
            _isDead = true;
            _onPlayerDied?.Invoke();
        }

        public Transform GetDamagePoint(Vector2 enemyPosition)
        {
            if (enemyPosition.x < transform.position.x)
            {
                return damagePointLeft;
            }

            if (enemyPosition.x > transform.position.x)
            {
                return damagePointRight;
            }

            return damagePointLeft;
        }
    }

    [Serializable]
    public struct PlayerHeartsStruct
    {
        public int amount;

        public PlayerHeartsStruct(int amount)
        {
            this.amount = amount;
        }
    }
}