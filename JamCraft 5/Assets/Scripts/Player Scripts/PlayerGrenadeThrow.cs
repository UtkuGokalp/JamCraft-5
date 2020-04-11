using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        Grenade grenade;
        int grenadesLeft;
        public GameObject hand;

        private MeshRenderer render;
        private Rigidbody rb;
        private SphereCollider col, colision;
        public int throwingForce = 10;
        private bool cooldown = false;
        #endregion

        #region ProvisionalGrenadeClass
        //Just a provisional layout for the grenade
        public class Grenade
        {
            public float damage { get; private set; }
            public float explosionRadius { get; private set; }
            public float explosionTime { get; private set; }
            public Grenade(float dmg, float range, float expTime)
            {
                damage = dmg;
                explosionRadius = range;
                explosionTime = expTime;
            }

        }
        #endregion

        #region Awake
        private void Awake()
        {
            grenadesLeft = 15;//conect the number to the inventory
            transform.parent = hand.transform;
            render = GetComponent<MeshRenderer>();
            render.enabled = false;
            rb = GetComponent<Rigidbody>();
            rb.useGravity = false;

            grenade = new Grenade(20, 4f, 5f);//that should take the active grenade properties
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
            coli.radius = grenade.explosionRadius;
            coli.enabled = false;
            return coli;
        }
        #endregion

        #region Update
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.F) && !cooldown && grenadesLeft>=1)
            {
                grenadesLeft--;
                StartCoroutine(ThrowGrenade());
            }
        }
        #endregion

        #region ThrowGrenade
        IEnumerator ThrowGrenade()
        {
            cooldown = true;
            render.enabled = true;

            yield return new WaitForSeconds(1f);//fix it with the animation (player throwing grenade)
            transform.parent = null;
            rb.velocity = Vector3.zero;
            rb.useGravity = true;
            rb.AddForce(new Vector3(0, 0, throwingForce), ForceMode.Impulse);
            colision.enabled = true;

            yield return new WaitForSeconds(grenade.explosionTime);
            col.enabled = true;
            //Maybe could create a particle system here, I'm not pretty good at that

            yield return new WaitForSeconds(0.1f);
            col.enabled = false;
            colision.enabled = false;
            transform.parent = hand.transform;
            transform.position = Vector3.zero;
            yield return new WaitForSeconds(0.5f);//cooldown before new grenade
            cooldown = false;
        }
        #endregion
    }
}