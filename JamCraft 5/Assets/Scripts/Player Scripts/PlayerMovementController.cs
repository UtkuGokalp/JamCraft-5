using UnityEngine;
using Utility.Development;

namespace JamCraft5.Player.Movement
{
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerMovementController : MonoBehaviour
    {
        #region Variables
        [SerializeField]
        private float movementSpeed;
        [Range(0, 1)]
        [SerializeField]
        private float slowDownMultiplier;
        private Rigidbody rb;
        private Vector3 playerInput;
        /// <summary>
        /// Calculated using Input.GetAxisRaw() method, so there'll be no smoothing for this input. This means we can check this variable to see if the player is actually pressing keys in the current frame.
        /// </summary>
        private Vector3 currentInput;
        #endregion

        #region Awake
        private void Awake()
        {
            rb = GetComponent<Rigidbody>();
        }
        #endregion

        #region Update
        private void Update()
        {
            playerInput.x = Input.GetAxis("Horizontal");
            playerInput.z = Input.GetAxis("Vertical");
            currentInput.x = Input.GetAxisRaw("Horizontal");
            currentInput.z = Input.GetAxisRaw("Vertical");

            playerInput.Normalize();
        }
        #endregion

        #region FixedUpdate
        private void FixedUpdate()
        {
            if (!PlayerDashController.Dashing)
            {
                Vector3 movementDirection = default;
                //Every transform.right here
                if (playerInput.x < 0)
                {
                    if (playerInput.z == 0)
                    {
                        movementDirection = -transform.right;
                    }
                    else if (playerInput.z > 0)
                    {
                        movementDirection.x = -transform.right.x + transform.forward.x;
                        movementDirection.y = -transform.right.y + transform.forward.y;
                        movementDirection.z = -transform.right.z + transform.forward.z;
                    }
                    else if (playerInput.z < 0)
                    {
                        movementDirection.x = -transform.right.x + -transform.forward.x;
                        movementDirection.y = -transform.right.y + -transform.forward.y;
                        movementDirection.z = -transform.right.z + -transform.forward.z;
                    }
                }
                else if (playerInput.x > 0)
                {
                    if (playerInput.z == 0)
                    {
                        movementDirection = transform.right;
                    }
                    else if (playerInput.z > 0)
                    {
                        movementDirection.x = transform.right.x + transform.forward.x;
                        movementDirection.y = transform.right.y + transform.forward.y;
                        movementDirection.z = transform.right.z + transform.forward.z;
                    }
                    else if (playerInput.z < 0)
                    {
                        movementDirection.x = transform.right.x + -transform.forward.x;
                        movementDirection.y = transform.right.y + -transform.forward.y;
                        movementDirection.z = transform.right.z + -transform.forward.z;
                    }
                }
                else
                {
                    if (playerInput.z > 0)
                    {
                        movementDirection = transform.forward;
                    }
                    else if (playerInput.z < 0)
                    {
                        movementDirection = -transform.forward;
                    }
                }

                //All axis are 0, no input
                if (currentInput.magnitude == 0)
                {
                    if (movementDirection.magnitude > 0)
                    {
                        movementDirection *= slowDownMultiplier * Time.fixedDeltaTime;
                    }
                    rb.velocity = movementDirection.With(null, rb.velocity.y, null);
                }
                else
                {
                    movementDirection *= movementSpeed * Time.fixedDeltaTime;
                    rb.velocity = rb.velocity.With(movementDirection.x, null, movementDirection.z);
                }
            }
        }
        #endregion
    }
}
