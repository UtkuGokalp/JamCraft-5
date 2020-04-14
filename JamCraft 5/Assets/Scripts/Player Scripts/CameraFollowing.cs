using UnityEngine;
using Utility.Development;

public class CameraFollowing : MonoBehaviour
{
    [SerializeField]
    private Vector3 playerDistance;
    [SerializeField]
    private int xRotation;

    private void FixedUpdate()
    {
        transform.position = GameUtility.PlayerPosition + playerDistance;
        transform.rotation = Quaternion.Euler(Vector3.left * -xRotation);
        // I invert the rotation only for comodity, the usual rotations are about -50º
    }

    private void OnValidate()
    {
        xRotation = Mathf.Clamp(xRotation, 0, 360);
    }
}
