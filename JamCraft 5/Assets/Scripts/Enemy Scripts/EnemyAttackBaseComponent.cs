using UnityEngine;
using JamCraft5.Audio;
using Utility.Development;

namespace JamCraft5.Enemies.Components
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(EnemyState))]
    public abstract class EnemyAttackBaseComponent : MonoBehaviour
    {
        #region Variables
        [SerializeField]
        private float attackRange;
        private float attackRate;
        private float timeLeftForNextAttack;
        private Animator animator;
        private EnemyState enemyState;
        private Transform transformCache;

        protected float AttackRate
        {
            get
            {
                if (attackRate == default)
                {
                    foreach (AnimationClip clip in AnimatorComponent.runtimeAnimatorController.animationClips)
                    {
                        if (clip.name.ToLower().Contains("attack"))
                        {
                            attackRate = clip.length;
                        }
                    }
                }
                return attackRate;
            }
        }
        private Animator AnimatorComponent
        {
            get
            {
                if (animator == null)
                {
                    animator = GetComponent<Animator>();
                }
                return animator;
            }
        }
        private EnemyState EnemyState
        {
            get
            {
                if (enemyState == null)
                {
                    enemyState = GetComponent<EnemyState>();
                }
                return enemyState;
            }
        }
        protected Transform TransformCache
        {
            get
            {
                if (transformCache == null)
                {
                    transformCache = transform;
                }
                return transformCache;
            }
        }
        public float AttackRange => attackRange;
        public bool Attacking { get; private set; }
        public bool InAttackRange => Vector3.Distance(TransformCache.position, GameUtility.PlayerPosition) < attackRange;
        #endregion

        #region Update
        private void Update()
        {
            if (EnemyState.StateOfEnemy == EnemyStateEnum.Damaging)
            {
                timeLeftForNextAttack = 0;
                return;
            }
            if (InAttackRange)
            {
                if (Attacking == false)
                {
                    Attacking = true;
                }
                if (timeLeftForNextAttack <= 0)
                {
                    EnemyState.StateOfEnemy = EnemyStateEnum.Attacking;
                    AttackOnce();
                    timeLeftForNextAttack = AttackRate;
                }
                else
                {
                    timeLeftForNextAttack -= Time.deltaTime;
                }
            }
            else
            {
                if (Attacking == true)
                {
                    Attacking = false;
                }
                timeLeftForNextAttack = 0;
                EnemyState.StateOfEnemy = EnemyStateEnum.Idle;
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
        protected virtual void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, attackRange);
        }
        #endregion
    }
}
