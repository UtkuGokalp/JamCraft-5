using UnityEngine;
using Utility.Development;

namespace JamCraft5.Camera
{
    public class CameraClippingPlanesController : MonoBehaviour
    {
        #region Variables
        private Transform transformCache;
        private Vector3 DirectionToPlayer => GameUtility.GetDirection(transformCache.position, GameUtility.PlayerPosition);
        #endregion

        #region Awake
        private void Awake()
        {
            transformCache = transform;
        }
        #endregion

        #region FixedUpdate
        private void FixedUpdate()
        {
            RaycastHit[] hitInfos = Physics.RaycastAll(transformCache.position, DirectionToPlayer, CameraFollowing.DistanceBetweenCameraAndPlayer);

            foreach (RaycastHit hitInfo in hitInfos)
            {
                if (hitInfo.transform.CompareTag(GameUtility.PLAYER_TAG))
                {
                    continue;
                }
                if (hitInfo.transform.GetComponent<ObjectTransparencyController>() == null)
                {
                    hitInfo.transform.gameObject.AddComponent<ObjectTransparencyController>();
                }
            }
        }
        #endregion
    }
}
