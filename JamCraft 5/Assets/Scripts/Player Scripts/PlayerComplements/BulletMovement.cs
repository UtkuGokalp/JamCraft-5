using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JamCraft5.Player.Attack
{
    public class BulletMovement : MonoBehaviour
    {
        [SerializeField]
        private float TimeToDie;
        [SerializeField]
        private int bulletError;
        [SerializeField]
        private int speed;

        private Rigidbody rb;
        private BoxCollider box;

        private void Start()
        {
            rb = gameObject.AddComponent<Rigidbody>();
            rb.useGravity = false;

            box = gameObject.AddComponent<BoxCollider>();
            box.isTrigger = true;
            box.tag = "PlayerAttack";

            transform.Rotate(Random.Range(-bulletError, bulletError), Random.Range(-bulletError, bulletError), Random.Range(-bulletError,bulletError));
        }
        void FixedUpdate()
        {
            if (TimeToDie > 0)
            {
                rb.AddRelativeForce(0, 0, speed * Time.fixedDeltaTime);
                TimeToDie -= Time.fixedDeltaTime;
            } else { Destroy(gameObject); }
        }
    }
}
