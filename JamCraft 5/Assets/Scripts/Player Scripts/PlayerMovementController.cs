using UnityEngine;
using Utility.Development;

namespace JamCraft5.Player.Movement
{
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerMovementController : MonoBehaviour
    {
        #region Variables
        [SerializeField]
        private bool calculateUsingRotation;
        [SerializeField]
        private float movementSpeed;
        [Range(0, 1)]
        [SerializeField]
        private float slowDownMultiplier;
        private Rigidbody rb;
        private Animator animator;
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
            animator = GetComponent<Animator>();
        }
        #endregion

        #region Update
        private void Update()
        {
            playerInput.x = Input.GetAxis("Horizontal");
            playerInput.z = Input.GetAxis("Vertical");
            currentInput.x = Input.GetAxisRaw("Horizontal");
            currentInput.z = Input.GetAxisRaw("Vertical");

            if (currentInput.sqrMagnitude != 0)
            {
                animator.SetBool("IsRunning", true);
            }
            else
            {
                animator.SetBool("IsRunning", false);
            }

            playerInput.Normalize();
        }
        #endregion

        #region FixedUpdate
        private void FixedUpdate()
        {
            if (!PlayerDashController.Dashing)
            {
                if (!calculateUsingRotation)
                {
                    Vector3 movementDirection = playerInput * movementSpeed * Time.fixedDeltaTime;
                    movementDirection.Normalize();
                    //If there is no input in current frame
                    if (currentInput.sqrMagnitude == 0)
                    {
                        if (movementDirection.sqrMagnitude > 0)
                        {
                            movementDirection *= slowDownMultiplier * Time.fixedDeltaTime;
                        }
                        rb.velocity = movementDirection.With(null, rb.velocity.y, null);
                    }
                    else
                    {
                        movementDirection = playerInput * movementSpeed * Time.fixedDeltaTime;
                        rb.velocity = rb.velocity.With(movementDirection.x, null, movementDirection.z);
                    }
                }
                else
                {
                    Vector3 movementDirection = default;
                    float yRotation = GeYtRotation();
                    bool facingBackwards = yRotation > 30 && yRotation < 230;
                    int horizontalAxisMultiplier = facingBackwards ? -1 : 1;

                    if (playerInput.x < 0)
                    {
                        if (playerInput.z == 0)
                        {
                            movementDirection = -transform.right * horizontalAxisMultiplier;
                        }
                        else if (playerInput.z > 0)
                        {
                            movementDirection = -transform.right * horizontalAxisMultiplier + transform.forward;
                        }
                        else if (playerInput.z < 0)
                        {
                            movementDirection = -transform.right * horizontalAxisMultiplier + -transform.forward;
                        }
                    }
                    else if (playerInput.x > 0)
                    {
                        if (playerInput.z == 0)
                        {
                            movementDirection = transform.right * horizontalAxisMultiplier;
                        }
                        else if (playerInput.z > 0)
                        {
                            movementDirection = transform.right * horizontalAxisMultiplier + transform.forward;
                        }
                        else if (playerInput.z < 0)
                        {
                            movementDirection = transform.right * horizontalAxisMultiplier + -transform.forward;
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
                    if (currentInput.sqrMagnitude == 0)
                    {
                        if (movementDirection.sqrMagnitude > 0)
                        {
                            movementDirection.Normalize();
                            movementDirection *= slowDownMultiplier * Time.fixedDeltaTime;
                        }
                        rb.velocity = movementDirection.With(null, rb.velocity.y, null);
                    }
                    else
                    {
                        movementDirection.Normalize();
                        movementDirection *= movementSpeed * Time.fixedDeltaTime;
                        rb.velocity = rb.velocity.With(movementDirection.x, null, movementDirection.z);
                    }
                }
            }
        }
        #endregion

        #region GeYtRotation
        private float GeYtRotation()
        {
            float yRotation = transform.rotation.eulerAngles.y;
            if (yRotation >= 360)
            {
                while (yRotation >= 360)
                {
                    yRotation -= 360;
                }
            }
            else if (yRotation < 0)
            {
                while (yRotation < 0)
                {
                    yRotation += 360;
                }
            }
            return yRotation;
        }
        #endregion
    }
}
