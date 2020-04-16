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

        private InventoryItem wep;
        [SerializeField]
        private Transform bullet;//The bullet for the shotgun, it should have attached the Bullet script

        public BoxCollider attackColider { get; private set; }
        public CapsuleCollider attackCapsColider { get; private set; }
        private bool attacking = false;
        private int comboAttacks = 0;
        private bool pressedMouse = false;//Is here to detect if betwen combo attacks the mouse has been pressed
        #endregion

        #region Awake
        private void Awake()
        {
            wep = GetComponent<Inventory.PlayerInventoryManager>().SelectedWeapon.ContainedWeapon;
            //Make that the wep gets from the inventory

            rotation = GetComponent<JamCraft5.Player.Movement.PlayerRotationController>();
            attackColider = gameObject.AddComponent<BoxCollider>();
            attackColider.enabled = false;
            attackCapsColider = gameObject.AddComponent<CapsuleCollider>();
            attackCapsColider.enabled = false;
        }
        #endregion

        #region CreateRectColider
        BoxCollider CreateRectColider(BoxCollider box)
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
                Debug.LogError("There's an error with the weapon");
                return null;
            }
        }
        #endregion

        #region CreateHammerColider
        CapsuleCollider CreateHammerColider(CapsuleCollider box)
        {
            /*sumary
             Create a capsule colider with the radius = range of the current weapon
             */
            if (wep != null)
            {
                box.isTrigger = true;
                box.direction = 1;
                box.center = Vector3.forward * wep.ItemData.weaponRange / 2;
                box.radius = wep.ItemData.weaponRange / 2;
                box.height = 2;
                box.enabled = false;
                box.tag = "PlayerAttack";//The tag that the enemy will detect
                return box;
            }
            else
            {
                Debug.LogError("There's an error with the weapon range");
                return null;
            }
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
                    switch (GetComponent<Inventory.PlayerInventoryManager>().CurrentWeaponSlotIndex)
                    {
                        case 0:
                            CreateRectColider(attackColider);
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
                            CreateHammerColider(attackCapsColider);
                            StartCoroutine(hammerAttack());
                            break;

                        case 2:
                            CreateRectColider(attackColider);
                            StartCoroutine(spearAttack());
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
            else
            {
                StartCoroutine(Combo());
            }
        }
        #endregion

        #endregion

        #region hammerAttack
        IEnumerator hammerAttack()
        {
            rotation.enabled = false;
            yield return new WaitForSeconds(0.2f);//fix this value with the animation 
            attackCapsColider.enabled = true;
            yield return new WaitForSeconds(0.1f);//fix this value with the animation
            attackCapsColider.enabled = false;
            rotation.enabled = true;
            attacking = false;
        }
        #endregion

        #region spearAttack
        IEnumerator spearAttack()
        {
            //The code is essentialy = to hammer. but the times to wait may be changed
            rotation.enabled = false;
            yield return new WaitForSeconds(0.2f);//fix this value with the animation 
            attackCapsColider.enabled = true;
            yield return new WaitForSeconds(0.1f);//fix this value with the animation
            attackCapsColider.enabled = false;
            rotation.enabled = true;
            attacking = false;
        }
        #endregion

        IEnumerator gunAttack()
        {
            Transform reference = FindObjectOfType<WeaponPositionReferenceScript>().Trans;
            rotation.enabled = false;
            yield return new WaitForSeconds(0.2f);//fix this value with the animation
            for (int i = Random.Range(3,8); i>0; i--)
            {
                Instantiate(bullet, reference);//Assign the hand transform                
            }
            yield return new WaitForSeconds(0.1f);//fix this value with the animation
            rotation.enabled = true;
            attacking = false;
        }
    }
}

