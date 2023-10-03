using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Sword : MonoBehaviour
{
    private PlayerControls _playerControls;
    private Animator _animator;
    private PlayerController _playerController;
    private ActiveWeapon _activeWeapon;

    private static readonly int AttackTrigger = Animator.StringToHash("Attack");

    private void Awake()
    {
        _playerControls = new PlayerControls();
        _animator = GetComponent<Animator>();
        _playerController = GetComponentInParent<PlayerController>();
        _activeWeapon = GetComponentInParent<ActiveWeapon>();
    }

    private void OnEnable()
    {
        _playerControls.Enable();
    }

    private void Start()
    {
        _playerControls.Combat.Attack.started += Attack;
    }

    private void Update()
    {
        MouseFollowWithOffset();
    }

    private void Attack(InputAction.CallbackContext _)
    {
        _animator.SetTrigger(AttackTrigger);
    }

    private void MouseFollowWithOffset()
    {
        float angle = Mathf.Atan2(Input.mousePosition.y, Input.mousePosition.x) * Mathf.Rad2Deg;

        _activeWeapon.transform.rotation = _playerController.direction switch
        {
            > 0 => Quaternion.Euler(0, -180, angle),
            < 0 => Quaternion.Euler(0, 0, angle),
            _ => _activeWeapon.transform.rotation
        };
    }
}