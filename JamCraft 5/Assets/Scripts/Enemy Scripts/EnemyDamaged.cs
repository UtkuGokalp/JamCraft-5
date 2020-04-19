using System.Collections;
using UnityEngine;
using Utility.Health;
using JamCraft5.Items;
using JamCraft5.Player.Inventory;
using Utility.Development;
using JamCraft5.Items.Controllers;

namespace JamCraft5.Enemies.Components
{
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(EnemyState))]
    [RequireComponent(typeof(GroundedItemDropController))]
    public class EnemyDamaged : MonoBehaviour
    {
        #region Variables
        private HealthSystem hs;
        private Rigidbody rb;
        private EnemyState enemyState;

        private Animator animator;
        private AnimationClip deathAnimation;
        private InventoryItem grenade;
        private PlayerInventoryManager playerInventoryManager;
        private GroundedItemDropController groundedItemDropController;
        private bool dying;
        #endregion

        private void Awake()
        {
            rb = GetComponent<Rigidbody>();
            hs = GetComponent<HealthSystem>();
            animator = GetComponent<Animator>();
            enemyState = GetComponent<EnemyState>();
            playerInventoryManager = GameUtility.Player.GetComponent<PlayerInventoryManager>();
            groundedItemDropController = GetComponent<GroundedItemDropController>();
            grenade = new InventoryItem(new ItemsBase());
        }

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

        private void OnEnable()
        {
            hs.OnDeath += OnDeath;
        }

        private void OnDisable()
        {
            hs.OnDeath -= OnDeath;
        }

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

        private void OnDeath()
        {
            if (!dying)
            {
                dying = true;
                StartCoroutine(OnDeathCoroutine());
            }
        }

        private IEnumerator OnDeathCoroutine()
        {
            groundedItemDropController.DropItems();
            animator.SetTrigger("Dead");
            GetComponent<EnemyChasePlayerComponent>().enabled = false;
            yield return new WaitForSeconds(deathAnimation.length);
            Destroy(gameObject);
        }
    }
}

