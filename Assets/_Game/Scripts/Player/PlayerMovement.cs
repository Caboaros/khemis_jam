using UnityEngine;

namespace _Game.Scripts.Player
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private float movementSpeed = 5;
        [Space] [SerializeField] private Transform rendererTransform;

        public MovementDirection MovementDirection
        {
            get => _movementDirection;
            set
            {
                if (_movementDirection == value) return;
                
                _movementDirection = value;
                _animation.SetDirection(_movementDirection);
            }
        }

        public bool CanMove
        {
            get => _canMove;
            set => _canMove = value;
        }

        [SerializeField] private MovementDirection _movementDirection;

        [HideInInspector] public Vector2 inputDirection;
        
        private bool _canMove;
        private Vector2 _position = Vector2.zero;
        private Vector2 _rendererPosition;
        private Transform _transform;
        private Rigidbody2D _rigidbody;
        private float _sin = 0;
        private PlayerAnimations _animation;

        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _animation = GetComponentInChildren<PlayerAnimations>();
            
            _transform = transform;
            _position = _rigidbody.position;
            _rendererPosition = rendererTransform.localPosition;
            _canMove = true;
        }

        public void StopMovement()
        {
            _canMove = false;
            _rigidbody.velocity = Vector2.zero;
        }

        private void FixedUpdate()
        {
            if (!_canMove) return;

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
            
            _animation.MovementAnimation(vertical, horizontal);

            if (vertical == 0 && horizontal == 0) return;
            
            inputDirection = new Vector2(horizontal, vertical);
            MovementDirection = GetDirection(inputDirection);

            _rigidbody.MovePosition(_position);
        }

        private MovementDirection GetDirection(Vector2 input)
        {
            if (Mathf.Abs(input.x) > Mathf.Abs(input.y))
            {
                if (input.x > 0)
                    return MovementDirection.Right;
                else
                    return MovementDirection.Left;
            }
            else
            {
                if (input.y > 0)
                    return MovementDirection.Top;
                else
                    return MovementDirection.Down;
            }
        }
    }

    public enum MovementDirection
    {
        Top,
        Down,
        Left,
        Right
    }
}