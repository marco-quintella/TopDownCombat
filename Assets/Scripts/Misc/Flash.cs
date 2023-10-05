using System.Collections;
using UnityEngine;

namespace Misc
{
    public class Flash : MonoBehaviour
    {
        [SerializeField] private Material flashMaterial;
        [SerializeField] private float flashDuration = .2f;

        private SpriteRenderer _spriteRenderer;

        private Material _defaultMaterial;

        private Material CurrentMaterial
        {
            set => _spriteRenderer.material = value;
        }

        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _defaultMaterial = _spriteRenderer.material;
        }

        public IEnumerator FlashRoutine()
        {
            CurrentMaterial = flashMaterial;
            yield return new WaitForSeconds(flashDuration);
            CurrentMaterial = _defaultMaterial;
        }
    }
}