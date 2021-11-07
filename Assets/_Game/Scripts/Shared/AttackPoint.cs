using System.Collections.Generic;
using _Game.Scripts.Shared;
using _Game.Scripts.Utility;
using UnityEngine;

namespace _Game.Scripts.Player
{
    public class AttackPoint : MonoBehaviour
    {
        private List<IMortal> _targets = new List<IMortal>();

        public void ApplyDamage(int damage)
        {
            for (int i = 0; i < _targets.Count; i++)
            {
                print($"Inimigo: {_targets[i]} tomou dano de {damage}");
                _targets[i].TakeDamage(damage, transform.position);
                CameraShakeController.Shake();
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.transform.root.TryGetComponent(out IMortal target)) return;

            if (!_targets.Contains(target))
            {
                _targets.Add(target);
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (!other.transform.root.TryGetComponent(out IMortal enemy)) return;

            if (_targets.Contains(enemy))
            {
                _targets.Remove(enemy);
            }
        }
    }
}