using System;
using TMPro;
using UnityEngine;

namespace _Game.Scripts.Enemies
{
    public class EnemyLife : MonoBehaviour
    {
        [SerializeField] private bool isImmortal;
        [Space]
        [SerializeField] private int maxLife = 3;
        [SerializeField] private TextMeshProUGUI heartsAmountText;


        private EnemyDrop _drop;
        private Rigidbody2D _rigidbody;
        private int _currentHeartsAmount;
        private bool _isDead;

        public int HeartsAmount
        {
            get => _currentHeartsAmount;
            set
            {
                if(_isDead) return;
                
                _currentHeartsAmount = Mathf.Clamp(value, 0, maxLife);
                heartsAmountText.text = _currentHeartsAmount.ToString();

                if (_currentHeartsAmount <= 0)
                {
                    _isDead = true;
                    Die();
                }
            }
        }

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _drop = GetComponent<EnemyDrop>();
        }

        private void Start()
        {
            HeartsAmount = maxLife;
        }

        public void TakeDamage(int damage, Vector3 damageSourcePosition)
        {
            if (!isImmortal)
            {
                HeartsAmount -= damage;
            }

            _rigidbody.AddForce((transform.position - damageSourcePosition).normalized * 2, ForceMode2D.Impulse);
        }

        private void Die()
        {
            _isDead = true;
            _drop.DropCrystal();
            Destroy(gameObject);
        }
    }
}