using Sirenix.OdinInspector;
using UnityEngine;

namespace _Game.Scripts.Items
{
    [CreateAssetMenu(fileName = "New Weapon", menuName = "Weapon")]
    public class SO_Weapon : ScriptableObject
    {
        public string id;
        public string name;
        public Sprite sprite;
        public WeaponType type;
        [Range(0, 10)] public int damage;
        [Title("Stun")] [Range(0f, 1f)] public float stunPercentage;
        [Range(1, 10)] public float stunDuration;
    }

    public enum WeaponType
    {
        Melee,
        Throwable
    }
}