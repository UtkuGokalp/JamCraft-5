using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JamCraft5.Enemies.Components
{
    public class EnemyDamaged : MonoBehaviour
    {
        private void OnTriggerEnter(Collider col)
        {
            if (col.CompareTag("PlayerAttack"))
            {
                GetDamage();
            }
        }

        void GetDamage()
        {

        }
    }
}

