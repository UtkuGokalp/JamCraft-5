﻿using UnityEngine;
using Utility.Development;
using JamCraft5.Player.Attack;
using JamCraft5.Audio;

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
        private Vector3 currentInput;
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
            if (PlayerUnlocking.playerPause) return;
            playerInput.x = Input.GetAxis("Horizontal");
            playerInput.z = Input.GetAxis("Vertical");
            currentInput.x = Input.GetAxisRaw("Horizontal");
            currentInput.z = Input.GetAxisRaw("Vertical");
            playerInput.Normalize();

            if (currentInput.sqrMagnitude != 0 && !PlayerAttack.Attacking && !PlayerUnlocking.playerPause)
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
                //If there is no input in current frame
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
                    rb.velocity = rb.velocity.With(movementDirection.x, null, movementDirection.z);

                    #region Player Rotation Without Mouse


                    //Rotate the player model without the mouse
                    switch (currentInput.x)//for some reason I can't switch a Vector3
                    {
                        case 1:
                            switch (currentInput.z)
                            {
                                case 1:
                                    transformCache.rotation = Quaternion.Euler(0, 45, 0);
                                    break;
                                case 0:
                                    transformCache.rotation = Quaternion.Euler(0, 90, 0);
                                    break;
                                case -1:
                                    transformCache.rotation = Quaternion.Euler(0, 135, 0);
                                    break;
                            }
                            break;
                        case 0:
                            switch (currentInput.z)
                            {
                                case 1:
                                    transformCache.rotation = Quaternion.Euler(0, 0, 0);
                                    break;
                                case -1:
                                    transformCache.rotation = Quaternion.Euler(0, 180, 0);
                                    break;
                            }
                            break;
                        case -1:
                            switch (currentInput.z)
                            {
                                case 1:
                                    transformCache.rotation = Quaternion.Euler(0, 315, 0);
                                    break;
                                case 0:
                                    transformCache.rotation = Quaternion.Euler(0, 270, 0);
                                    break;
                                case -1:
                                    transformCache.rotation = Quaternion.Euler(0, 225, 0);
                                    break;
                            }
                            break;
                    }



                    #endregion
                }
            }
        }

        #endregion

        #region GetYRotation
        private float GetYRotation()
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

        #region PlayFootstepSound
        private void PlayFootstepSound()
        {
            if (!PlayerUnlocking.playerPause && !PlayerAttack.Attacking && currentInput.sqrMagnitude != 0)
            {
                AudioManager.Instance.PlayAudio(Audio.AudioType.FootstepSound);
            }
        }
        #endregion
    }
}
