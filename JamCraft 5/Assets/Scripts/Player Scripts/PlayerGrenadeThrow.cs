using System.Collections;
using UnityEngine;
using JamCraft5.Inventory;
using JamCraft5.Items;

namespace JamCraft5.Player.Attack
{
    public class PlayerGrenadeThrow : MonoBehaviour
    {
        /*This script should be attached to the grenade
         As the grenade would have a cooldown we can use the same gameobject for
         every grenade, avoiding instatiate new gameobjects
         grenade is son of player
             */

        #region Variables
        InventorySlot grenades;
        private int grenadesLeft;
        public GameObject hand;

        private MeshRenderer render;
        private Rigidbody rb;
        private SphereCollider col, colision;
        public int throwingForce = 50;
        private bool cooldown = false;
        #endregion

        #region Awake
        private void Awake()
        {
            
            transform.parent = hand.transform;
            render = GetComponent<MeshRenderer>();
            render.enabled = false;
            rb = GetComponent<Rigidbody>();
            rb.useGravity = false;

            GrenadeCheck();
            col = gameObject.AddComponent<SphereCollider>();
            col = CreateColider(col);
            colision = gameObject.AddComponent<SphereCollider>();//another collider for the grenade not to fall into the ground
            colision.enabled = false;
        }
        #endregion

        #region CreateColider
        SphereCollider CreateColider(SphereCollider coli)
        {
            coli.isTrigger = true;
            coli.radius = grenades.ContainedItem.ItemData.grenadeRange;
            coli.tag = "Player";
            coli.enabled = false;
            return coli;
        }
        #endregion

        #region GrenadeCheck
        void GrenadeCheck()
        {
            foreach (InventorySlot i in GetComponentInParent<Inventory.PlayerInventoryManager>().Inventory)
            {
                if (i.ContainedItem.ItemData.type == 1)
                {
                    grenades = i;
                }
            }
            if (grenades == null)
            {
                grenades = new InventorySlot();
                //grenades.ItemCount = 0;
                //TEST
                grenades.ItemCount = 5;
                grenades.ContainedItem = new InventoryItem(new ItemsBase());
                grenades.ContainedItem.ItemData.type = 1;
                grenades.ContainedItem.ItemData.grenadeRange = 10;
                grenades.ContainedItem.ItemData.grenadeForce = 10;
                grenades.ContainedItem.ItemData.grenadeTime = 10;
            }
            grenadesLeft = grenades.ItemCount;
        }
        #endregion

        #region Update
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.G) && !cooldown)
            {
                GrenadeCheck();
                if (grenadesLeft >= 1)
                {
                    grenades.ItemCount--;
                    StartCoroutine(ThrowGrenade());
                }               
            }
        }
        #endregion

        #region ThrowGrenade
        IEnumerator ThrowGrenade()
        {
            cooldown = true;
            render.enabled = true;

            yield return new WaitForSeconds(1f);//fix it with the animation (player throwing grenade)
            transform.position = transform.parent.position +Vector3.up;
            transform.parent = null;
            rb.velocity = Vector3.zero;
            rb.useGravity = true;
            rb.AddForce(new Vector3(0, 0, throwingForce), ForceMode.Impulse);
            colision.enabled = true;

            yield return new WaitForSeconds(grenades.ContainedItem.ItemData.grenadeTime);
            col.enabled = true;
            //Maybe could create a particle system here, I'm not pretty good at that

            yield return new WaitForSeconds(0.1f);
            col.enabled = false;
            colision.enabled = false;
            rb.useGravity = false;
            transform.parent = hand.transform;
            transform.position = Vector3.zero;
            rb.velocity = Vector3.zero;
            yield return new WaitForSeconds(0.5f);//cooldown before new grenade
            cooldown = false;
        }
        #endregion
    }
}