using System;
using _Game.Scripts.HUD;
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
                DataEvent.Notify(new CrystalsCollected(_crystalCollected));
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

                if (item.transform.root.TryGetComponent(out ICollectable collectable))
                {
                    collectable.OnCollect(PlayerController.Instance);
                }
            }
        }
    }
}