using UnityEngine;
using Utility.Development;

[RequireComponent(typeof(CameraFollowing))]
public class CameraRotation : MonoBehaviour
{
    #region Variables
    [SerializeField]
    private float sensitivity;
    private Transform transformCache;
    private CameraFollowing cameraPlayerFollowController;
    public static bool RotatingCamera => Input.GetMouseButton(MouseButton.MIDDLE);
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
            transformCache.RotateAround(GameUtility.PlayerPosition, Vector3.up, rotationAngle);
            cameraPlayerFollowController.OffsetFromPlayer = Quaternion.AngleAxis(rotationAngle, Vector3.up) * cameraPlayerFollowController.OffsetFromPlayer;
        }
    }
    #endregion
}
