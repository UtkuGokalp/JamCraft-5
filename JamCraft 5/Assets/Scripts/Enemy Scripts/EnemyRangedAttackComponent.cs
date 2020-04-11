using UnityEngine;

namespace JamCraft5.Enemies.Components
{
    public class EnemyRangedAttackComponent : EnemyAttackBaseComponent
    {
        #region AttackOnce
        protected override void AttackOnce()
        {
            Debug.Log("Ranged Enemy attacking to player...", gameObject);
        }
        #endregion
    }
}
