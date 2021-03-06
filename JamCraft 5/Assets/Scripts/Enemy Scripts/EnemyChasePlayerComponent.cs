﻿using UnityEngine;
using Utility.Development;

namespace JamCraft5.Enemies.Components
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(EnemyState))]
    public class EnemyChasePlayerComponent : MonoBehaviour
    {
        #region Variables
        [SerializeField]
        private float chaseSpeed;
        [SerializeField]
        private float detectionRange;
        private EnemyState enemyState;
        private Transform transformCache;
        private Rigidbody rb;

        public float DetectionRange => detectionRange;
        public bool PlayerIsInDetectionRange => Vector3.Distance(transformCache.position, GameUtility.PlayerPosition) < detectionRange;
        #endregion

        #region Awake
        private void Awake()
        {
            transformCache = transform;
            enemyState = GetComponent<EnemyState>();
            rb = GetComponent<Rigidbody>();
        }
        #endregion

        #region FixedUpdate
        private void FixedUpdate()
        {
            if (PlayerIsInDetectionRange)
            {
                if (enemyState.StateOfEnemy != EnemyStateEnum.Damaging)
                {
                    if (enemyState.StateOfEnemy != EnemyStateEnum.Attacking)
                    {
                        enemyState.StateOfEnemy = EnemyStateEnum.Chasing;
                        MoveTowardsPlayer();
                    }
                    else
                    {
                        rb.velocity = Vector3.zero;
                    }
                }
                else
                {
                    rb.velocity = Vector3.zero;
                }
            }
        }
        #endregion

        #region MoveTowardsPlayer
        private void MoveTowardsPlayer()
        {
            Vector3 direction = GameUtility.GetDirection(transformCache.position, GameUtility.PlayerPosition);
            rb.velocity = direction * chaseSpeed * Time.fixedDeltaTime;
        }
        #endregion

        #region OnValidate
        private void OnValidate()
        {
            if (detectionRange < 0)
            {
                detectionRange = 0;
            }
        }
        #endregion

        #region OnDrawGizmosSelected
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, detectionRange);
        }
        #endregion
    }
}
