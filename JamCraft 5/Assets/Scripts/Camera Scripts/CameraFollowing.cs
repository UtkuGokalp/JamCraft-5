using UnityEngine;
using Utility.Development;
using JamCraft5.Player.Movement;

namespace JamCraft5.Camera
{
    public class CameraFollowing : MonoBehaviour
    {
        #region Variables
        [Range(0, 1)]
        [SerializeField]
        private float smoothing;
        [SerializeField]
        private Vector3 offsetFromPlayer;
        public Vector3 OffsetFromPlayer { get; set; }
        public static float DistanceBetweenCameraAndPlayer { get; private set; }
        #endregion

        #region Awake
        private void Awake()
        {
            OffsetFromPlayer = offsetFromPlayer;
            transform.position = GameUtility.PlayerPosition + OffsetFromPlayer;
            DistanceBetweenCameraAndPlayer = Vector3.Distance(transform.position, GameUtility.PlayerPosition);
        }
        #endregion

        #region LateUpdate
        private void LateUpdate()
        {
            Vector3 desiredPosition = GameUtility.PlayerPosition + (Quaternion.AngleAxis(PlayerRotationController.CurrentRotationOffset, Vector3.up) * OffsetFromPlayer);
            transform.position = Vector3.Slerp(transform.position, desiredPosition, smoothing);
            transform.LookAt(GameUtility.PlayerTransform, Vector3.up);
        }
        #endregion
    }
}
