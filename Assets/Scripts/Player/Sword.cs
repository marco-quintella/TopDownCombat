using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class Sword : MonoBehaviour
    {
        [SerializeField] private GameObject slashAnimationPrefab;
        [SerializeField] private Transform slashAnimationParent;
        [SerializeField] private Transform weaponCollider;
    
        private PlayerControls _playerControls;
        private Animator _animator;
        private PlayerController _playerController;
        private ActiveWeapon _activeWeapon;
    
        private GameObject _slashAnimation;

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
            weaponCollider.gameObject.SetActive(true);
            _slashAnimation = Instantiate(slashAnimationPrefab, slashAnimationParent.position, Quaternion.identity);
            _slashAnimation.transform.parent = slashAnimationParent;
        }

        public void DoneAttackingAnimationEvent()
        {
            weaponCollider.gameObject.SetActive(false);
        }

        private void SwingUpFlipAnimation()
        {
            _slashAnimation.gameObject.transform.rotation = Quaternion.Euler(-180, 0, 0);
            if (_playerController.FacingLeft)
            {
                _slashAnimation.GetComponent<SpriteRenderer>().flipX = true;
            } 
        }
    
        private void SwingDownFlipAnimation()
        {
            _slashAnimation.gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
            if (_playerController.FacingLeft)
            {
                _slashAnimation.GetComponent<SpriteRenderer>().flipX = true;
            } 
        }

        private void MouseFollowWithOffset()
        {
            float angle = Mathf.Atan2(Input.mousePosition.y, Input.mousePosition.x) * Mathf.Rad2Deg;
        
            Transform activeWeaponRotation = _activeWeapon.transform;

            switch (_playerController.direction)
            { 
                case > 0:
                    activeWeaponRotation.rotation = Quaternion.Euler(0, -180, angle);
                    weaponCollider.transform.rotation = Quaternion.Euler(0, -180, 0);
                    break;
                case < 0:
                    activeWeaponRotation.rotation = Quaternion.Euler(0, 0, angle);
                    weaponCollider.transform.rotation = Quaternion.Euler(0, 0, 0);
                    break;
            }
        }
    }
}