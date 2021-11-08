using _Game.Scripts.HUD;
using Blazewing.DataController;
using Blazewing.DataEvent;
using UnityEngine;

namespace _Game.Scripts.Player
{
    public class PlayerInventory : MonoBehaviour
    {
        [SerializeField] private float collectItemsRange;
        [SerializeField] private LayerMask itemsLayer;

        private Collider2D[] _hitItems;
        private int _crystalCollected;

        public int CrystalsCollected
        {
            get => _crystalCollected;
            set
            {
                _crystalCollected = Mathf.Clamp(value, 0, 100);
                CrystalsCollected crystalsCollected = new CrystalsCollected(_crystalCollected);

                DataEvent.Notify(crystalsCollected);
                DataController.Add(crystalsCollected);
            }
        }

        private void Start()
        {
            CrystalsCollected = 0;
            _hitItems = new Collider2D[5];
        }

        private void Update()
        {
            var size = Physics2D.OverlapCircleNonAlloc(transform.position, collectItemsRange, _hitItems, itemsLayer);
            foreach (Collider2D item in _hitItems)
            {
                if (item == null) continue;

                var collectable = item.GetComponentInParent<ICollectable>();

                if (collectable != null)
                {
                    collectable.OnCollect(PlayerController.Instance);
                }
            }
        }
    }
}