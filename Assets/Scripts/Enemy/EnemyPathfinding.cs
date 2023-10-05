using UnityEngine;

namespace Enemy
{
    public class EnemyPathfinding : MonoBehaviour
    {
        [SerializeField] private float moveSpeed = 4f;
        private Vector2 _direction;

        private Knockback _knockback;
        private Rigidbody2D _rb;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
            _knockback = GetComponent<Knockback>();
        }

        private void FixedUpdate()
        {
            if (!_knockback.GettingKnockedBack)
                _rb.MovePosition(_rb.position + _direction * (moveSpeed * Time.fixedDeltaTime));
        }

        public void MoveTo(Vector2 targetPosition)
        {
            _direction = targetPosition;
        }
    }
}