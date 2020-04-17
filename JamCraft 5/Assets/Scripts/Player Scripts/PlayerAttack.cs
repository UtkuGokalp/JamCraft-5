using UnityEngine;
using System.Collections;
using Utility.Development;
using JamCraft5.Player.Inventory;
using JamCraft5.Player.Movement;

namespace JamCraft5.Player.Attack
{
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(PlayerInventoryManager))]
    public class PlayerAttack : MonoBehaviour
    {
        #region Variables
        [SerializeField]
        private Transform bullet;//The bullet for the shotgun, it should have attached the Bullet script

        [SerializeField]
        private BoxCollider saberCol;
        [SerializeField]
        private CapsuleCollider hammerCol;
        [SerializeField]
        private BoxCollider spearCol;
        [SerializeField]
        private AnimationClip hammerAttackAnimationClip;
        [SerializeField]
        private AnimationClip halberdAttackAnimationClip;

        public static bool Attacking { get; private set; }
        private int comboAttacks = 0;
        private bool pressedMouse = false;//Is here to detect if betwen combo attacks the mouse has been pressed
        private PlayerRotationController playerRotationController;
        private PlayerMovementController playerMovementController;
        private PlayerInventoryManager playerInventoryManager;
        private Animator animator;
        #endregion

        #region Awake
        private void Awake()
        {
            Attacking = false;
            animator = GetComponent<Animator>();
            playerRotationController = GetComponent<PlayerRotationController>();
            playerMovementController = GetComponent<PlayerMovementController>();
            playerInventoryManager = GetComponent<PlayerInventoryManager>();
        }
        #endregion

        #region Update
        private void Update()
        {
            if (Input.GetMouseButtonDown(MouseButton.LEFT) && !PlayerDashController.Dashing)
            {
                pressedMouse = true;
                if (!Attacking && playerInventoryManager.Weapons[playerInventoryManager.CurrentWeaponSlotIndex].ContainedWeapon != null)
                {
                    Attacking = true;
                    switch (playerInventoryManager.CurrentWeaponSlotIndex)
                    {
                        case 0:
                            animator.SetTrigger(GameUtility.SWORD_ATTACK_ANIMATION_TRIGGER_NAME);
                            if (comboAttacks == 0)
                            {
                                StartCoroutine(AttackSaber());
                            }
                            else if (comboAttacks < 3)//If comboAttacks is < AttacksBeforeCooldown
                            {
                                StartCoroutine(ComboAttack());
                            }
                            break;
                        case 1:
                            animator.SetTrigger(GameUtility.HAMMER_ATTACK_ANIMATION_TRIGGER_NAME);
                            StartCoroutine(AttackBase(hammerCol, hammerAttackAnimationClip.length));
                            break;
                        case 2:
                            animator.SetTrigger(GameUtility.HALBERD_ATTACK_ANIMATION_TRIGGER_NAME);
                            StartCoroutine(AttackBase(spearCol, halberdAttackAnimationClip.length));
                            break;
                        case 3:
                            animator.SetTrigger(GameUtility.SHOTGUN_ATTACK_ANIMATION_TRIGGER_NAME);
                            StartCoroutine(gunAttack());
                            break;
                    }
                }

            }
        }
        #endregion

        #region Saber

        #region Attack
        private IEnumerator AttackSaber()
        {
            playerRotationController.enabled = false;//for the player not to move while attacking
            playerMovementController.enabled = false;
            //start the attack animation
            yield return new WaitForSeconds(0.2f);//fix this value with the animation
            saberCol.enabled = true;
            yield return new WaitForSeconds(0.1f);//fix this value with the animation
            saberCol.enabled = false;
            playerRotationController.enabled = true;
            playerMovementController.enabled = true;
            Attacking = false;
            StartCoroutine(Combo());
        }
        #endregion

        #region ComboCount
        private IEnumerator Combo()
        {
            pressedMouse = false;
            comboAttacks++;
            yield return new WaitForSeconds(0.4f);
            if (!pressedMouse)
            {
                comboAttacks = 0;
            }
        }
        #endregion

        #region ComboAttack
        private IEnumerator ComboAttack()
        {
            /*summary
             it makes a combo attack, faster than the frist attack
             needed to adjust the times of the animation and balance it         
             */
            Attacking = true;
            playerRotationController.enabled = false;
            playerMovementController.enabled = false;
            //start the attck animation
            yield return new WaitForSeconds(0.1f);//the combo attacks would be faster
            saberCol.enabled = true;
            yield return new WaitForSeconds(0.05f);//the combo attacks would be faster
            saberCol.enabled = false;
            playerRotationController.enabled = true;
            playerMovementController.enabled = true;
            Attacking = false;
            if (comboAttacks > 1)//comboAttacks = numberOfAttacksBeforeCooldown(3) - 2 
            {
                yield return new WaitForSeconds(0.1f);//(need to adjust it) Cooldown before continue attacking
                comboAttacks = 0;
            }
            else
            {
                StartCoroutine(Combo());
            }
        }
        #endregion

        #endregion

        #region gunAttack
        IEnumerator gunAttack()
        {
            WeaponPositionReferenceScript reference = FindObjectOfType<WeaponPositionReferenceScript>();
            if (reference != null)
            {
                playerRotationController.enabled = false;
                playerMovementController.enabled = false;
                yield return new WaitForSeconds(0.2f);//fix this value with the animation
                for (int i = Random.Range(3, 8); i > 0; i--)
                {
                    Instantiate(bullet, reference.GetTrans().position, reference.GetTrans().rotation, null);//Assign the hand transform                
                }
                yield return new WaitForSeconds(0.1f);//fix this value with the animation
                playerRotationController.enabled = true;
                playerMovementController.enabled = true;
                Attacking = false;
            }
        }
        #endregion

        #region AttackBase
        IEnumerator AttackBase(Collider box, float T2)
        {
            playerRotationController.enabled = false;
            playerMovementController.enabled = false;
            //HERE
            box.enabled = true;
            yield return new WaitForSeconds(T2);//fix this value with the animation
            box.enabled = false;
            playerRotationController.enabled = true;
            playerMovementController.enabled = true;
            Attacking = false;
        }
        #endregion
    }
}
