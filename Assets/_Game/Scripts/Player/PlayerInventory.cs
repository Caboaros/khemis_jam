using _Game.Scripts.HUD;
using Blazewing.DataController;
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

                CrystalsCollected crystalsCollected = new CrystalsCollected(_crystalCollected);

                DataEvent.Notify(crystalsCollected);
                DataController.Add(crystalsCollected);
            }
        }

        private void Start()
        {
            CrystalsCollected = 0;
        }
    }
}
