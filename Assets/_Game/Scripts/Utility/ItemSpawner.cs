using System.Collections.Generic;
using _Game.Scripts.Items;
using Unity.Mathematics;
using UnityEngine;

namespace _Game.Scripts.Utility
{
    public class ItemSpawner : MonoBehaviour
    {
        private static ItemSpawner _instance;

        [SerializeField] private CrystalBehaviour crystalPrefab;
        [SerializeField] private HeartBehaviour heartPrefab;
        [Space] [SerializeField] private List<WeaponBehaviour> weaponsList;
        [Space] [SerializeField] private List<ThrowableItem> throwableItemsList;

        private void Awake()
        {
            if (_instance != null)
            {
                Destroy(gameObject);
                return;
            }

            _instance = this;
            DontDestroyOnLoad(gameObject);
        }

        public static void SpawnCrystal(Vector2 position)
        {
            SpawnItem(_instance.crystalPrefab, position);
        }
        
        public static void SpawnHeart(Vector2 position)
        {
            SpawnItem(_instance.heartPrefab, position);
        }

        public static void SpawnWeapon(string weaponId, Vector2 position)
        {
            SpawnItem(GetWeaponPrefabById(weaponId), position);
        }
        
        public static void SpawnThrowable(string throwableId, Vector2 position, Vector2 direction, float speed, LayerMask layer)
        {
            SpawnItem(GetThrowablePrefabById(throwableId), position).Throw(direction, speed, layer);
        }

        public static T SpawnItem<T>(T item, Vector2 position) where T : ItemBase
        {
            T newItem = Instantiate(item, position, quaternion.identity);
            newItem.SpawnAnimation();

            return newItem;
        }

        private static WeaponBehaviour GetWeaponPrefabById(string id)
        {
            return _instance.weaponsList.Find(weapon => weapon.Data.id == id);
        }
        
        private static ThrowableItem GetThrowablePrefabById(string id)
        {
            return _instance.throwableItemsList.Find(weapon => weapon.Data.id == id);
        }
    }
}
