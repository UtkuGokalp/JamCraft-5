using UnityEngine;
using Utility.Development;

public class CameraFollowing : MonoBehaviour
{
    [SerializeField]
    private Vector3 playerDistance;
    [SerializeField]
    private Vector3 rotationOffset;

    private void FixedUpdate()
    {
        transform.position = GameUtility.PlayerPosition + playerDistance;
        transform.rotation = Quaternion.Euler(-rotationOffset);
        //I invert the rotation only for comodity, the usual rotations are about -50º
    }
}
