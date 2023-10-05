using System.Collections;
using Misc;
using Player;
using UnityEngine;

namespace Enemy
{
    public class EnemyHealth : MonoBehaviour
    {
        [SerializeField] private int initialHealth = 3;
        
        private Knockback _knockback;
        private Flash _flash;

        private int _currentHealth;

        private void Awake()
        {
            _knockback = GetComponent<Knockback>();
            _flash = GetComponent<Flash>();
        }

        private void Start()
        {
            _currentHealth = initialHealth;
        }

        public void TakeDamage(int damage)
        {
            _currentHealth -= damage;
            StartCoroutine(FlashRoutine());
            _knockback.GetKnockedBack(PlayerController.Instance.transform, 15f);
        }

        private IEnumerator FlashRoutine()
        {
            yield return StartCoroutine(_flash.FlashRoutine());
            DetectDeath();
        }

        private void DetectDeath()
        {
            if (_currentHealth <= 0)
                Destroy(gameObject);
        }
    }
}