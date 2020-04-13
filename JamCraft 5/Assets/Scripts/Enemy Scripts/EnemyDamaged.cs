using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utility.Health;
using JamCraft5.Inventory;
using JamCraft5.Items;

namespace JamCraft5.Enemies.Components
{
    [RequireComponent(typeof(EnemyState))]
    public class EnemyDamaged : MonoBehaviour
    {
        #region Variables
        private HealthSystem hs;
        private Rigidbody rb;
        private EnemyState enemyState;

        private InventoryItem wep;
        private InventoryItem grenade;
        #endregion

        private void Awake()
        {
            hs = GetComponent<HealthSystem>();
            rb = GetComponent<Rigidbody>();
            enemyState = GetComponent<EnemyState>();

            wep = new InventoryItem(new ItemsBase());
            wep.ItemData.weaponDamage = 10;
            grenade = new InventoryItem(new ItemsBase());
            wep.ItemData.grenadeDamage = 10;
        }

        private void OnTriggerEnter(Collider col)
        {
            if (col.CompareTag("PlayerAttack"))
            {
                StartCoroutine(GetDamage());
            } else if (col.CompareTag("GrenadeExplosion"))
            {
                StartCoroutine(GetGrenadeDamage());
            }
        }

        IEnumerator GetDamage()
        {
            enemyState.StateOfEnemy = EnemyStateEnum.Damaging;
            rb.AddRelativeForce(0, 0, 1/*Insert weapon Knockback*/, ForceMode.Impulse);
            hs.DecreaseHealth(wep.ItemData.weaponDamage);
            yield return new WaitForSeconds(0.1f);
            enemyState.StateOfEnemy = EnemyStateEnum.Idle;
        }

        IEnumerator GetGrenadeDamage()
        {
            enemyState.StateOfEnemy = EnemyStateEnum.Damaging;
            rb.AddRelativeForce(0,0,1/*Insert grenade Knockback*/, ForceMode.Impulse);
            hs.DecreaseHealth(grenade.ItemData.grenadeDamage);
            yield return new WaitForSeconds(0.1f);
            enemyState.StateOfEnemy = EnemyStateEnum.Idle;
        }
    }
}

