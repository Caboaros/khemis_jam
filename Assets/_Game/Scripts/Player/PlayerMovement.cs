using Sirenix.OdinInspector;
using UnityEngine;

namespace _Game.Scripts.Player
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField, ReadOnly] private MovementDirection _movementDirection;
        [Space]
        [SerializeField] private float movementSpeed = 5;
        [Space] [SerializeField] private Transform rendererTransform;

        public MovementDirection MovementDirection
        {
            get => _movementDirection;
            set
            {
                if (_movementDirection == value) return;
                
                _movementDirection = value;
                
                PlayerController.Instance.Animations.SetDirection(_movementDirection);
                PlayerController.Instance.Combat.SetDirection(_movementDirection);
            }
        }

        public bool CanMove
        {
            get => _canMove;
            set => _canMove = value;
        }

        [HideInInspector] public Vector2 inputDirection;
        
        private bool _canMove;
        private Vector2 _position = Vector2.zero;
        private Rigidbody2D _rigidbody;

        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _position = _rigidbody.position;
            _canMove = true;

            MovementDirection = MovementDirection.Down;
        }

        public void StopMovement()
        {
            _canMove = false;
            _rigidbody.velocity = Vector2.zero;
        }

        private void Update()
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
            
            PlayerController.Instance.Animations.MovementAnimation(vertical, horizontal);

            if (vertical == 0 && horizontal == 0)
            {
                PlayerController.Instance.Status = PlayerStatus.Idle;
                return;
            }
            
            PlayerController.Instance.Status = PlayerStatus.Walking;
            
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
        None = -1, 
        Top = 0,
        Down = 1,
        Left = 2,
        Right = 3
    }
}