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
        [SerializeField]
        private float verticalRotationLimit;
        private float firstXRotation;
        private Transform transformCache;
        private CameraFollowing cameraPlayerFollowController;
        public static float CurrentVerticalRotationAngle { get; private set; }
        public static float CurrentHorizontalRotationAngle { get; private set; }
        public static bool RotatingCamera => Input.GetMouseButton(MouseButton.RIGHT);
        #endregion

        #region Awake
        private void Awake()
        {
            transformCache = transform;
            CurrentVerticalRotationAngle = firstXRotation = transformCache.rotation.eulerAngles.x;
            cameraPlayerFollowController = GetComponent<CameraFollowing>();
        }
        #endregion

        #region LateUpdate
        private void LateUpdate()
        {
            if (RotatingCamera)
            {
                //Horizontal rotation
                float horizontalRotationAngle = sensitivity * Input.GetAxis("Mouse X");
                CurrentHorizontalRotationAngle += horizontalRotationAngle;
                transformCache.RotateAround(GameUtility.PlayerPosition, Vector3.up, horizontalRotationAngle);
                cameraPlayerFollowController.OffsetFromPlayer = Quaternion.AngleAxis(horizontalRotationAngle, Vector3.up) * cameraPlayerFollowController.OffsetFromPlayer;
                
                //Vertical rotation
                float verticalRotationAngle = sensitivity * Input.GetAxis("Mouse Y");
                CurrentVerticalRotationAngle -= verticalRotationAngle; //Subtraction is used instead of addition because addition reverses the directions (mouse goes down, camera goes up and vice versa)
                CurrentVerticalRotationAngle = Mathf.Clamp(CurrentVerticalRotationAngle, firstXRotation - verticalRotationLimit, firstXRotation + verticalRotationLimit);
                transformCache.rotation = transformCache.rotation.ChangeEulerAngles(CurrentVerticalRotationAngle, null, null);
            }
        }
        #endregion
    }
}
