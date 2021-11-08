using System;
using _Game.Scripts.Player;
using DG.Tweening;
using UnityEngine;

namespace _Game.Scripts.Items
{
    public class ItemBase : MonoBehaviour, ICollectable
    {
        [SerializeField] protected Transform spriteTransform;
        [SerializeField] protected Collider2D collider;
        
        private bool _collected;
        protected Rigidbody2D _rigidbody;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        private void Start()
        {
            IdleAnimation();
        }

        public virtual void IdleAnimation()
        {
            spriteTransform.DOLocalMoveY(.1f, .5f).onComplete = () =>
            {
                spriteTransform.DOLocalMoveY(0f, .5f).onComplete = IdleAnimation;
            };
        }

        public virtual void SpawnAnimation()
        {
            collider.enabled = false;
            spriteTransform.DOLocalMoveY(.5f, .3f).SetEase(Ease.OutCubic).onComplete = () =>
            {
                spriteTransform.DOLocalMoveY(0, .6f).SetEase(Ease.OutBounce).onComplete = () => collider.enabled = true;
            };
        }

        protected void MoveToPlayer(Transform playerTransform, Action onMoveToPlayer)
        {
            if(_collected) return;
            
            _collected = true;
            
            transform.DOMove(playerTransform.position, .2f).onComplete = () =>
            {
                onMoveToPlayer?.Invoke();
                Destroy(gameObject);
            };
        }

        public virtual void OnCollect(PlayerController player)
        {
        }
    }
}
