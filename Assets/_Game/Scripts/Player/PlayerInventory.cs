using _Game.Scripts.HUD;
using Blazewing.DataEvent;
using UnityEngine;

namespace _Game.Scripts.Player
{
    public class PlayerInventory : MonoBehaviour
    {
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
        }
    }
}
