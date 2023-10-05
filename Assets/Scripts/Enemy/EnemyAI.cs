using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Enemy
{
    public class EnemyAI : MonoBehaviour
    {
        private enum State
        {
            Roaming
        }

        [SerializeField] private State state;
        private EnemyPathfinding _enemyPathfinding;

        private void Awake()
        {
            state = State.Roaming;
            _enemyPathfinding = GetComponent<EnemyPathfinding>();
        }

        private void Start()
        {
            StartCoroutine(RoamingRoutine());
        }

        private IEnumerator RoamingRoutine()
        {
            while (state == State.Roaming)
            {
                Vector2 roamingPosition = GetRoamingPosition();
                _enemyPathfinding.MoveTo(roamingPosition);
                yield return new WaitForSeconds(2f);
            }
        }

        private Vector2 GetRoamingPosition()
        {
            return new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
        }
    }
}