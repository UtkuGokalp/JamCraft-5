using UnityEngine;
using Utility.Development;

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
        public Vector3 OffsetFromPlayer { get => offsetFromPlayer; set => offsetFromPlayer = value; }
        #endregion

        #region Awake
        private void Awake()
        {
            //Set the first offset from player to the value set from the editor.
            //This is a separate variable so that changing the value from code
            //doesn't somehow affect the orginal value permanantly.
            OffsetFromPlayer = offsetFromPlayer;
        }
        #endregion

        #region FixedUpdate
        private void FixedUpdate()
        {
            Vector3 desiredPosition = GameUtility.PlayerPosition + OffsetFromPlayer;
            transform.position = Vector3.Slerp(transform.position, desiredPosition, smoothing);
        }
        #endregion
    }
}
