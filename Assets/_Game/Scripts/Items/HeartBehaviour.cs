using _Game.Scripts.Player;

namespace _Game.Scripts.Items
{
    public class HeartBehaviour : ItemBase
    {
        public override void OnCollect(PlayerController player)
        {
            player.Life.Heal(1);
        
            Destroy(gameObject);
        }
    }
}
