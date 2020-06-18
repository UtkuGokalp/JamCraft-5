﻿using UnityEngine;
using Utility.Development;
using JamCraft5.Player.Attack;
using JamCraft5.EventArguments;

namespace JamCraft5.Player.Movement
{
    public class PlayerRotationController : MonoBehaviour
    {
        #region Variables
        [Range(0, 1)]
        [SerializeField]
        private float lerpSpeed;
        private Transform transformCache;
        private float previousAngle;
        public static float CurrentRotationOffset { get; private set; }
        public static event System.EventHandler<OnPlayerRotationChangedEventArgs> OnPlayerRotationChanged;
        #endregion

        #region Awake
        private void Awake()
        {
            transformCache = transform;
        }
        #endregion

        #region Update
        private void Update()
        {
            if (!PlayerAttack.Attacking && !PlayerDashController.Dashing && !PlayerUnlocking.playerPause)
            {
                Vector3 direction = GameUtility.GetDirectionToMouse(transformCache.position);
                float rotation = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
                Quaternion desiredRotation = Quaternion.Euler(transformCache.rotation.eulerAngles.x, rotation, transformCache.rotation.eulerAngles.z);
                transformCache.rotation = Quaternion.Slerp(transformCache.rotation, desiredRotation, lerpSpeed);
                if (previousAngle != rotation)
                {
                    CurrentRotationOffset += rotation;
                    previousAngle = rotation;
                    OnPlayerRotationChanged?.Invoke(this, new OnPlayerRotationChangedEventArgs(CurrentRotationOffset));
                }
            }
        }
        #endregion
    }
}
