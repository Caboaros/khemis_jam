using System;
using Blazewing.DataEvent;
using UnityEngine;

namespace _Game.Scripts.Player
{
    public class PlayerLife : MonoBehaviour
    {
        [SerializeField] private int maxHearts = 5;
        
        private bool _isDead;
        private int _currentHeartAmount;
        private Action _onPlayerDied;

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
        
        public void Init(Action onPlayerDied)
        {
            _onPlayerDied = onPlayerDied;

            HeartsAmount = maxHearts;
        }

        public void TakeDamage(int damage)
        {
            HeartsAmount -= damage;
        }

        public void Heal(int amount)
        {
            HeartsAmount += amount;
        }

        private void Die()
        {
            _isDead = true;
            _onPlayerDied?.Invoke();
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