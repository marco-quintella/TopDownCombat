using System;
using Player;
using UnityEngine;

namespace Enemy
{
    public class EnemyHealth : MonoBehaviour
    {
        [SerializeField] private int initialHealth = 3;
        
        private Knockback _knockback;

        private int _currentHealth;

        private void Awake()
        {
            _knockback = GetComponent<Knockback>();
        }

        private void Start()
        {
            _currentHealth = initialHealth;
        }

        public void TakeDamage(int damage)
        {
            _currentHealth -= damage;
            _knockback.GetKnockedBack(PlayerController.Instance.transform, 15f);
            DetectDeath();
        }

        private void DetectDeath()
        {
            if (_currentHealth <= 0)
                Destroy(gameObject);
        }
    }
}