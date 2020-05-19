using UnityEngine;
using Utility.Health;
using JamCraft5.Items;
using System.Collections;
using Utility.Development;
using System.Collections.Generic;
using JamCraft5.Player.Inventory;
using JamCraft5.Items.Controllers;

namespace JamCraft5.Enemies.Components
{
    [RequireComponent(typeof(Collider))]
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(EnemyState))]
    [RequireComponent(typeof(HealthSystem))]
    [RequireComponent(typeof(GroundedItemDropController))]
    public class EnemyDamaged : MonoBehaviour
    {
        #region Variables
        private Rigidbody rb;
        private HealthSystem hs;
        private EnemyState enemyState;
        private Collider colliderComponent;

        private Animator animator;
        private AnimationClip deathAnimation;
        private InventoryItem grenade;
        private PlayerInventoryManager playerInventoryManager;
        private GroundedItemDropController groundedItemDropController;
        private bool dying;
        #endregion

        #region Awake
        private void Awake()
        {
            rb = GetComponent<Rigidbody>();
            hs = GetComponent<HealthSystem>();
            animator = GetComponent<Animator>();
            enemyState = GetComponent<EnemyState>();
            colliderComponent = GetComponent<Collider>();
            playerInventoryManager = GameUtility.Player.GetComponent<PlayerInventoryManager>();
            groundedItemDropController = GetComponent<GroundedItemDropController>();
            grenade = new InventoryItem(new ItemsBase());
        }
        #endregion

        #region Start
        private void Start()
        {
            foreach (AnimationClip clip in animator.runtimeAnimatorController.animationClips)
            {
                if (clip.name.ToLower().Contains("death"))
                {
                    deathAnimation = clip;
                    break;
                }
            }
        }
        #endregion

        #region OnEnable
        private void OnEnable()
        {
            hs.OnDeath += OnDeath;
        }
        #endregion

        #region OnDisable
        private void OnDisable()
        {
            hs.OnDeath -= OnDeath;
        }
        #endregion

        private void OnTriggerEnter(Collider col)
        {
            if (col.CompareTag(GameUtility.PLAYER_TAG))
            {
                StartCoroutine(GetDamage());
            }
            else if (col.CompareTag("GrenadeExplosion"))
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
            if (!hs.IsAlive) OnDeath();
            yield return new WaitForSeconds(0.1f);
            enemyState.StateOfEnemy = currentEnemyState;
        }

        IEnumerator GetGrenadeDamage()
        {
            EnemyStateEnum currentEnemyState = enemyState.StateOfEnemy;
            enemyState.StateOfEnemy = EnemyStateEnum.Damaging;
            rb.AddRelativeForce(0, 0, 1/*Insert grenade Knockback*/, ForceMode.Impulse);
            hs.DecreaseHealth(grenade.ItemData.grenadeDamage);
            yield return new WaitForSeconds(0.1f);
            enemyState.StateOfEnemy = currentEnemyState;
        }

        #region OnDeath
        private void OnDeath()
        {
            if (!dying)
            {
                dying = true;
                StartCoroutine(OnDeathCoroutine());
            }
        }
        #endregion

        #region OnDeathCoroutine
        private IEnumerator OnDeathCoroutine()
        {
            groundedItemDropController.DropItems();
            animator.SetTrigger("Dead");
            Physics.IgnoreCollision(colliderComponent, GameUtility.PlayerCollider);
            GetComponent<EnemyChasePlayerComponent>().enabled = false;
            yield return new WaitForSeconds(deathAnimation.length);
            Destroy(gameObject);
        }
        #endregion
    }
}

