using _Game.Scripts.Player;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

namespace _Game.Scripts.Items
{
    public class WeaponBehaviour : ItemBase
    {
        [PropertyOrder(0)] public SO_Weapon Data;
        [Space] [SerializeField] protected SpriteRenderer spriteRenderer;
        [SerializeField] protected TextMeshPro weaponName;

        private void Start()
        {
            DisplayData();
        }

        public void Init(SO_Weapon data)
        {
            Data = data;
            DisplayData();
        }

        private void DisplayData()
        {
            spriteRenderer.sprite = Data.sprite;
            weaponName.text = Data.name;
        }

        public override void OnCollect(PlayerController player)
        {
            player.Combat.EquipWeapon(Data);
            Destroy(gameObject);
        }
    }
}