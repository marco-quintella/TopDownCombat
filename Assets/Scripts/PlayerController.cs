using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 4f;
    private PlayerControls _playerControls;
    private Vector2 _movement;
    private Rigidbody2D _rb;
    private Animator _animator;
    private SpriteRenderer _spriteRenderer;
    private static readonly int MoveX = Animator.StringToHash("moveX");
    private static readonly int MoveY = Animator.StringToHash("moveY");
    
    public float direction { get; private set; }

    private void Awake()
    {
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
        direction = ((Vector2)transform.position - mousePosition).x;
        _spriteRenderer.flipX = direction switch
        {
            > 0 => true,
            < 0 => false,
            _ => _spriteRenderer.flipX
        };
    }

    private void Move()
    {
        // _rb.velocity = _movement * moveSpeed;
        _rb.MovePosition(_movement * (moveSpeed * Time.fixedDeltaTime) + _rb.position);
    }
}