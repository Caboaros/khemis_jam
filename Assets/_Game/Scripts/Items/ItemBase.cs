using _Game.Scripts.Player;
using DG.Tweening;
using UnityEngine;

namespace _Game.Scripts.Items
{
    public class ItemBase : MonoBehaviour, ICollectable
    {
        [SerializeField] protected Transform spriteTransform;
        [SerializeField] protected Collider2D collider;
        
        protected Rigidbody2D _rigidbody;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        public void SpawnAnimation()
        {
            collider.enabled = false;
            spriteTransform.DOLocalMoveY(.5f, .3f).SetEase(Ease.OutCubic).onComplete = () =>
            {
                spriteTransform.DOLocalMoveY(0, .6f).SetEase(Ease.OutBounce).onComplete = () => collider.enabled = true;
            };
        }

        public virtual void OnCollect(PlayerController player)
        {
        }
    }
}
