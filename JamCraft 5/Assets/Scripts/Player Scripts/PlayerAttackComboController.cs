using UnityEngine;
using JamCraft5.Items;
using System.Collections;
using Utility.Development;
using JamCraft5.Player.Inventory;

namespace JamCraft5.Player.Attack
{
    public class PlayerAttackComboController : MonoBehaviour
    {
        #region Variables
        private PlayerInventoryManager playerInventoryManager;
        private bool attacking;
        private float currentComboTime;
        public static event System.EventHandler OnPlayerAttacked;
        public static event System.EventHandler OnPlayerStoppedAttacking;
        #endregion

        #region Awake
        private void Awake()
        {
            playerInventoryManager = FindObjectOfType<PlayerInventoryManager>();
        }
        #endregion

        #region OnEnable
        private void OnEnable()
        {
            playerInventoryManager.OnSelectedWeaponChanged += OnSelectedWeaponChanged;
        }
        #endregion

        #region OnDisable
        private void OnDisable()
        {
            playerInventoryManager.OnSelectedWeaponChanged -= OnSelectedWeaponChanged;
        }
        #endregion

        #region StartCheckingAttack
        public void StartCheckingAttack()
        {
            attacking = true;
            OnPlayerAttacked?.Invoke(this, System.EventArgs.Empty);
            StartCoroutine(StartCheckingAttackCoroutine());
        }
        #endregion

        #region StartCheckingAttackCoroutine
        private IEnumerator StartCheckingAttackCoroutine()
        {
            bool attacked = false;
            //localCurrentComboTime is used in order to prevent changing the global variable
            //every time this coroutine is executed
            float localCurrentComboTime = currentComboTime;
            while (localCurrentComboTime < 0)
            {
                if (Input.GetMouseButtonDown(MouseButton.LEFT))
                {
                    attacked = true;
                    break;
                }
                currentComboTime -= Time.deltaTime;
                yield return null;
            }

            attacking = attacked;
            if (!attacking)
            {
                OnPlayerStoppedAttacking?.Invoke(this, System.EventArgs.Empty);
            }
        }
        #endregion

        #region OnSelectedWeaponChanged
        private void OnSelectedWeaponChanged(object sender, EventArguments.OnSelectedWeaponChangedEventArgs e)
        {
            switch (InventoryWeapon.CurrentWeaponType)
            {
                case Weapons.WeaponType.Sword:
                    currentComboTime = PlayerAttack.SwordAttackAnimationClip.length;
                    break;
                case Weapons.WeaponType.Hammer:
                    currentComboTime = PlayerAttack.HammerAttackAnimationClip.length;
                    break;
                case Weapons.WeaponType.Halberd:
                    currentComboTime = PlayerAttack.HalberdAttackAnimationClip.length;
                    break;
                case Weapons.WeaponType.Shotgun:
                    currentComboTime = PlayerAttack.ShotgunAttackAnimationClip.length;
                    break;
            }
        }
        #endregion
    }
}
