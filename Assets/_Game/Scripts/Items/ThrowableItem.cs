using _Game.Scripts.Shared;
using _Game.Scripts.Utility;
using UnityEngine;

namespace _Game.Scripts.Items
{
    public class ThrowableItem : WeaponBehaviour
    {
        [SerializeField] private float attackRange = .5f;
        
        private bool _isActive;
        private Vector3 _direction;
        private float _speed;
        private LayerMask _layer;
        
        public virtual void Throw(Vector3 direction, float speed, LayerMask layer)
        {
            weaponName.enabled = false;

            _layer = layer;
            _direction = direction;
            _speed = speed;
            _isActive = true;
            
            Invoke(nameof(Destroy), 5);
        }

        private void Update()
        {
            if (!_isActive) return;

            transform.position += _direction.normalized * _speed * Time.deltaTime;

            Collider2D[] hitTargets = new Collider2D[1];
            var size = Physics2D.OverlapCircleNonAlloc(transform.position, attackRange, hitTargets, _layer);
            foreach (Collider2D target in hitTargets)
            {
                if (target == null) continue;

                if (target.transform.root.TryGetComponent(out IMortal targetLife))
                {
                    CameraShakeController.Shake();
                    print($"Inimigo: {target.transform.root.name} tomou dano de {Data.damage}");
                    targetLife.TakeDamage(Data.damage, transform.position);

                    float random = Random.value;
                    if (random <= Data.stunPercentage)
                    {
                        targetLife.ApplyStun(Data.stunDuration);
                    }
                    
                    DestroyObject();
                }
            }
        }

        public override void SpawnAnimation()
        {
            collider.enabled = false;
        }

        private void DestroyObject()
        {
            Destroy(gameObject);
        }
    }
}
