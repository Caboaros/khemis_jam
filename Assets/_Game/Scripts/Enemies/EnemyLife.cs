using _Game.Scripts.Shared;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace _Game.Scripts.Enemies
{
    public class EnemyLife : MonoBehaviour, IMortal
    {
        [SerializeField] private bool isImmortal;
        [Space]
        [SerializeField] private int maxLife = 3;
        [SerializeField] private TextMeshProUGUI heartsAmountText;
        [Space] [SerializeField] private GameObject poofEffectPrefab;
        [Space] [SerializeField] private EnemyAnimations animations;

        private EnemyController _controller;
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

        public void Init(EnemyController controller)
        {
            _controller = controller;
            
            _rigidbody = GetComponent<Rigidbody2D>();
            
            HeartsAmount = maxLife;
        }

        private void OnMouseDown()
        {
            TakeDamage(1, transform.position);
        }

        public void ApplyStun(float duration)
        {
            
        }

        public virtual void Die()
        {
            _isDead = true;
            _controller.Drop.DropCrystal();

            Instantiate(poofEffectPrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }

        public void TakeDamage(int damage, Vector3 damagePoint)
        {
            if (!isImmortal)
            {
                HeartsAmount -= damage;
            }

            _rigidbody.DOMove(transform.position + ((transform.position - damagePoint).normalized) * .5f,
                .25f);
            
            animations.PlayHitAnimation();
        }

        public void Heal(int amount)
        {
            throw new System.NotImplementedException();
        }
    }
}