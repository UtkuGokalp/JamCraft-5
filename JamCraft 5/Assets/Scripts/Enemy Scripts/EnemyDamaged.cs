using System.Collections;
using UnityEngine;
using Utility.Health;
using JamCraft5.Items;
using JamCraft5.Player.Inventory;

namespace JamCraft5.Enemies.Components
{
    [RequireComponent(typeof(EnemyState))]
    public class EnemyDamaged : MonoBehaviour
    {
        #region Variables
        private HealthSystem hs;
        private Rigidbody rb;
        private EnemyState enemyState;

        private InventoryItem grenade;
        private PlayerInventoryManager playerInventoryManager;
        #endregion

        private void Awake()
        {
            hs = GetComponent<HealthSystem>();
            rb = GetComponent<Rigidbody>();
            enemyState = GetComponent<EnemyState>();
            playerInventoryManager = GetComponent<PlayerInventoryManager>();
            grenade = new InventoryItem(new ItemsBase());
        }

        private void OnTriggerEnter(Collider col)
        {
            if (col.CompareTag("Player"))
            {
                StartCoroutine(GetDamage());
            } else if (col.CompareTag("GrenadeExplosion"))
            {
                StartCoroutine(GetGrenadeDamage());
            }
        }

        IEnumerator GetDamage()
        {
            EnemyStateEnum currentEnemyState = enemyState.StateOfEnemy;
            enemyState.StateOfEnemy = EnemyStateEnum.Damaging;
            rb.AddRelativeForce(0, 0, 1/*Insert weapon Knockback*/, ForceMode.Impulse);
            hs.DecreaseHealth(playerInventoryManager.SelectedWeapon.ContainedWeapon.ItemData.weaponDamage);
            yield return new WaitForSeconds(0.1f);
            enemyState.StateOfEnemy = currentEnemyState;
        }

        IEnumerator GetGrenadeDamage()
        {
            EnemyStateEnum currentEnemyState = enemyState.StateOfEnemy;
            enemyState.StateOfEnemy = EnemyStateEnum.Damaging;
            rb.AddRelativeForce(0,0,1/*Insert grenade Knockback*/, ForceMode.Impulse);
            hs.DecreaseHealth(grenade.ItemData.grenadeDamage);
            yield return new WaitForSeconds(0.1f);
            enemyState.StateOfEnemy = currentEnemyState;
        }
    }
}

