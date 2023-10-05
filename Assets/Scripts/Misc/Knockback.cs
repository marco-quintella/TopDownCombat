using System.Collections;
using UnityEngine;

namespace Misc
{
    public class Knockback : MonoBehaviour
    {
        public bool GettingKnockedBack { get; private set; }

        private Rigidbody2D _rb;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
        }

        public void GetKnockedBack(Transform damageSource, float knockbackThrust)
        {
            GettingKnockedBack = true;
            Vector2 direction = transform.position - damageSource.position;
            Vector2 force = direction.normalized * knockbackThrust * _rb.mass;
            _rb.AddForce(force, ForceMode2D.Impulse);
            StartCoroutine(ResetKnockback());
        }

        private IEnumerator ResetKnockback()
        {
            yield return new WaitForSeconds(.2f);
            _rb.velocity = Vector2.zero;
            GettingKnockedBack = false;
        }
    }
}