using System.Collections;
using UnityEngine;
using JamCraft5.Items;
using JamCraft5.Inventory;
using Utility.Development;

namespace JamCraft5.Player.Attack
{
    public class PlayerAttack : MonoBehaviour
    {
        #region Variables
        JamCraft5.Player.Movement.PlayerRotationController rotation;
        JamCraft5.Player.Movement.PlayerMovementController pMov;

        private InventoryItem wep;
        [SerializeField]
        private Transform bullet;//The bullet for the shotgun, it should have attached the Bullet script

        [SerializeField]
        private BoxCollider saberCol;
        [SerializeField]
        private CapsuleCollider hammerCol;
        [SerializeField]
        private BoxCollider spearCol;

        public bool attacking { get; private set;}
        private int comboAttacks = 0;
        private bool pressedMouse = false;//Is here to detect if betwen combo attacks the mouse has been pressed
        #endregion

        #region Awake
        private void Awake()
        {
            wep = GetComponent<Inventory.PlayerInventoryManager>().SelectedWeapon.ContainedWeapon;
            //Make that the wep gets from the inventory
            
            //TEST
            if (wep == null)
            {
                wep = new InventoryItem(new ItemsBase());
                wep.ItemData.weaponRange = 2;
            }

            attacking = false;

            rotation = GetComponent<JamCraft5.Player.Movement.PlayerRotationController>();
            pMov = GetComponent<JamCraft5.Player.Movement.PlayerMovementController>();
        }
        #endregion

        #region Update
        private void Update()
        {

            if (Input.GetMouseButtonDown(MouseButton.LEFT))
            {
                pressedMouse = true;
                if (!attacking)
                {
                    attacking = true;
                    //Test code
                    GetComponent<Animator>().SetBool("IsAttackingWithSword", attacking);

                    switch (GetComponent<Inventory.PlayerInventoryManager>().CurrentWeaponSlotIndex)
                    {
                        case 0:
                            
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
                            StartCoroutine(AttackBase(hammerCol, 2f, 2f));//fix the times with animations

                            break;

                        case 2:
                            StartCoroutine(AttackBase(spearCol, 2f, 2f));//fix the times with animations
                            break;
                            
                        case 3:
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
            rotation.enabled = false;//for the player not to move while attacking
            pMov.enabled = false;
            //start the attack animation
            yield return new WaitForSeconds(0.2f);//fix this value with the animation
            saberCol.enabled = true;
            yield return new WaitForSeconds(0.1f);//fix this value with the animation
            saberCol.enabled = false;
            rotation.enabled = true;
            pMov.enabled = true;
            attacking = false;
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
            attacking = true;
            rotation.enabled = false;
            pMov.enabled = false;
            //start the attck animation
            yield return new WaitForSeconds(0.1f);//the combo attacks would be faster
            saberCol.enabled = true;
            yield return new WaitForSeconds(0.05f);//the combo attacks would be faster
            saberCol.enabled = false;
            rotation.enabled = true;
            pMov.enabled = true;
            attacking = false;
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
            Transform reference = FindObjectOfType<WeaponPositionReferenceScript>().GetTrans();
            rotation.enabled = false;
            pMov.enabled = false;
            yield return new WaitForSeconds(0.2f);//fix this value with the animation
            for (int i = Random.Range(3,8); i>0; i--)
            {
                Instantiate(bullet, reference.position, reference.rotation, null);//Assign the hand transform                
            }
            yield return new WaitForSeconds(0.1f);//fix this value with the animation
            rotation.enabled = true;
            pMov.enabled = true;
            attacking = false;
        }
        #endregion

        #region AttackBase
        IEnumerator AttackBase(Collider box, float T1, float T2)
        {
            rotation.enabled = false;
            pMov.enabled = false;
            yield return new WaitForSeconds(T1);//fix this value with the animation 
            //caps.enabled = true;
            box.enabled = true;
            yield return new WaitForSeconds(T2);//fix this value with the animation
            box.enabled = false;
            rotation.enabled = true;
            pMov.enabled = true;
            attacking = false;
        }
        #endregion
    }
}

