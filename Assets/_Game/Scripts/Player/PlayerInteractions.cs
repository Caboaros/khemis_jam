using System;
using UnityEngine;

namespace _Game.Scripts.Player
{
    public class PlayerInteractions : MonoBehaviour
    {
        private PlayerController _player;

        private void Awake()
        {
            _player = GetComponent<PlayerController>();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.transform.root.TryGetComponent(out ICollectable collectable))
            {
                collectable.OnCollect(_player);
            }
        }
    }
}