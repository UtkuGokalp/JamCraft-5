using UnityEngine;
using Utility.Development;

namespace JamCraft5.Player.Movement
{
    public class PlayerRotationController : MonoBehaviour
    {
        #region Variables
        private Transform transformCache;
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
            Ray ray = GameUtility.MainCam.ScreenPointToRay(Input.mousePosition);
            Plane plane = new Plane(Vector3.up, Vector3.zero);
            if (plane.Raycast(ray, out float distance))
            {
                Vector3 target = ray.GetPoint(distance);
                Vector3 direction = GameUtility.GetDirection(transformCache.position, target);
                float rotation = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
                transformCache.rotation = Quaternion.Euler(0, rotation, 0);
            }
        }
        #endregion
    }
}
