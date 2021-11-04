using _Game.Scripts.Items;
using UnityEngine;

public class TestItem : MonoBehaviour
{
    [SerializeField] private SO_Weapon weaponData;

    private void Start()
    {
        if (TryGetComponent(out WeaponBehaviour weapon))
        {
            weapon.Init(weaponData);
        }
    }
}
