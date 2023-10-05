using UnityEngine;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        public static PlayerController Instance { get; private set; }

        [SerializeField] private float moveSpeed = 4f;
        private PlayerControls _playerControls;
        private Vector2 _movement;
        private Rigidbody2D _rb;
        private Animator _animator;
        private SpriteRenderer _spriteRenderer;

        public bool FacingLeft { get; private set; }

        private static readonly int MoveX = Animator.StringToHash("moveX");
        private static readonly int MoveY = Animator.StringToHash("moveY");

        public float Direction { get; private set; }

        private void Awake()
        {
            if (Instance != null && Instance != this)
                Destroy(gameObject);
            else
                Instance = this;

            _playerControls = new PlayerControls();
            _rb = GetComponent<Rigidbody2D>();
            _animator = GetComponent<Animator>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void OnEnable()
        {
            _playerControls.Enable();
        }

        private void Update()
        {
            PlayerInput();
        }

        private void PlayerInput()
        {
            _movement = _playerControls.Movement.Move.ReadValue<Vector2>();
            _animator.SetFloat(MoveX, _movement.x);
            _animator.SetFloat(MoveY, _movement.y);
        }

        private void FixedUpdate()
        {
            AdjustPlayerFacingDirection();
            Move();
        }

        private void AdjustPlayerFacingDirection()
        {
            Vector2 mousePosition = (Vector2)Input.mousePosition - new Vector2(Screen.width / 2f, Screen.height / 2f);
            Direction = ((Vector2)transform.position - mousePosition).x;

            switch (Direction)
            {
                case > 0:
                    _spriteRenderer.flipX = true;
                    FacingLeft = true;
                    break;
                case < 0:
                    _spriteRenderer.flipX = false;
                    FacingLeft = false;
                    break;
            }
        }

        private void Move()
        {
            // _rb.velocity = _movement * moveSpeed;
            _rb.MovePosition(_movement * (moveSpeed * Time.fixedDeltaTime) + _rb.position);
        }
    }
}