using System.Collections;
using UnityEngine;
using JamCraft5.Items;
using JamCraft5.Inventory;

namespace JamCraft5.Player.Attack
{
    public class PlayerAttack : MonoBehaviour
    {
        #region Variables
        JamCraft5.Player.Movement.PlayerRotationController rotation;

        private InventoryItem wep;

        public BoxCollider attackColider { get; private set; }
        private bool attacking = false;
        private int comboAttacks = 0;
        private bool pressedMouse = false;//Is here to detect if betwen combo attacks the mouse has been pressed
        #endregion

        #region Awake
        private void Awake()
        {
            wep = GetComponent<Inventory.PlayerInventoryManager>().SelectedWeapon.ContainedItem;
           //Make that the wep gets from the inventory

            rotation = GetComponent<JamCraft5.Player.Movement.PlayerRotationController>();
            attackColider = gameObject.AddComponent<BoxCollider>();
            CreateColider(attackColider);
        }
        #endregion

        #region CreateColider
        BoxCollider CreateColider(BoxCollider box)
        {
            /*sumary
             Create a box colider with the range of the current weapon
             */
            if (wep != null)
            {
                box.isTrigger = true;
                box.size = new Vector3(0.8f, 1, wep.ItemData.weaponRange);
                box.center = Vector3.forward * wep.ItemData.weaponRange / 2;
                box.enabled = false;
                box.tag = "PlayerAttack";//The tag that the enemy will detect
                return box;
            }
            else
            {
                print("There's an error with the weapon range");
                return null;
            }
        }
        #endregion

        #region Update
        private void Update()
        {

            if (Input.GetMouseButtonDown(0))
            {
                pressedMouse = true;
                if (!attacking)
                {
                    attacking = true;
                    if (comboAttacks == 0)
                    {
                        StartCoroutine(Attack());
                    }
                    else if (comboAttacks < 3)//If comboAttacks is < AttacksBeforeCooldown
                    {
                        StartCoroutine(ComboAttack());
                    }
                }
                
            }
        }
        #endregion

        #region Attack
        private IEnumerator Attack()
        {
            rotation.enabled = false;//for the player not to move while attacking
            //start the attack animation
            yield return new WaitForSeconds(0.2f);//fix this value with the animation
            attackColider.enabled = true;
            yield return new WaitForSeconds(0.1f);//fix this value with the animation
            attackColider.enabled = false;
            rotation.enabled = true;
            attacking = false;
            StartCoroutine(Combo());
        }
        #endregion

        #region Combo
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
            //start the attck animation
            yield return new WaitForSeconds(0.1f);//the combo attacks would be faster
            attackColider.enabled = true;
            yield return new WaitForSeconds(0.05f);//the combo attacks would be faster
            attackColider.enabled = false;
            rotation.enabled = true;
            attacking = false;
            if (comboAttacks > 1)//comboAttacks = numberOfAttacksBeforeCooldown(3) - 2 
            {
                yield return new WaitForSeconds(0.1f);//(need to adjust it) Cooldown before continue attacking
                comboAttacks = 0;
            }
            else {
                StartCoroutine(Combo());
            }
        }
        #endregion
    }
}

