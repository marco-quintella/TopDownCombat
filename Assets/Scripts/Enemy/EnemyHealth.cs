using UnityEngine;

namespace Enemy
{
    public class EnemyHealth : MonoBehaviour
    {
        [SerializeField] private int initialHealth = 3;

        private int _currentHealth;

        private void Start()
        {
            _currentHealth = initialHealth;
        }

        public void TakeDamage(int damage)
        {
            _currentHealth -= damage;
            Debug.Log(_currentHealth);
            DetectDeath();
        }

        private void DetectDeath()
        {
            if (_currentHealth <= 0)
                Destroy(gameObject);
        }
    }
}