using UnityEngine;
using Utility.Health;
using Utility.Development;

namespace JamCraft5.Enemies.Components
{
    public class EnemyRangedAttackComponent : EnemyAttackBaseComponent
    {
        #region Variables
        [SerializeField]
        private int damage;
        private HealthSystem playerHealthSystem;
        #endregion

        #region Awake
        private void Awake()
        {
            playerHealthSystem = GameUtility.Player.GetComponent<HealthSystem>();
        }
        #endregion

        #region AttackOnce
        protected override void AttackOnce()
        {
            Collider[] colliders = Physics.OverlapSphere(TransformCache.position, AttackRange, GameUtility.PlayerLayer);
            if (colliders.Length > 0)
            {
                //We've hit the player
                playerHealthSystem.DecreaseHealth(damage);
            }
        }
        #endregion
    }
}
