using UnityEngine;
using JamCraft5.Audio;
using Utility.Development;
using JamCraft5.Player.Attack;

namespace JamCraft5.Player.Movement
{
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerMovementController : MonoBehaviour
    {
        #region Variables
        [SerializeField]
        private float movementSpeed;
        [SerializeField]
        private float footstepSoundRate;
        [Range(0, 1)]
        [SerializeField]
        private float slowDownMultiplier;
        private Rigidbody rb;
        private Transform transformCache;
        private Animator animator;
        private Vector3 playerInput;
        /// <summary>
        /// Calculated using Input.GetAxisRaw() method, so there'll be no smoothing for this input. This means we can check this variable to see if the player is actually pressing keys in the current frame.
        /// </summary>
        private Vector3 rawPlayerInput;
        #endregion

        #region Awake
        private void Awake()
        {
            transformCache = transform;
            rb = GetComponent<Rigidbody>();
            animator = GetComponent<Animator>();
        }
        #endregion

        #region Start
        private void Start()
        {
            InvokeRepeating(nameof(PlayFootstepSound), 0, footstepSoundRate);
        }
        #endregion

        #region Update
        private void Update()
        {
            if (PlayerUnlocking.playerPause)
            {
                return;
            }
            playerInput.x = Input.GetAxis("Horizontal");
            playerInput.z = Input.GetAxis("Vertical");
            rawPlayerInput.x = Input.GetAxisRaw("Horizontal");
            rawPlayerInput.z = Input.GetAxisRaw("Vertical");
            playerInput.Normalize();

            if (rawPlayerInput.sqrMagnitude != 0 && !PlayerAttack.Attacking && !PlayerUnlocking.playerPause)
            {
                animator.SetBool("IsRunning", true);
            }
            else
            {
                animator.SetBool("IsRunning", false);
            }
        }
        #endregion
        
        #region FixedUpdate
        private void FixedUpdate()
        {
            if (PlayerAttack.Attacking || PlayerUnlocking.playerPause)
            {
                rb.velocity = Vector3.zero;
                animator.SetBool("IsRunning", false);
                return;
            }
            if (!PlayerDashController.Dashing)
            {
                Vector3 movementDirection = playerInput * movementSpeed * Time.fixedDeltaTime;

                movementDirection = Quaternion.AngleAxis(PlayerRotationController.CurrentRotationOffset, Vector3.up) * movementDirection;
                
                //If there is no input in current frame
                if (rawPlayerInput.sqrMagnitude == 0)
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
                    rb.velocity = rb.velocity.With(movementDirection.x, null, movementDirection.z);
                }
            }
        }

        #endregion

        #region PlayFootstepSound
        private void PlayFootstepSound()
        {
            if (!PlayerUnlocking.playerPause && !PlayerAttack.Attacking && rawPlayerInput.sqrMagnitude != 0)
            {
                AudioManager.Instance.PlaySFX(SFXType.FootstepSound);
            }
        }
        #endregion
    }
}
