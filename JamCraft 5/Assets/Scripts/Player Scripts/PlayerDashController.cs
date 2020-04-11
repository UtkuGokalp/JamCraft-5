using UnityEngine;
using Utility.Development;

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
        private float currentDashTime;
        private Vector3 directionToMouse;
        private Rigidbody rb;
        private Transform transformCache;

        public static bool Dashing { get; private set; }
        #endregion

        #region Awake
        private void Awake()
        {
            transformCache = transform;
            rb = GetComponent<Rigidbody>();
        }
        #endregion

        #region Update
        private void Update()
        {
            if (Input.GetMouseButtonDown(MouseButton.RIGHT))
            {
                Dash();
            }
        }
        #endregion

        #region Dash
        private void Dash()
        {
            if (!Dashing)
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
                rb.velocity = directionToMouse * dashSpeed * Time.fixedDeltaTime;
                currentDashTime -= Time.fixedDeltaTime;

                if (currentDashTime <= 0)
                {
                    Dashing = false;
                }
            }
        }
        #endregion
    }
}
