using UnityEngine;

namespace _Game.Scripts.Shared
{
    public interface IMortal
    {
        int HeartsAmount { get; set; }
        
        void TakeDamage(int damage, Vector3 damagePoint);

        void Heal(int amount);

        void ApplyStun(float duration);

        void Die();
    }
}
