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
        private float currentComboTime;
        private PlayerInventoryManager playerInventoryManager;
        public static bool Attacking { get; private set; }
        public static event System.EventHandler OnPlayerAttacked;
        public static event System.EventHandler OnPlayerStoppedAttacking;
        #endregion

        #region Awake
        private void Awake()
        {
            playerInventoryManager = FindObjectOfType<PlayerInventoryManager>();
            currentComboTime = PlayerAttack.SwordAttackAnimationClip.length;
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
            Attacking = true;
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
                    //If player is attacked, we should set the attacking variable to true too
                    //because that variable will be used to inform other classes whether 
                    //the player is currently in the middle of a combo or not
                    Attacking = attacked = true;
                    break;
                }
                localCurrentComboTime -= Time.deltaTime;
                yield return null;
            }

            //When executing this line, if the local variable attacked is false, 
            //that means player hasn't attacked so the attacking variable will be set 
            //to false.
            Attacking = attacked;

            if (!Attacking)
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
