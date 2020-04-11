using UnityEngine;

namespace JamCraft5.Enemies.Components
{
    public class EnemyMeleeAttackComponent : EnemyAttackBaseComponent
    {
        #region AttackOnce
        protected override void AttackOnce()
        {
            Debug.Log("Melee Enemy attacking to player...", gameObject);
        }
        #endregion
    }
}
