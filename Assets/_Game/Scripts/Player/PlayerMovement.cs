using UnityEngine;

namespace _Game.Scripts.Player
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private float movementSpeed = 5;
        [Space] [SerializeField] private Transform rendererTransform;
    
        private Vector2 _position = Vector2.zero;
        private Vector2 _rendererPosition;
        private Transform _transform;
        private Rigidbody2D _rigidbody;
        private float _sin = 0;

        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _transform = transform;
            _position = _rigidbody.position;
            _rendererPosition = rendererTransform.localPosition;
        }

        private void FixedUpdate()
        {
            float vertical = Input.GetAxis("Vertical");
            float horizontal = Input.GetAxis("Horizontal");

            if (horizontal != 0)
            {
                _position.x += horizontal * movementSpeed * Time.deltaTime;
            }

            if (vertical != 0)
            {
                _position.y += vertical * movementSpeed * Time.deltaTime;
            }

            _sin += Time.deltaTime * 25 * vertical;
            _rendererPosition.y = Mathf.Sin(_sin) / 15f;
            //rendererTransform.localPosition = _rendererPosition;

            if(vertical == 0 && horizontal == 0) return;
            
            _rigidbody.MovePosition(_position);
        }
    }
}
