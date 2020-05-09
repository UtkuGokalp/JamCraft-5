using UnityEngine;
using Utility.Development;

namespace JamCraft5.Camera
{
    [RequireComponent(typeof(CameraFollowing))]
    public class CameraRotation : MonoBehaviour
    {
        #region Variables
        [SerializeField]
        private float sensitivity;
        private Transform transformCache;
        private CameraFollowing cameraPlayerFollowController;
        public static float CurrentRotationAngle { get; private set; }
        public static bool RotatingCamera => Input.GetMouseButton(MouseButton.RIGHT);
        #endregion

        #region Awake
        private void Awake()
        {
            transformCache = transform;
            cameraPlayerFollowController = GetComponent<CameraFollowing>();
        }
        #endregion

        #region Update
        private void Update()
        {
            if (RotatingCamera)
            {
                float rotationAngle = sensitivity * Input.GetAxis("Mouse X");
                CurrentRotationAngle += rotationAngle;
                transformCache.RotateAround(GameUtility.PlayerPosition, Vector3.up, rotationAngle);
                cameraPlayerFollowController.OffsetFromPlayer = Quaternion.AngleAxis(rotationAngle, Vector3.up) * cameraPlayerFollowController.OffsetFromPlayer;
            }
        }
        #endregion
    }
}
