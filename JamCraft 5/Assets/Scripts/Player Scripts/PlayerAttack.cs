using System.Collections;
using UnityEngine;

namespace JamCraft5.Player.Attack
{
    public class PlayerAttack : MonoBehaviour
    {
        #region Variables
        JamCraft5.Player.Movement.PlayerRotationController rotation;

        Weapon wep;/*when creating the items part, this class should be removed but 
        for now to apply the paroperties of the weapons I'll do it like this*/
        private BoxCollider attackColider;
        private bool attacking = false;
        private int comboAttacks = 0;
        #endregion

        #region ProvisionalWeaponClass
        //Just a provisional layout for the weapons
        public class Weapon
        {
            public float damage { get; private set; }
            public float attackRange { get; private set; }
            public Weapon(float dmg, float range)
            {
                damage = dmg;
                attackRange = range;
            }

        }
        #endregion

        #region Awake
        private void Awake()
        {
            wep = new Weapon(10, 2f);//that should be the access to the weapon in the inventory

            rotation = GetComponent<JamCraft5.Player.Movement.PlayerRotationController>();
            attackColider = CreateColider();
        }
        #endregion

        #region CreateColider
        BoxCollider CreateColider()
        {
            /*sumary
             Create a box colider with the range of the current weapon
             */
            if (wep != null)
            {
                BoxCollider box = new BoxCollider();
                box.isTrigger = true;
                box.size = new Vector3(0.8f, 1, wep.attackRange);
                box.center = Vector3.right * wep.attackRange / 2;
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
            if (Input.GetMouseButtonDown(0) && !attacking)
            {
                attacking = true;
                if (comboAttacks == 0)
                {
                    StartCoroutine(Attack());
                }
                else if (comboAttacks < 3)
                {
                    StartCoroutine(ComboAttack());
                }
            }
        }
        #endregion

        #region Attack
        private IEnumerator Attack()
        {
            rotation.enabled = false;
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

        #region ComboStart
        private IEnumerator Combo()
        {
            comboAttacks++;
            yield return new WaitForSeconds(1f);
            if (comboAttacks == 1)
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
            comboAttacks++;
            rotation.enabled = false;
            //start the attck animation
            yield return new WaitForSeconds(0.1f);//the combo attacks would be faster
            attackColider.enabled = true;
            yield return new WaitForSeconds(0.05f);//the combo attacks would be faster
            attackColider.enabled = false;
            rotation.enabled = true;
            attacking = false;
            if (comboAttacks > 2)
            {
                yield return new WaitForSeconds(0.1f);//need to adjust it
                comboAttacks = 0;
            }
        }
        #endregion
    }
}

