using System;
using UnityEngine;

public class EnemyPathfinding: MonoBehaviour
{
    [SerializeField] private float moveSpeed = 4f;
    private Vector2 _direction;
    
    private Rigidbody2D _rb;
    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        _rb.MovePosition(_rb.position + _direction * (moveSpeed * Time.fixedDeltaTime));
    }

    public void MoveTo(Vector2 targetPosition)
    {
        _direction = targetPosition;
    }
}