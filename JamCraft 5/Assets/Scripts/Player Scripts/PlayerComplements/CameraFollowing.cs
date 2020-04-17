using UnityEngine;
using Utility.Development;

public class CameraFollowing : MonoBehaviour
{
    [Range(0, 1)]
    [SerializeField]
    private float smoothing;
    [SerializeField]
    private Vector3 playerDistance;
    [SerializeField]
    private Vector3 rotationOffset;

    private void FixedUpdate()
    {
        Vector3 desiredPosition = GameUtility.PlayerPosition + playerDistance;
        transform.position = Vector3.Slerp(transform.position, desiredPosition, smoothing);

        Quaternion desiredRotation = Quaternion.Euler(-rotationOffset);
        transform.rotation = Quaternion.Slerp(transform.rotation, desiredRotation, smoothing);
        //I invert the rotation only for comodity, the usual rotations are about -50º
    }
}
