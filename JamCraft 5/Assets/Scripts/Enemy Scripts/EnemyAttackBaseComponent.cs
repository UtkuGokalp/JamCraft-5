using UnityEngine;
using Utility.Development;

namespace JamCraft5.Enemies.Components
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(EnemyState))]
    public abstract class EnemyAttackBaseComponent : MonoBehaviour
    {
        #region Variables
        [SerializeField]
        private float attackRange;
        [SerializeField]
        private float attackRate;
        private float timeLeftForNextAttack;
        private EnemyState enemyState;
        private Transform transformCache;

        public float AttackRange => attackRange;
        public bool InAttackRange => Vector3.Distance(transformCache.position, GameUtility.PlayerPosition) < attackRange;
        #endregion

        #region Awake
        private void Awake()
        {
            transformCache = transform;
            enemyState = GetComponent<EnemyState>();
        }
        #endregion

        #region Update
        private void Update()
        {
            if (enemyState.StateOfEnemy == EnemyStateEnum.Damaging)
            {
                timeLeftForNextAttack = 0;
                return;
            }
            if (InAttackRange)
            {
                if (timeLeftForNextAttack <= 0)
                {
                    enemyState.StateOfEnemy = EnemyStateEnum.Attacking;
                    AttackOnce();
                    timeLeftForNextAttack = attackRate;
                }
                else
                {
                    timeLeftForNextAttack -= Time.deltaTime;
                }
            }
            else
            {
                timeLeftForNextAttack = 0;
                enemyState.StateOfEnemy = EnemyStateEnum.Idle;
            }
        }
        #endregion

        #region AttackOnce
        protected abstract void AttackOnce();
        #endregion

        #region OnValidate
        private void OnValidate()
        {
            if (attackRange < 0)
            {
                attackRange = 0;
            }
        }
        #endregion

        #region OnDrawGizmosSelected
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, attackRange);
        }
        #endregion
    }
}
