using UnityEngine;
using Utility.Development;
using JamCraft5.Enemies.Components;

namespace JamCraft5.Audio
{
    public class PlayerAttackMusicController : MonoBehaviour
    {
        #region Variables
        [SerializeField]
        private float enemyCheckRadius;
        [SerializeField]
        private float transitionTime;
        private Collider[] detectedEnemies;
        #endregion

        #region FixedUpdate
        private void FixedUpdate()
        {
            //1 << GameUtility.EnemyLayer.value returns an integer which has only 10th bit set to 1
            //(because GameUtility.EnemyLayer.value is 10)
            detectedEnemies = Physics.OverlapSphere(GameUtility.PlayerPosition, enemyCheckRadius, 1 << GameUtility.EnemyLayer.value);

            if (detectedEnemies.Length > 0)
            {
                foreach (Collider enemy in detectedEnemies)
                {
                    EnemyAttackBaseComponent enemyAttackController = enemy.GetComponent<EnemyAttackBaseComponent>();
                    if (enemyAttackController != null && enemyAttackController.Attacking)
                    {
                        if (AudioManager.Instance.PlayingIdleTrack)
                        {
                            AudioManager.Instance.TransitionToCombatTrack();
                        }
                        break;
                    }
                }
            }
            else
            {
                if (AudioManager.Instance.PlayingCombatTrack)
                {
                    AudioManager.Instance.TransitionToIdleTrack(transitionTime);
                }
            }
        }
        #endregion

        #region OnDrawGizmosSelected
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, enemyCheckRadius);
        }
        #endregion
    }
}
