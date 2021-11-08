using UnityEngine;

namespace _Game.Scripts.Items
{
    public class FireBall : ThrowableItem
    {
        public override void Throw(Vector3 direction, float speed, LayerMask layer)
        {
            spriteTransform.localEulerAngles = new Vector3(0, 0, GetRotation(direction));

            base.Throw(direction, speed, layer);
        }

        private float GetRotation(Vector3 direction)
        {
            if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
            {
                return direction.x > 0 ? 0 : 180;
            }

            if (direction.y > 0)
                return 90;
            else
                return -90;
        }
    }
}