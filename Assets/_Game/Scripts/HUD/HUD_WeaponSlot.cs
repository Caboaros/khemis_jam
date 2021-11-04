using _Game.Scripts.Items;
using _Game.Scripts.Player;
using Blazewing.DataEvent;
using UnityEngine;
using UnityEngine.UI;

namespace _Game.Scripts.HUD
{
    public class HUD_WeaponSlot : MonoBehaviour
    {
        [SerializeField] private Image weaponEquippedImage;

        private SO_Weapon _currentWeapon;
        
        private void OnEnable()
        {
            DataEvent.Register<WeaponEquippedStruct>(OnWeaponEquipped);
        }

        private void OnDisable()
        {
            DataEvent.Unregister<WeaponEquippedStruct>(OnWeaponEquipped);
        }

        private void OnWeaponEquipped(WeaponEquippedStruct eventData)
        {
            _currentWeapon = eventData.weapon;
            weaponEquippedImage.enabled = eventData.weapon != null;
            weaponEquippedImage.sprite = eventData.weapon != null ? eventData.weapon.sprite : null;
        }
    }
}
