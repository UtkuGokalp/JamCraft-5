using UnityEngine;
using Utility.Development;

namespace JamCraft5.Enemies.Components
{
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(EnemyChasePlayerComponent))]
    [RequireComponent(typeof(EnemyAttackBaseComponent))]
    public class EnemyAnimationController : MonoBehaviour
    {
        #region Variables
        private Animator animator;
        #endregion

        #region Awake
        private void Awake()
        {
            animator = GetComponent<Animator>();
        }
        #endregion

        #region FixedUpdate
        private void FixedUpdate()
        {
            float distanceToPlayer = Vector3.Distance(GameUtility.PlayerPosition, transform.position);

            float maxDistance = GetComponent<EnemyChasePlayerComponent>().DetectionRange;
            float minDistance = GetComponent<EnemyAttackBaseComponent>().AttackRange;

            distanceToPlayer /= maxDistance - minDistance;

            animator.SetFloat("DistanceToPlayer", distanceToPlayer);
        }
        #endregion
    }
}
