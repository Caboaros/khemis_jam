using _Game.Scripts.Player;

namespace _Game.Scripts.Items
{
    public class CrystalBehaviour : ItemBase
    {
        public override void OnCollect(PlayerController player)
        {
            MoveToPlayer(player.transform, () => player.Inventory.CrystalsCollected++);
        }
    }
}
