using UnityEngine;
using System.Collections;
using Utility.Development;
using JamCraft5.Player.Attack;

namespace JamCraft5.Player.Movement
{
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerDashController : MonoBehaviour
    {
        #region Variables
        [SerializeField]
        private float dashSpeed;
        [SerializeField]
        private float dashTime;
        [SerializeField]
        private float cooldown;
        private bool coolD = false;

        private float currentDashTime;
        private Vector3 directionToMouse;
        private Rigidbody rb;
        private Transform transformCache;
        private PlayerUnlocking playerUnlockController;
        public static bool Dashing { get; private set; }
        #endregion

        #region Awake
        private void Awake()
        {
            transformCache = transform;
            rb = GetComponent<Rigidbody>();
            playerUnlockController = GetComponent<PlayerUnlocking>();
        }
        #endregion

        #region Update
        private void Update()
        {
            if (Input.GetMouseButtonDown(MouseButton.RIGHT) && !PlayerAttack.Attacking)
            {
                Dash();
            }
        }
        #endregion

        #region Dash
        private void Dash()
        {
            if (!Dashing && !coolD && !PlayerAttack.Attacking && playerUnlockController.Dash)
            {
                directionToMouse = GameUtility.GetDirectionToMouse(transformCache.position);
                currentDashTime = dashTime;
                Dashing = true;
            }
        }
        #endregion

        #region FixedUpdate
        private void FixedUpdate()
        {
            if (Dashing)
            {
                transform.localScale = new Vector3(4, 3.3f,6f);
                rb.velocity = directionToMouse * dashSpeed * Time.fixedDeltaTime;
                currentDashTime -= Time.fixedDeltaTime;

                if (currentDashTime <= 0)
                {
                    Dashing = false;
                    coolD = true;
                    transform.localScale = new Vector3(4, 4, 4);
                    StartCoroutine(ApplyCooldown());
                }
            }
        }
        #endregion

        #region ApplyCooldown
        IEnumerator ApplyCooldown()
        {
            yield return new WaitForSeconds(cooldown);
            coolD = false;
        }
        #endregion
    }
}
